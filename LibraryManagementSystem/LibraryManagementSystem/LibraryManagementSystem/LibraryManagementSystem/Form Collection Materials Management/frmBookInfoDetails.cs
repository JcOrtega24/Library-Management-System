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
using System.Drawing.Imaging;

namespace LibraryManagementSystem
{
    public partial class frmBookInfoDetails : Form
    {
        private string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Locations, Status, Quantity, PageNumber, ImageText;
        string imagePath;

        public frmBookInfoDetails(string Accession, string Title, string Author, string Publisher, string Language, string DatePublish, string Subject, string Type, string Department, string Location, string Status, string Quantity, string PageNumber, string ImageText)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBookInfoDetails_Load(object sender, EventArgs e)
        {
            try
            {
                lblTitleHeader.Text = Title;
                lblPublishHeader.Text = Publisher;
                lblPublishDateeader.Text = DatePublish;
                lblAuthorLabel.Text = Author;
                lblAccession.Text = Accession;
                lblTitle.Text = Title;
                lblAuthor.Text = Author;
                lblPublisher.Text = Publisher;
                lblLanguage.Text = Language;
                lblDatePublish.Text = DatePublish;
                lblSubject.Text = Subject;
                lblType.Text = Type;
                lblDepartment.Text = Department;
                lblLocation.Text = Locations;
                lblStatus.Text = Status;
                lblQuantity.Text = Quantity;
                lblPageNumber.Text = ImageText;
                Bitmap image = new Bitmap(imagePath = @"C:\Users\pollyana\Desktop\LibraryManagementSystem\LibraryManagementSystem\LibraryManagementSystem\LibraryManagementSystem\bin\Debug\Image\" + ImageText);
                pictureBox1.Image = image;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Error on Book Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
    }
}
