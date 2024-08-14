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
    public partial class frmCollectionManagement : Form
    {
        private string username = "ADMIN - CIRCULATION";
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 10;
        public frmCollectionManagement()
        {
            InitializeComponent();
        }

        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");

        public void autoRefresh()
        {
            if (cmbType.SelectedIndex == 0 || cmbType.SelectedIndex == -1)
            {
                try
                {
                    int startIndex = (currentPage - 1) * pageSize;
                    DataTable dt = bookList.GetData($"SELECT Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, Image FROM tblbooksinformation WHERE Type != '{username}' ORDER BY Accession LIMIT {startIndex}, {pageSize}");
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].Visible = false;
                    dataGridView1.Columns[14].Visible = false;
                 
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

                    int recordCount = Convert.ToInt32(bookList.GetData($"SELECT COUNT(*) FROM tblbooksinformation").Rows[0][0]);
                    totalPages = (int)Math.Ceiling((double)recordCount / pageSize);

                    lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Books info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    int startIndex = (currentPage - 1) * pageSize;
                    DataTable dt = bookList.GetData($"SELECT Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, Image FROM tblbooksinformation WHERE Type = '{cmbType.Text}' AND (Accession LIKE '%{txtSearch.Text}%' OR Title LIKE '%{txtSearch.Text}%' OR Author LIKE '%{txtSearch.Text}%' OR Subject LIKE '%{txtSearch.Text}%'  OR Publisher LIKE '%{txtSearch.Text}%') ORDER BY Accession LIMIT {startIndex}, {pageSize}");

                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[6].Visible = false;
                    dataGridView1.Columns[8].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].Visible = false;
                    dataGridView1.Columns[14].Visible = false;

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

                    int recordCount = Convert.ToInt32(bookList.GetData($"SELECT COUNT(*) FROM tblbooksinformation WHERE Type = '{cmbType.Text}'").Rows[0][0]);
                    totalPages = (int)Math.Ceiling((double)recordCount / pageSize);

                    lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Books info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmCollectionManagement_Load(object sender, EventArgs e)
        {
            //Author
            try
            {
                DataTable dt = bookList.GetData("SELECT * FROM category");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string category;
                        category = dt.Rows[i]["Category"].ToString();
                        cmbType.Items.Add(category);
                    }
                }

                autoRefresh();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Category Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbType.Text = "Select material";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddMaterial addBook = new frmAddMaterial(username);
            addBook.Show();
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
                string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, ImageText;
                Accession = checkedRow.Cells[1].Value.ToString();
                Title = checkedRow.Cells[2].Value.ToString();
                Author = checkedRow.Cells[3].Value.ToString();
                Publisher = checkedRow.Cells[4].Value.ToString();
                Language = checkedRow.Cells[5].Value.ToString();
                DatePublish = checkedRow.Cells[6].Value.ToString();
                Subject = checkedRow.Cells[7].Value.ToString();
                Type = checkedRow.Cells[8].Value.ToString();
                Department = checkedRow.Cells[9].Value.ToString();
                Location = checkedRow.Cells[10].Value.ToString();
                Status = checkedRow.Cells[11].Value.ToString();
                Quantity = checkedRow.Cells[12].Value.ToString();
                PageNumber = checkedRow.Cells[13].Value.ToString();
                ImageText = checkedRow.Cells[14].Value.ToString();
                frmBookInfoDetails viewBooks = new frmBookInfoDetails(Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, ImageText);
                viewBooks.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar(e.KeyChar) == 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void cmbType_TextChanged(object sender, EventArgs e)
        {
            autoRefresh();    
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void cmbType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                errorProvider1.SetError(cmbType, "Input must be text only");
            }
            else
            {
                errorProvider1.Clear();
            }
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
                        if (Status != "Not Available")
                        {
                            isValid = false;
                            MessageBox.Show("Selected requests must be in 'NOT AVAILABLE' status!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                if (isValid && isChecked) // check if at least one checkbox is checked
                {
                    // Ask for confirmation
                    DialogResult answer = MessageBox.Show("Are you sure you want to delete the selected list?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        // Deletion code goes here
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[i].Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                            if (checkbox.Value != null && (bool)checkbox.Value == true)
                            {
                                string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                bookList.executeSQL("DELETE FROM tblbooksinformation WHERE Accession = '" + ID + "'");
                                if (bookList.rowAffected > 0)
                                {
                                    // Do something if the deletion is successful
                                    bookList.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Collection','"+ ID + " deleted in collection table')");
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
                MessageBox.Show(error.Message, "Error on Delete List!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                autoRefresh();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                autoRefresh();
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPage = 1; // reset the current page when a new type is selected
            autoRefresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, ImageText;
                Accession = checkedRow.Cells[1].Value.ToString();
                Title = checkedRow.Cells[2].Value.ToString();
                Author = checkedRow.Cells[3].Value.ToString();
                Publisher = checkedRow.Cells[4].Value.ToString();
                Language = checkedRow.Cells[5].Value.ToString();
                DatePublish = checkedRow.Cells[6].Value.ToString();
                Subject = checkedRow.Cells[7].Value.ToString();
                Type = checkedRow.Cells[8].Value.ToString();
                Department = checkedRow.Cells[9].Value.ToString();
                Location = checkedRow.Cells[10].Value.ToString();
                Status = checkedRow.Cells[11].Value.ToString();
                Quantity = checkedRow.Cells[12].Value.ToString();
                PageNumber = checkedRow.Cells[13].Value.ToString();
                ImageText = checkedRow.Cells[14].Value.ToString();
                frmUpdateMaterial viewBooks  = new frmUpdateMaterial(Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, ImageText);
                viewBooks.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to view.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }   
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int startIndex = (currentPage - 1) * pageSize;
                DataTable dt = bookList.GetData($"SELECT Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, Image FROM tblbooksinformation WHERE Type != '{username}' AND (Accession LIKE '%{txtSearch.Text}%' OR Title LIKE '%{txtSearch.Text}%' OR Author LIKE '%{txtSearch.Text}%' OR Subject LIKE '%{txtSearch.Text}%' OR Publisher LIKE '%{txtSearch.Text}%') ORDER BY Accession LIMIT {startIndex}, {pageSize}");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[12].Visible = false;
                dataGridView1.Columns[13].Visible = false;
                dataGridView1.Columns[14].Visible = false;

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                int recordCount = Convert.ToInt32(bookList.GetData($"SELECT COUNT(*) FROM tblbooksinformation").Rows[0][0]);
                totalPages = (int)Math.Ceiling((double)recordCount / pageSize);

                lblPageInfo.Text = $"Page {currentPage} of {totalPages}";
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Books info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
