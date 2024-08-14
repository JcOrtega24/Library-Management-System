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
    public partial class frmApproveStudent : Form
    {
        private string ID_Number, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, Status, approvedBy;
        private string libraryID = "11-" + DateTime.Now.ToString("MMddyyyyHHmmss");
        public frmApproveStudent(string ID_Number, string Student_Name, string Course, string Email_ID, string Mobile_Number, string Reg_Date, string dateApproved, string Status, string approvedBy)
        {
            InitializeComponent();
            this.ID_Number = ID_Number;
            this.Student_Name = Student_Name;
            this.Course = Course;
            this.Email_ID = Email_ID;
            this.Mobile_Number = Mobile_Number;
            this.Reg_Date = Reg_Date;
            this.dateApproved = dateApproved;
            this.Status = Status;
            this.approvedBy = approvedBy;
        }
        Library1 student = new Library1("localhost", "librarymanagementstudent", "root", "");
        private void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to approved this Student?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.Yes)
                {
                    student.executeSQL("UPDATE tbluseraccount SET Library_Access_ID = '" + libraryID + "', Status = 'APPROVED', approvedBy = '" + approvedBy + "', dateApproved = '" + DateTime.Now.ToString("dd-MM-yyyy") + "' WHERE ID_Number = '" + txtStudID.Text + "' ");
                    if (student.rowAffected > 0)
                    {
                        MessageBox.Show("Student Approved!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmUserManagement newFrm = (frmUserManagement)Application.OpenForms["frmUserManagement"];
                        newFrm.autoRefresh();
                        this.Close();
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Approved Student!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        private void frmApproveStudent_Load(object sender, EventArgs e)
        {
            txtStudID.Text = ID_Number;
            txtFullName.Text = Student_Name;
            txtCourse.Text = Course;
            txtEmailID.Text = Email_ID;
            txtMobileNumber.Text = Mobile_Number;
            txtRegDate.Text = Reg_Date;
            txtStatus.Text = Status;
        }
    }
}
