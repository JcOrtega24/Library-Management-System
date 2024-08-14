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
    public partial class frmAssignMaterials : Form
    {
        private string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Locations, Status, Quantity, PageNumber, ImageText;
        private DataTable dt;
        string imagePath;
        public frmAssignMaterials(string Accession, string Title, string Author, string Publisher, string Language, string DatePublish, string Subject, string Type, string Department, string Location, string Status, string Quantity, string PageNumber, string ImageText)
        {
            InitializeComponent();
            this.Accession = Accession;
            this.Title = Title;
            this.Author = Author;
            this.Publisher = Publisher;
            this.Language = Language;
            this.DatePublish = DatePublish;
            this.Subject = Subject;
            this.Type = Type;
            this.Department = Department;
            this.Locations = Location;
            this.Status = Status;
            this.Quantity = Quantity;
            this.PageNumber = PageNumber;
            this.ImageText = ImageText;
        }
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        private void frmAssignMaterials_Load(object sender, EventArgs e)
        {
            //User
            try
            {
                dt = bookList.GetData("SELECT * FROM tbluseraccount");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string studentName = dt.Rows[i]["Student_Name"].ToString();
                        cmbUser.Items.Add(studentName);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Category Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbUser.Text = "Select User";
            try
            {
                txtAccession.Text = Accession;
                txtTitle.Text = Title;
                txtAuthor.Text = Author;
                txtPublisher.Text = Publisher;
                txtLanguage.Text = Language;
                txtSubject.Text = Subject;
                txtCategory.Text = Type;
                txtDepartment.Text = Department;
                Bitmap image = new Bitmap(imagePath = @"C:\Users\charl\Desktop\LibraryManagementSystem\LibraryManagementSystem\LibraryManagementSystem\bin\Debug\Image\" + ImageText);
                pictureBox1.Image = image;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Book Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void validateAssign()
        {
            if (cmbUser.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbUser, "User is Required");
            }
            else
            {
                errorProvider1.SetError(cmbUser, "");
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
        private void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected student name
            string selectedStudentName = cmbUser.SelectedItem.ToString();

            // Find the row in the data table that matches the selected student name
            DataRow[] matchingRows = dt.Select("Student_Name = '" + selectedStudentName + "'");

            if (matchingRows.Length > 0)
            {
                // If a matching row is found, update the txtLibraryAccessID text box with the corresponding library access ID
                string libraryAccessID = matchingRows[0]["Library_Access_ID"].ToString();
                txtLibraryAccessID.Text = libraryAccessID;
                // If a matching row is found, update the txtLibraryAccessID text box with the corresponding library access ID
                string userType = matchingRows[0]["userType"].ToString();
                txtUserType.Text = userType;
            }
        }
        int duration;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserType.Text == "College Student")
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

            // Calculate the deadline date by adding the duration to the current date
            DateTime deadline = now.AddDays(duration);
            validateAssign();
            ErrorCounts();
            if (errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to issue this book?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        bookList.executeSQL("INSERT INTO tblbookassign(Library_ID, User, Accession, Title, Issue_Date, Expected_Return, Status) VALUES('" + txtLibraryAccessID.Text + "', '"
                            + cmbUser.Text + "', '" + txtAccession.Text + "', '" + txtTitle.Text + "', '" + DateTime.Now.ToString("dddd, dd MMMM yyyy") + "', '" + deadline.ToString("dddd, dd MMMM yyyy") + "', 'OUT') ");
                        if (bookList.rowAffected > 0)
                        {
                            MessageBox.Show("Books Issued!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            ucAssignMaterial ucAssignMaterial = new ucAssignMaterial();
                            ucAssignMaterial.autoRefresh();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Books Issued!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }        
    }
}
