using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LibraryManagementSystem.Form_Publisher_Management
{
    public partial class frmUpdatePublisher : Form
    {
        string ID, name, gender;

        public frmUpdatePublisher(string ID, string name, string gender)
        {
            InitializeComponent();

            this.ID = ID;
            this.name = name;
            this.gender = gender;
        }

        Library1 Publisher = new Library1("localhost", "librarymanagementstudent", "root", "");      

        private void validateName()
        {
            DataTable dt = Publisher.GetData("SELECT * FROM publisher WHERE Name = '" + txtName.Text + "'");
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

        private void frmUpdatePublisher_Load(object sender, EventArgs e)
        {
            txtName.Text = name;
            cmbGender.Text = gender;
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

        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
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
                        Publisher.executeSQL("UPDATE publisher SET name = '" + txtName.Text + "', gender = '" + cmbGender.Text + "' WHERE id = '" + ID + "'");
                        if (Publisher.rowAffected > 0)
                        {
                            MessageBox.Show("Publisher Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmPublisherList newFrm = (frmPublisherList)Application.OpenForms["frmPublisherList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Update Publisher!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }    
}
