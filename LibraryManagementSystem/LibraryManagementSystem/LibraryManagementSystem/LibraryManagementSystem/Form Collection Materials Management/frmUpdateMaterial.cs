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
    public partial class frmUpdateMaterial : Form
    {
        string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, Image;
        string imagePath;
        public frmUpdateMaterial(string Accession, string Title, string Author, string Publisher, string Language, string DatePublish, string Subject, string Type, string Department, string Location, string Status, string Quantity, string PageNumber, string Image)
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
            this.Location = Location;
            this.Status = Status;
            this.Quantity = Quantity;
            this.PageNumber = PageNumber;
            this.Image = Image;
        }
        Library1 Books = new Library1("localhost", "librarymanagementstudent", "root", "");

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

        private void frmUpdateMaterial_Load(object sender, EventArgs e)
        {
            txtAccession.Text = Accession;
            txtTitle.Text = Title;
            cmbAuthor.Text = Author;
            cmbPublisher.Text = Publisher;
            cmbLanguage.Text = Language;
            cmbSubject.Text = Subject;
            cmbType.Text = Type;
            cmbDepartment.Text = Department;           
            ImageText.Text = Image;
            string imagePath = Path.Combine(Application.StartupPath, "Image", Image);
            Bitmap image = new Bitmap(imagePath);
            pictureBox1.Image = image;

            //setting combobox
            //Author
            try
            {
                bool authordone = false;
                DataTable dt = Books.GetData("SELECT * FROM author");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string author;
                        author = dt.Rows[i]["Name"].ToString();
                        cmbAuthor.Items.Add(author);
                    }
                    authordone = true;
                }

                //setting the update data in the combobox
                if (dt.Rows.Count > 0 && authordone == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Name"].ToString() == Author)
                        {
                            cmbAuthor.SelectedIndex = i;
                        }
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
                bool publisherdone = false;
                DataTable dt = Books.GetData("SELECT * FROM publisher");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string publisher;
                        publisher = dt.Rows[i]["Name"].ToString();
                        cmbPublisher.Items.Add(publisher);
                    }
                    publisherdone = true;
                }

                //setting the update data in the combobox
                if (dt.Rows.Count > 0 && publisherdone == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Name"].ToString() == Publisher)
                        {
                            cmbPublisher.SelectedIndex = i;
                        }
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
                bool categorydone = false;
                DataTable dt = Books.GetData("SELECT * FROM category");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string category;
                        category = dt.Rows[i]["Category"].ToString();
                        cmbType.Items.Add(category);
                    }
                    categorydone = true;
                }

                //setting the update data in the combobox
                if (dt.Rows.Count > 0 && categorydone == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Category"].ToString() == Type)
                        {
                            cmbType.SelectedIndex = i;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Category Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Location
            try
            {
                bool locationdone = false;
                DataTable dt = Books.GetData("SELECT * FROM location");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string locationdt;
                        locationdt = dt.Rows[i]["Location"].ToString();
                        cmbLocation.Items.Add(locationdt);
                    }
                    locationdone = true;
                }

                //setting the update data in the combobox
                if (dt.Rows.Count > 0 && locationdone == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Location"].ToString() == Location)
                        {
                            cmbLocation.SelectedIndex = i;
                        }
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
                bool departmentdone = false;
                DataTable dt = Books.GetData("SELECT * FROM department");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string department;
                        department = dt.Rows[i]["Department"].ToString();
                        cmbDepartment.Items.Add(department);
                    }
                    departmentdone = true;
                }

                //setting the update data in the combobox
                if (dt.Rows.Count > 0 && departmentdone == true)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Department"].ToString() == Department)
                        {
                            cmbDepartment.SelectedIndex = i;
                        }
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
        private void btnSave_Click(object sender, EventArgs e)
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
                        Books.executeSQL("INSERT INTO tblbooksinformation (Image, Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity , PageNumber) VALUES('" + Path.GetFileName(pictureBox1.ImageLocation) + "', '"
                            + Accession + "', '" + txtTitle.Text + "', '" + cmbAuthor.Text + "', '" + cmbPublisher.Text + "', '" + cmbLanguage.Text + "', '" + dtpDatePublish.Value.Date.ToString("yyyy-MM-dd") + "', '" + cmbSubject.Text + "', '" +
                            cmbType.Text + "', '" + cmbDepartment.Text + "', '" + cmbLocation.Text + "', 'Available', '" + nudQuantity.Text + "', '" + nudPages.Text + "') ");
                        if (Books.rowAffected > 0)
                        {
                            MessageBox.Show("New Book added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmCollectionManagement newFrm = (frmCollectionManagement)Application.OpenForms["frmCollectionManagement"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Update new Material!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
