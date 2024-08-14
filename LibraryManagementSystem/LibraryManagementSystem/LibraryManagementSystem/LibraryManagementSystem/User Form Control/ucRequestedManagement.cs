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
    public partial class ucRequestedManagement : UserControl
    {
        public ucRequestedManagement()
        {
            InitializeComponent();
        }
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        private string username = "ADMIN - CIRCULATION";
        int currentPage = 1;
        int rowsPerPage = 10;
        public void autoRefresh()
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = bookList.GetData("SELECT Id, Accession, requestedBy, dateRequest, statusRequest FROM tblbookrequest WHERE Id <> '" + username +
                    "'AND(requestedBy LIKE '%" + txtSearch.Text + "%' OR statusRequest LIKE '%" + txtSearch.Text + "%' ) ORDER BY Id ");

                dataGridView1.DataSource = dt;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Material Issue info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*private void AddCheckBoxHeader()
        {
            // Create a CheckBox control and set its properties
            CheckBox checkBox = new CheckBox();
            checkBox.Size = new Size(15, 15);
            checkBox.BackColor = Color.Transparent;
            checkBox.Margin = new Padding(3, 0, 0, 0);

            // Calculate the horizontal center of the column header
            int headerCenter = dataGridView1.Columns[0].HeaderCell.ContentBounds.X + (dataGridView1.Columns[0].HeaderCell.ContentBounds.Width / 5) - (checkBox.Width / 5);

            // Attach the CheckBox control to the column header
            dataGridView1.Controls.Add(checkBox);
            checkBox.Location = new Point(headerCenter, dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.Y);
            checkBox.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
        }
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // Get the CheckBox control that raised the event
            CheckBox checkBox = (CheckBox)sender;

            // Check or uncheck all CheckBox controls in the column
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell checkboxCell = dataGridView1.Rows[i].Cells[0] as DataGridViewCheckBoxCell;
                checkboxCell.Value = checkBox.Checked;
            }
        }*/

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void ucRequestedManagement_Load(object sender, EventArgs e)
        {
            autoRefresh();
            //AddCheckBoxHeader();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            autoRefresh();
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
    }
}
