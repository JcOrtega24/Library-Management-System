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
using System.Xml.Linq;

namespace LibraryManagementSystem.Form_Location_Management
{
    public partial class frmUpdateLocation : Form
    {
        Library1 Location = new Library1("localhost", "librarymanagementstudent", "root", "");
        string ID, location;

        private void btnSave_Click(object sender, EventArgs e)
        {
            validateLocation();
            ErrorCounts();

            if (errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        Location.executeSQL("UPDATE location SET Location = '" + txtLocation.Text + "' WHERE LocationID = '" + ID + "'");
                        if (Location.rowAffected > 0)
                        {
                            MessageBox.Show("Location Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmLocationList newFrm = (frmLocationList)Application.OpenForms["frmLocationList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Update Location!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public frmUpdateLocation(string ID, string location)
        {
            InitializeComponent();
            this.ID = ID;
            this.location = location;
        }

        private int errorCount;

        private void frmUpdateLocation_Load(object sender, EventArgs e)
        {
            txtLocation.Text = location;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLocation.Clear();
        }

        private void validateLocation()
        {
            DataTable dt = Location.GetData("SELECT * FROM publisher WHERE Name = '" + txtLocation.Text + "'");
            if (txtLocation.Text == "")
            {
                errorProvider1.SetError(txtLocation, "Name is Required");
            }
            else if (txtLocation.Text != location && dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtLocation, "Name should be Unique");
            }
            else
            {
                errorProvider1.SetError(txtLocation, "");
            }
        }

        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
    }
}
