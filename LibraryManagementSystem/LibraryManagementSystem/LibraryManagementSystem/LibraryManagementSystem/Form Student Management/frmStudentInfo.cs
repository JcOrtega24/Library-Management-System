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
    public partial class frmStudentInfo : Form
    {
        private string ID_Number, Password, Student_Name, Library_Access_ID, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status;

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmStudentInfo(string ID_Number, string Password, string Student_Name, string Library_Access_ID, string Course, string Email_ID, string Mobile_Number, string Reg_Date, string dateApproved, string approvedBy, string Status)
        {
            InitializeComponent();
            this.ID_Number = ID_Number;
            this.Password = Password;
            this.Student_Name = Student_Name;
            this.Library_Access_ID = Library_Access_ID;
            this.Course = Course;
            this.Email_ID = Email_ID;
            this.Mobile_Number = Mobile_Number;
            this.Reg_Date = Reg_Date;
            this.dateApproved = dateApproved;
            this.approvedBy = approvedBy;
            this.Status = Status;
        }
        private void frmStudentInfo_Load(object sender, EventArgs e)
        {
            txtStudID.Text = ID_Number;
            txtPass.Text = Password;
            txtLibID.Text = Library_Access_ID;
            txtFullName.Text = Student_Name;
            txtLibID.Text = Library_Access_ID;
            txtCourse.Text = Course;
            txtEmailID.Text = Email_ID;
            txtMobileNumber.Text = Mobile_Number;
            txtRegDate.Text = Reg_Date;            txtDateApproved.Text = dateApproved;            txtApprovedBy.Text = approvedBy;            txtStatus.Text = Status;        }
    }
}
