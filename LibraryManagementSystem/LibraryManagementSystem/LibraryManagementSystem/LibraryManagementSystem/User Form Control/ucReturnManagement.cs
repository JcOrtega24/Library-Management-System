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
    public partial class ucReturnManagement : UserControl
    {
        public ucReturnManagement()
        {
            InitializeComponent();
        }
        int currentPage = 1;
        int rowsPerPage = 10;
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        public void autoRefresh()
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = bookList.GetData("SELECT Id, Library_ID, User, Title, Accession, Issue_Date, Expected_Return, Date_Return, Status FROM tblbookassign WHERE Status = 'RETURNED' " +
                    "AND('Library_ID' LIKE '%" + txtSearch.Text + "%' OR Title LIKE '%" + txtSearch.Text + "%' OR User LIKE '%" + txtSearch.Text + "%') ORDER BY Id ");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Material Issue info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucReturnManagement_Load(object sender, EventArgs e)
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
                        string Status = row.Cells[9].Value.ToString();
                        if (Status != "RETURNED")
                        {
                            isValid = false;
                            MessageBox.Show("Selected requests must be in 'RETURNED' status!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                if (isValid && isChecked) // check if at least one checkbox is checked
                {
                    // Ask for confirmation
                    DialogResult answer = MessageBox.Show("Are you sure you want to delete the selected requests?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        // Deletion code goes here
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[i].Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                            if (checkbox.Value != null && (bool)checkbox.Value == true)
                            {
                                string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                bookList.executeSQL("DELETE FROM tblbookassign WHERE Id = '" + ID + "'");
                                if (bookList.rowAffected > 0)
                                {
                                    // Do something if the deletion is successful
                                }
                            }
                        }
                        MessageBox.Show("Selected data deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        autoRefresh();
                    }
                }
                else if (!isChecked) // display error message if no checkbox is checked
                {
                    MessageBox.Show("Please select at least one data to delete!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Delete Request!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
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
                string Library_ID, User, Title, Accession, Issue_Date, Expected_Return, Date_Return, Status;
                Library_ID = checkedRow.Cells[1].Value.ToString();
                User = checkedRow.Cells[2].Value.ToString();
                Title = checkedRow.Cells[3].Value.ToString();
                Accession = checkedRow.Cells[4].Value.ToString();
                Issue_Date = checkedRow.Cells[5].Value.ToString();
                Expected_Return = checkedRow.Cells[6].Value.ToString();
                Date_Return = checkedRow.Cells[7].Value.ToString();
                Status = checkedRow.Cells[8].Value.ToString();
                frmIssueDetails viewStudent = new frmIssueDetails(Library_ID, User, Title, Accession, Issue_Date, Expected_Return, Date_Return, Status);
                viewStudent.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
