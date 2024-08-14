using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class frmUserManagement : Form
    {
        private string username = "ADMIN - CIRCULATION";
        int currentPage = 1;
        int rowsPerPage = 10;

        public frmUserManagement()
        {
            InitializeComponent();
        }

        Library1 student = new Library1("localhost", "librarymanagementstudent", "root", "");
        public void autoRefresh()
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = student.GetData("SELECT ID_Number, Password, Library_Access_ID, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status, Image FROM tbluseraccount WHERE ID_Number <> '" + username +
                    "' ORDER BY Reg_Date ASC LIMIT " + offset + ", " + rowsPerPage);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[12].Visible = false;

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {
                    if (dc.Index.Equals(0))
                    {
                        dc.ReadOnly = false;
                    }
                    else
                    {
                        dc.ReadOnly = true;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Student info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = student.GetData("SELECT ID_Number, Password, Library_Access_ID, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status, Image FROM tbluseraccount WHERE ID_Number <> '" + username +
                    "'AND(ID_Number LIKE '%" + txtSearch.Text + "%' OR Library_Access_ID LIKE '%" + txtSearch.Text + "%' OR Student_Name LIKE '%" + txtSearch.Text + "%' OR Course LIKE '%" + txtSearch.Text + "%'  OR Status LIKE '%" + txtSearch.Text + "%') ORDER BY Reg_Date ASC LIMIT " + offset + ", " + rowsPerPage);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[12].Visible = false;

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Student info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            int checkedCount = 0; // Count of checked checkboxes
            DataGridViewRow checkedRow = null; // The row with the checked checkbox

            // Loop through the rows and check the checkboxes
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if (chk.Value != null && (bool)chk.Value)
                {
                    checkedCount++;
                    checkedRow = dataGridView1.Rows[i];
                }
            }

            // If more than one checkbox is checked, show an error message
            if (checkedCount > 1)
            {
                MessageBox.Show("Please select only one row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If one checkbox is checked, open the form
            else if (checkedCount == 1)
            {
                string ID_Number, Password, Student_Name, Library_Access_ID, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status;
                ID_Number = checkedRow.Cells[1].Value.ToString();
                Password = checkedRow.Cells[2].Value.ToString();
                Library_Access_ID = checkedRow.Cells[3].Value.ToString();
                Student_Name = checkedRow.Cells[4].Value.ToString();
                Course = checkedRow.Cells[5].Value.ToString();
                Email_ID = checkedRow.Cells[6].Value.ToString();
                Mobile_Number = checkedRow.Cells[7].Value.ToString();
                Reg_Date = checkedRow.Cells[8].Value.ToString();
                dateApproved = checkedRow.Cells[9].Value.ToString();
                approvedBy = checkedRow.Cells[10].Value.ToString();
                Status = checkedRow.Cells[11].Value.ToString();;
                frmStudentInfo viewStudent = new frmStudentInfo(ID_Number, Password, Student_Name, Library_Access_ID, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status);
                viewStudent.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            int checkedCount = 0; // Count of checked checkboxes
            DataGridViewRow checkedRow = null; // The row with the checked checkbox

            // Loop through the rows and check the checkboxes
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if (chk.Value != null && (bool)chk.Value)
                {
                    checkedCount++;
                    checkedRow = dataGridView1.Rows[i];
                }
            }

            // If more than one checkbox is checked, show an error message
            if (checkedCount > 1)
            {
                MessageBox.Show("Please select only one row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If one checkbox is checked, open the form
            else if (checkedCount == 1)
            {
                string ID_Number, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status;
                ID_Number = checkedRow.Cells[1].Value.ToString();
                Student_Name = checkedRow.Cells[3].Value.ToString();
                Course = checkedRow.Cells[5].Value.ToString();
                Email_ID = checkedRow.Cells[6].Value.ToString();
                Mobile_Number = checkedRow.Cells[7].Value.ToString();
                Reg_Date = checkedRow.Cells[8].Value.ToString();
                dateApproved = checkedRow.Cells[9].Value.ToString();
                Status = checkedRow.Cells[11].Value.ToString();
                if(Status == "WAITING FOR APPROVAL")
                {
                    frmApproveStudent approveStudent = new frmApproveStudent(ID_Number, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, Status, username);
                    approveStudent.Show();
                }
                else
                {
                    MessageBox.Show("The selected student's status is not waiting for approval.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {    
            int checkedCount = 0; // Count of checked checkboxes
            DataGridViewRow checkedRow = null; // The row with the checked checkbox

            // Loop through the rows and check the checkboxes
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if (chk.Value != null && (bool)chk.Value)
                {
                    checkedCount++;
                    checkedRow = dataGridView1.Rows[i];
                }
            }

            // If more than one checkbox is checked, show an error message
            if (checkedCount > 1)
            {
                MessageBox.Show("Please select only one row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If one checkbox is checked, open the form
            else if (checkedCount == 1)
            {
                string Email_Id;
                Email_Id = checkedRow.Cells[6].Value.ToString();
                frmEmail mail = new frmEmail(Email_Id);
                mail.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLabelBack_Click(object sender, EventArgs e)
        {
            currentPage--;
            if (currentPage < 1)
                currentPage = 1;
            autoRefresh();
        }

        private void btnLabelNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            autoRefresh();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation code goes here
                bool isValid = true;
                bool isChecked = false; // added variable to check if at least one checkbox is checked
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell checkbox = row.Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                    if (checkbox.Value != null && (bool)checkbox.Value == true)
                    {
                        isChecked = true; // update variable if at least one checkbox is checked
                        string Status = row.Cells[11].Value.ToString();                      
                    }
                }
                if (isValid && isChecked) // check if at least one checkbox is checked
                {
                    // Ask for confirmation
                    DialogResult answer = MessageBox.Show("Are you sure you want to delete this account?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        // Deletion code goes here
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[i].Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                            if (checkbox.Value != null && (bool)checkbox.Value == true)
                            {
                                string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                student.executeSQL("DELETE FROM tbluseraccount WHERE ID_Number = '" + ID + "'");
                                if (student.rowAffected > 0)
                                {
                                    MessageBox.Show("Selected data deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    student.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','User Management','" + ID + " user account is deleted')");
                                    autoRefresh();
                                }
                            }
                        }            
                    }
                }
                else if (!isChecked) // display error message if no checkbox is checked
                {
                    MessageBox.Show("Please select at least one data to delete!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Delete User!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
