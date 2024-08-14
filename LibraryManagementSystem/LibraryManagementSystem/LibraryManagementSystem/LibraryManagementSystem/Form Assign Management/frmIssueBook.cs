using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class frmIssueBook : Form
    {
        private DataTable dt;
        private int errorCount;
        int duration;
        private List<Book> books;
        string Accession;
        string Title;

        public frmIssueBook(List<Book> books)
        {
            InitializeComponent();
            this.books = books;
            string accessionNumbers = ""; // create an empty string to hold the accession numbers       
            foreach (Book book in books)
            {
                accessionNumbers += book.Accession + ", "; // concatenate the Accession number of each book to the string
            }
            dataGridView1.DataSource = books;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
  
        }

        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", ""); 

        private void PopulateUser()
        {
            //User
            try
            {
                dt = bookList.GetData("SELECT * FROM tbluseraccount WHERE Status = 'APPROVED' ORDER BY Student_Name");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string Student_Name = dt.Rows[i]["Student_Name"].ToString();
                        cmbUser.Items.Add(Student_Name);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Book Information Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmIssueBook_Load(object sender, EventArgs e)
        {
            PopulateUser();      
        }

        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedUser = cmbUser.SelectedItem.ToString();
                dt = bookList.GetData("SELECT * FROM tbluseraccount WHERE Student_Name = '" + selectedUser + "'");

                if (dt.Rows.Count > 0)
                {
                    string Library_Access_ID = dt.Rows[0]["Library_Access_ID"].ToString();
                    lblLibraryID.Text = Library_Access_ID;
                    string userType = dt.Rows[0]["userType"].ToString();
                    txtUsertype.Text = userType;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Book Information Selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void validateUser()
        {
            string user = cmbUser.Text;
            foreach (Book book in books)
            {
                string accession = book.Accession;

                // Check if the specified user has already borrowed the book with the given accession
                string sql = "SELECT COUNT(*) FROM tblbookassign WHERE User = '" + user + "' AND Accession = '" + accession + "' AND Status = 'OUT'";
                DataTable result = bookList.GetData(sql);
                int count = Convert.ToInt32(result.Rows[0][0]);

                if (count > 0)
                {
                    MessageBox.Show("User has already borrowed book with accession " + accession, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    errorCount++;
                }
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

        private ucIssueManagement issueManagementForm = new ucIssueManagement(); // Create an instance of ucIssueManagement

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUsertype.Text == "College Student")
            {
                // Define the duration in days
                duration = 7;
            }
            else
            {
                // Define the duration in days
                duration = 14;
            }

            // Get the current date
            DateTime now = DateTime.Now;

            validateUser();
            ErrorCounts();
            if (errorCount == 0)
            {
                try
                {
                    List<string> existingAccessions = new List<string>(); // Initialize list for existing accessions
                    StringBuilder sb = new StringBuilder();
                    foreach (Book book in books)
                    {
                        // Check for existing accession for the specific user
                        DataTable dt = bookList.GetData("SELECT * FROM tblbookassign WHERE Accession = '" + book.Accession + "' AND User = '" + lblLibraryID.Text + "' AND Status = 'OUT'");
                        if (dt.Rows.Count > 0)
                        {
                            existingAccessions.Add(book.Accession); // Add existing accession to list
                        }
                        else
                        {
                            // Append each row to the SQL string
                            sb.Append("INSERT INTO tblbookassign(Library_ID, User, Accession, Title, Issue_Date, Expected_Return, Status) VALUES('");
                            sb.Append(lblLibraryID.Text);
                            sb.Append("', '");
                            sb.Append(cmbUser.Text);
                            sb.Append("', '");
                            sb.Append(book.Accession);
                            sb.Append("', '");
                            sb.Append(book.Title);
                            sb.Append("', '");
                            sb.Append(now.ToString("dddd, dd MMMM yyyy"));
                            sb.Append("', '");
                            sb.Append(now.AddDays(duration).ToString("dddd, dd MMMM yyyy"));
                            sb.Append("', 'OUT'); ");

                            // Append the UPDATE statement to the SQL string
                            sb.Append("UPDATE tblbooksinformation SET Available = Available - 1 WHERE Accession = '");
                            sb.Append(book.Accession);
                            sb.Append("'; ");
                        }
                    }

                    // Check for existing accessions and display message for each one
                    if (existingAccessions.Count > 0)
                    {
                        string message = "The following accessions already borrowed by the user " + lblLibraryID.Text + ":\n";
                        foreach (string accession in existingAccessions)
                        {
                            message += "- " + accession + "\n";
                        }
                        message += "Please return these books from the list and try again.";
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DialogResult answer = MessageBox.Show("Are you sure you want to issue this book?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            // Execute the SQL string
                            bookList.executeSQL(sb.ToString());

                            if (bookList.rowAffected > 0)
                            {
                                MessageBox.Show("Books Issued!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                bookList.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Request On-site','" + cmbUser.Text + " issued a book')");
                                frmIssueManagements newFrm = (frmIssueManagements)Application.OpenForms["frmIssueManagements"];
                                newFrm.autoRefresh();
                                this.Close();
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Books Issued!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                errorProvider1.SetError(cmbUser, "Input must be text only");
            }
            else
            {
                errorProvider1.Clear();
            }
        }
    }
}
