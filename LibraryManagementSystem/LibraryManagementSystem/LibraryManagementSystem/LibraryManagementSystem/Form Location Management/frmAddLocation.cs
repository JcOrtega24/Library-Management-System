using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LibraryManagementSystem.Form_Location_Management
{
    public partial class frmAddLocation : Form
    {
        public frmAddLocation()
        {
            InitializeComponent();
        }

        Library1 location = new Library1("localhost", "librarymanagementstudent", "root", "");

        private void validateLocation()
        {
            DataTable dt = location.GetData("SELECT * FROM location WHERE Location = '" + txtLocation.Text + "'");
            if (txtLocation.Text == "")
            {
                errorProvider1.SetError(txtLocation, "Location is Required");
            }
            else if (dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtLocation, "Location should be Unique");
            }
            else
            {
                errorProvider1.SetError(txtLocation, "");

            }
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
                        location.executeSQL("INSERT INTO location (Location) VALUES('" + txtLocation.Text + "') ");
                        if (location.rowAffected > 0)
                        {
                            MessageBox.Show("New Location added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            location.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Location','" + txtLocation.Text + " added in location')");
                            frmLocationList newFrm = (frmLocationList)Application.OpenForms["frmLocationList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Add new publisher!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLocation.Clear();
        }

        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
