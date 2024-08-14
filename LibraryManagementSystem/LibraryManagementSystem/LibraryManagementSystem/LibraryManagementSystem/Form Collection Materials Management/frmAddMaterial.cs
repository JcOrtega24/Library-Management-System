using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LibraryManagementSystem
{
    public partial class frmAddMaterial : Form
    {
        string username;
        string Accession = DateTime.Now.ToString("yyyyMMddhhmmss");
        public frmAddMaterial(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        Library1 Books = new Library1("localhost", "librarymanagementstudent", "root", "");
        private void frmAddBooks_Load(object sender, EventArgs e)
        {
            //Author
            try
            {
                DataTable dt = Books.GetData("SELECT * FROM author");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string author;
                        author = dt.Rows[i]["Name"].ToString();
                        cmbAuthor.Items.Add(author);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Author Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Publisher
            try
            {
                DataTable dt = Books.GetData("SELECT * FROM publisher");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string publisher;
                        publisher = dt.Rows[i]["Name"].ToString();
                        cmbPublisher.Items.Add(publisher);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on publisher Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Category
            try
            {
                DataTable dt = Books.GetData("SELECT * FROM category");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string category;
                        category = dt.Rows[i]["Category"].ToString();
                        cmbType.Items.Add(category);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Category Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            txtAccession.Text = Accession;
            //Location
            try
            {
                DataTable dt = Books.GetData("SELECT * FROM location");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string location;
                        location = dt.Rows[i]["Location"].ToString();
                        cmbLocation.Items.Add(location);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on location Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Department
            try
            {
                DataTable dt = Books.GetData("SELECT * FROM department");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string department;
                        department = dt.Rows[i]["Department"].ToString();
                        cmbDepartment.Items.Add(department);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on department Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void validateAccession()
        {
            if (txtAccession.Text == "")
            {
                errorProvider1.SetError(txtAccession, "Accession is Required");
            }
            else
            {
                errorProvider1.SetError(txtAccession, "");

            }
        }
        private void validateTitle()
        {
            if (txtTitle.Text == "")
            {
                errorProvider1.SetError(txtTitle, "Title is Required");
            }
            else
            {
                errorProvider1.SetError(txtTitle, "");

            }
        }
        private void validateAuthor()
        {
            if (cmbAuthor.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbAuthor, "Author is Required");
            }
            else
            {
                errorProvider1.SetError(cmbAuthor, "");
            }
        }
        private void validatePublisher()
        {
            if (cmbPublisher.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbPublisher, "Publisher is Required");
            }
            else
            {
                errorProvider1.SetError(cmbPublisher, "");
            }
        }
        private void validateLanguage()
        {
            if (cmbLanguage.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbLanguage, "Language is Required");
            }
            else
            {
                errorProvider1.SetError(cmbLanguage, "");
            }
        }
        private void validateSubject()
        {
            if (cmbSubject.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbSubject, "Subject is Required");
            }
            else
            {
                errorProvider1.SetError(cmbSubject, "");
            }
        }
        private void validateType()
        {
            if (cmbType.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbType, "Type is Required");
            }
            else
            {
                errorProvider1.SetError(cmbType, "");
            }
        }
        private void validateDepartment()
        {
            if (cmbDepartment.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbDepartment, "Department is Required");
            }
            else
            {
                errorProvider1.SetError(cmbDepartment, "");
            }
        }
        private void validateLocation()
        {
            if (cmbLocation.SelectedIndex < 0)
            {
                errorProvider1.SetError(cmbLocation, "Location is Required");
            }
            else
            {
                errorProvider1.SetError(cmbLocation, "");
            }
        }
        private void pictureBox()
        {
            if (pictureBox1.Image == null)
            {
                errorProvider1.SetError(pictureBox1, "Please select an image.");
            }
            else
            {
                errorProvider1.SetError(pictureBox1, "");
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
        private void button2_Click(object sender, EventArgs e)
        {
            validateAccession();
            validateTitle();
            validateAuthor();
            validatePublisher();
            validateLanguage();
            validateSubject();
            validateType();          
            validateDepartment();
            validateLocation();
            pictureBox();
            ErrorCounts();
            if (errorCount == 0)
            {
                try
                {
                    File.Copy(ImageText.Text, Application.StartupPath + @"\Image\" + Path.GetFileName(pictureBox1.ImageLocation));
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {                      
                        Books.executeSQL("INSERT INTO tblbooksinformation (Image, Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, Available , PageNumber) VALUES('" + Path.GetFileName(pictureBox1.ImageLocation) + "', '"
                            + Accession + "', '" + txtTitle.Text + "', '" + cmbAuthor.Text + "', '" + cmbPublisher.Text + "', '" + cmbLanguage.Text + "', '" + dtpDatePublish.Value.Date.ToString("yyyy-MM-dd") + "', '" + cmbSubject.Text + "', '" +
                            cmbType.Text + "', '" + cmbDepartment.Text + "', '" + cmbLocation.Text + "', 'Available', '" + nudQuantity.Text + "', '" + nudQuantity.Text + "' , '" + nudPages.Text + "') "); 
                        if (Books.rowAffected > 0)
                        {
                            MessageBox.Show("New Book added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Books.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Collection','" + Accession + " added in collection table')");
                            frmCollectionManagement newFrm = (frmCollectionManagement)Application.OpenForms["frmCollectionManagement"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Add new account!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            cmbAuthor.SelectedIndex = -1;
            cmbPublisher.SelectedIndex = -1;
            cmbLanguage.SelectedIndex = -1;
            cmbSubject.SelectedIndex = -1;
            cmbType.SelectedIndex = -1;
            cmbDepartment.SelectedIndex = -1;
            cmbLocation.SelectedIndex = -1;
            nudQuantity.ResetText();
            nudPages.ResetText();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfd = new OpenFileDialog();
            openfd.Filter = "Image Files(*.jpg;*.jpeg;*.gif;) | *.jpg;*.jpeg;*.gif;";
            if (openfd.ShowDialog() == DialogResult.OK)
            {
                ImageText.Text = openfd.FileName;
                pictureBox1.Image = new Bitmap(openfd.FileName);
                pictureBox1.ImageLocation = openfd.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
