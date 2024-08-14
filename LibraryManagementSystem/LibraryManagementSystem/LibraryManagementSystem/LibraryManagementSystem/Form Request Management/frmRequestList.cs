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
    public partial class frmRequestList : Form
    {
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        private string username = "ADMIN - CIRCULATION";
        int currentPage = 1;
        int rowsPerPage = 10;

        public frmRequestList()
        {
            InitializeComponent();
        }

        public void autoRefresh()
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = bookList.GetData("SELECT Id, Accession, requestedBy, dateRequest, statusRequest FROM tblbookrequest WHERE Id <> '" + username +
                    "' ORDER BY Id ");

                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Material Issue info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private void frmRequestList_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = bookList.GetData("SELECT Id, Accession, requestedBy, dateRequest, statusRequest FROM tblbookrequest WHERE Id <> '" + username +
                    "'AND(requestedBy LIKE '%" + txtSearch.Text + "%' OR statusRequest LIKE '%" + txtSearch.Text + "%' ) ORDER BY Id ");

                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Material Issue info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApproved_Click(object sender, EventArgs e)
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
                        string Status = row.Cells[5].Value.ToString();
                        if (Status != "WAITING FOR APPROVAL")
                        {
                            isValid = false;
                            MessageBox.Show("Selected requests must be in 'WAITING FOR APPROVAL' status!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                if (isValid && isChecked) // check if at least one checkbox is checked
                {
                    // Ask for confirmation
                    DialogResult answer = MessageBox.Show("Are you sure you want to approve the selected requests?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        // Approval code goes here
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[i].Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                            if (checkbox.Value != null && (bool)checkbox.Value == true)
                            {
                                string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                bookList.executeSQL("UPDATE tblbookrequest SET statusRequest = 'APPROVED' WHERE ID = '" + ID + "'");
                                if (bookList.rowAffected > 0)
                                {
                                    // Do something if the update is successful
                                    bookList.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Request Online','bookrequestID " + ID + " status request is changed to approved')");
                                }
                            }
                        }
                        MessageBox.Show("Selected requests approved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        autoRefresh();
                    }
                }
                else if (!isChecked) // display error message if no checkbox is checked
                {
                    MessageBox.Show("Please select at least one request to approve!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Approve Request!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRejected_Click(object sender, EventArgs e)
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
                        string Status = row.Cells[5].Value.ToString();
                        if (Status != "WAITING FOR APPROVAL")
                        {
                            isValid = false;
                            MessageBox.Show("Selected requests must be in 'WAITING FOR APPROVAL' status!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                if (isValid && isChecked) // check if at least one checkbox is checked
                {
                    // Ask for confirmation
                    DialogResult answer = MessageBox.Show("Are you sure you want to reject the selected requests?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        // Approval code goes here
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[i].Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                            if (checkbox.Value != null && (bool)checkbox.Value == true)
                            {
                                string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                bookList.executeSQL("UPDATE tblbookrequest SET statusRequest = 'REJECTED' WHERE ID = '" + ID + "'");
                                if (bookList.rowAffected > 0)
                                {
                                    // Do something if the update is successful
                                    bookList.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Request Online','Bookrequest ID " + ID + " status request is changed to rejected')");
                                }
                            }
                        }
                        MessageBox.Show("Selected requests rejected!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        autoRefresh();
                    }
                }
                else if (!isChecked) // display error message if no checkbox is checked
                {
                    MessageBox.Show("Please select at least one request to reject!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Reject Request!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            autoRefresh();
        }
    }
}
