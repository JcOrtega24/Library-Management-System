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
using System.Xml.Linq;

namespace LibraryManagementSystem.Form_Department_Management
{
    public partial class frmAddDepartment : Form
    {
        Library1 department = new Library1("localhost", "librarymanagementstudent", "root", "");

        public frmAddDepartment()
        {
            InitializeComponent();
        }

        private void validateDepartment()
        {
            DataTable dt = department.GetData("SELECT * FROM department WHERE Department = '" + txtDepartment.Text + "'");
            if (txtDepartment.Text == "")
            {
                errorProvider1.SetError(txtDepartment, "Department is Required");
            }
            else if (dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtDepartment, "Department name is already existing");
            }
            else
            {
                errorProvider1.SetError(txtDepartment, "");

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
            validateDepartment();
            ErrorCounts();
            if (errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        department.executeSQL("INSERT INTO department (Department) VALUES('" + txtDepartment.Text + "') ");
                        if (department.rowAffected > 0)
                        {
                            MessageBox.Show("New Department added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            department.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Department','" + txtDepartment.Text + " added in department')");
                            frmDepartmentList newFrm = (frmDepartmentList)Application.OpenForms["frmDepartmentList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Add new Department!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
