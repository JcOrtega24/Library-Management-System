using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem.Form_Author_Management
{
    public partial class frmUpdateAuthor : Form
    {
        bool drag = false;
        Point start_point = new Point(0, 0);

        string ID, name, gender;

        public frmUpdateAuthor(string ID, string name, string gender)
        {
            InitializeComponent();
            this.ID = ID;
            this.name = name;
            this.gender = gender;
        }

        Library1 Author = new Library1("localhost", "librarymanagementstudent", "root", "");
        private void validateName()
        {
            DataTable dt = Author.GetData("SELECT * FROM author WHERE Name = '" + txtName.Text + "'");
            if (txtName.Text == "")
            {
                errorProvider1.SetError(txtName, "Name is Required");
            }
            else if (txtName.Text != name && dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtName, "Name should be Unique");
            }
            else
            {
                errorProvider1.SetError(txtName, "");

            }
        }
        private void validateGender()
        {
            if (cmbGender.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbGender, "Gender is Required");
            }
            else
            {
                errorProvider1.SetError(cmbGender, "");
            }
        }
        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            drag = true;
            start_point = new Point(e.X, e.Y);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - start_point.X, p.Y - start_point.Y);
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
        }
        private int errorCount;
        public void ErrorCounts()
        {
            errorCount = 0;
            foreach (Control c in errorProvider1.ContainerControl.Controls)
            {
                if (errorProvider1.GetError(c) != "")
                {
                    errorCount++;
                }
            }
        }

        private void frmUpdateAuthor_Load(object sender, EventArgs e)
        {
            txtName.Text = name;
            cmbGender.Text = gender;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            cmbGender.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            validateName();
            validateGender();
            ErrorCounts();

            if (errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        Author.executeSQL("UPDATE author SET name = '" + txtName.Text + "', gender = '" + cmbGender.Text + "' WHERE id = '" + ID + "'");
                        if (Author.rowAffected > 0)
                        {
                            MessageBox.Show("Author Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmAuthorList newFrm = (frmAuthorList)Application.OpenForms["frmAuthorList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Update Author!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
