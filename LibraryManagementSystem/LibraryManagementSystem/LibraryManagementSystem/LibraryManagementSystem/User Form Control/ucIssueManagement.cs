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
    public partial class ucIssueManagement : UserControl
    {
        public ucIssueManagement()
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
                DataTable dt = bookList.GetData("SELECT Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Available, PageNumber FROM tblbooksinformation WHERE Type = 'Books' AND Status = 'Available' AND Available <> 0 AND(Accession LIKE '%" + txtSearch.Text + "%' OR Title LIKE '%" + txtSearch.Text + "%' OR Author LIKE '%" + txtSearch.Text + "%' OR Subject LIKE '%" + txtSearch.Text + "%'  OR Publisher LIKE '%" + txtSearch.Text + "%') ORDER BY Accession LIMIT " + offset + ", " + rowsPerPage);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[10].Visible = false;
                dataGridView1.Columns[11].Visible = false;
                dataGridView1.Columns[13].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Books info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void ucIssueManagement_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation code goes here
                bool isValid = true;
                List<Book> selectedBooks = new List<Book>(); // create a list to store selected books
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewCheckBoxCell checkbox = row.Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                    if (checkbox.Value != null && (bool)checkbox.Value == true)
                    {
                        string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status;
                        int Quantity, PageNumber;
                        Accession = row.Cells[1].Value.ToString();
                        Title = row.Cells[2].Value.ToString();
                        Author = row.Cells[3].Value.ToString();
                        Publisher = row.Cells[4].Value.ToString();
                        Language = row.Cells[5].Value.ToString();
                        DatePublish = row.Cells[6].Value.ToString();
                        Subject = row.Cells[7].Value.ToString();
                        Type = row.Cells[8].Value.ToString();
                        Department = row.Cells[9].Value.ToString();
                        Location = row.Cells[10].Value.ToString();
                        Status = row.Cells[11].Value.ToString();
                        Quantity = int.Parse(row.Cells[12].Value.ToString());
                        PageNumber = int.Parse(row.Cells[13].Value.ToString());
                        Book book = new Book(Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber);
                        selectedBooks.Add(book); // add selected book to the list
                    }
                }
                if (isValid && selectedBooks.Count > 0) // check if at least one checkbox is checked
                {
                    // Approval code goes here
                    frmIssueBook issueBook = new frmIssueBook(selectedBooks);
                    issueBook.Show();
                }
                else if (selectedBooks.Count == 0) // display error message if no checkbox is checked
                {
                    MessageBox.Show("Please select at least one book to issue!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Book Issue!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void StartTimer()
        {
            timer2.Start();
            autoRefresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //autoRefresh();
        }
    }
}
