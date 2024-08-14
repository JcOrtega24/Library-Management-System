using LibraryManagementSystem.Form_Categoy_Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem.Form_Department_Management
{
    public partial class frmUpdateDepartment : Form
    {
        Library1 Department = new Library1("localhost", "librarymanagementstudent", "root", "");
        string ID, department;
        int errorCount;

        private void frmUpdateDepartment_Load(object sender, EventArgs e)
        {
            txtDepartment.Text = department;
        }

        private void validateDepartment()
        {
            DataTable dt = Department.GetData("SELECT * FROM department WHERE Department = '" + txtDepartment.Text + "'");
            if (txtDepartment.Text == "")
            {
                errorProvider1.SetError(txtDepartment, "Department is Required");
            }
            else if (txtDepartment.Text != department && dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtDepartment, "Department name is already existing");
            }
            else
            {
                errorProvider1.SetError(txtDepartment, "");

            }
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            validateDepartment();
            ErrorCounts();

            if (errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        Department.executeSQL("UPDATE department SET Department = '" + txtDepartment.Text + "' WHERE DepartmentID = '" + ID + "'");
                        if (Department.rowAffected > 0)
                        {
                            MessageBox.Show("Department Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmDepartmentList newFrm = (frmDepartmentList)Application.OpenForms["frmDepartmentList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Update Department!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDepartment.Clear();
        }

        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmUpdateDepartment(string ID, string department)
        {
            InitializeComponent();
            this.ID = ID;
            this.department = department;
        }
    }
}
