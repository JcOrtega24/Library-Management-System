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
    public partial class ucStudentManagement : UserControl
    {
        private string username = "ADMIN - CIRCULATION";
        int currentPage = 1;
        int rowsPerPage = 10;
        public ucStudentManagement()
        {
            InitializeComponent();
        }
        Library1 student = new Library1("localhost", "librarymanagementstudent", "root", "");
        public void autoRefresh()
        {
            int offset = (currentPage - 1) * rowsPerPage;
            try
            {
                DataTable dt = student.GetData("SELECT ID_Number, Password, Library_Access_ID, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status, Image FROM tbluseraccount WHERE ID_Number <> '" + username +
                    "'AND(ID_Number LIKE '%" + txtSearch.Text + "%' OR Library_Access_ID LIKE '%" + txtSearch.Text + "%' OR Student_Name LIKE '%" + txtSearch.Text + "%' OR Course LIKE '%" + txtSearch.Text + "%'  OR Status LIKE '%" + txtSearch.Text + "%') ORDER BY Reg_Date ASC LIMIT " + offset + ", " + rowsPerPage);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[6].Visible = false;
                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[11].Visible = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Student info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void studentManagement_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string ID_Number, Password, Student_Name, Library_Access_ID, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status;
            ID_Number = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
            Password = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
            Library_Access_ID = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
            Student_Name = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();            
            Course = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
            Email_ID = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
            Mobile_Number = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString();
            Reg_Date = dataGridView1.Rows[rowSelected].Cells[7].Value.ToString();
            dateApproved = dataGridView1.Rows[rowSelected].Cells[8].Value.ToString();
            approvedBy = dataGridView1.Rows[rowSelected].Cells[9].Value.ToString();
            Status = dataGridView1.Rows[rowSelected].Cells[10].Value.ToString();
            frmStudentInfo viewStudent = new frmStudentInfo(ID_Number, Password, Student_Name, Library_Access_ID, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, approvedBy, Status);
            viewStudent.Show();
        }
        private int rowSelected;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            rowSelected = dataGridView1.CurrentCell.RowIndex;
            (sender as DataGridView).CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            string Status = dataGridView1.Rows[rowSelected].Cells[10].Value.ToString();
            if (Status == "WAITING FOR APPROVAL")
            {
                string ID_Number, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved;
                ID_Number = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                Student_Name = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
                Course = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
                Email_ID = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
                Mobile_Number = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString();
                Reg_Date = dataGridView1.Rows[rowSelected].Cells[7].Value.ToString();
                dateApproved = dataGridView1.Rows[rowSelected].Cells[8].Value.ToString();
                Status = dataGridView1.Rows[rowSelected].Cells[10].Value.ToString();
                frmApproveStudent approveStudent = new frmApproveStudent(ID_Number, Student_Name, Course, Email_ID, Mobile_Number, Reg_Date, dateApproved, Status, username);
                approveStudent.Show();
            }
            else
            {
                MessageBox.Show("Status must be WAITING FOR APPROVAL!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            string Email_Id;
            Email_Id = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
            frmEmail mail = new frmEmail(Email_Id);
            mail.Show();
        }

        private void btnLabelBack_Click(object sender, EventArgs e)
        {
            currentPage--;
            if (currentPage < 1)
                currentPage = 1;
            autoRefresh();
        }

        private void btnLabelNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            autoRefresh();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            autoRefresh();
        }
    }
}
