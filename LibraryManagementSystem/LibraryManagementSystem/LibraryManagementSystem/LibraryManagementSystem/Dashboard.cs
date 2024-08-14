using LibraryManagementSystem.Form_Categoy_Management;
using LibraryManagementSystem.Form_Department_Management;
using LibraryManagementSystem.Form_Location_Management;
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
    public partial class Dashboard : Form
    {
        private string username, usertype;
        public Dashboard(string username, string usertype)
        {
            InitializeComponent();
            label1.Text = usertype;         
            this.username = username;
            this.usertype = usertype;
        }
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        public void autoRefreshMaterialList()
        {
            /*try
            {
                var queryAvailable = "SELECT COUNT(*) FROM tblbooksinformation WHERE Status = 'Available'";
                var availableTables = Convert.ToInt32(bookList.GetData(queryAvailable).Rows[0][0]);
                lblMaterialRecord.Text = availableTables.ToString();

                var queryAssign = "SELECT COUNT(*) FROM tblbookassign";
                var assignTable = Convert.ToInt32(bookList.GetData(queryAssign).Rows[0][0]);
                lblBorrowed.Text = assignTable.ToString();
                lblborrowed1.Text = assignTable.ToString();

                this.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on  table list!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }
        private void button5_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Close();
            customizeDesign();
        }
        private void customizeDesign()
        {
            panelSubMenu.Visible = false;
            panelSubMenuIssue.Visible = false;
        }
        private void hideSubMenu()
        {
            if(panelSubMenu.Visible == true)
                panelSubMenu.Visible = false;
            if(panelSubMenuIssue.Visible == true)
                panelSubMenuIssue.Visible = false;
        }
        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenu);
        }
        private void btnIssueManagement_Click(object sender, EventArgs e)
        {
            showSubMenu(panelSubMenuIssue);
        }

        public void loadForm(object Form)
        {
            if (this.panelContent.Controls.Count > 0)
                this.panelContent.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelContent.Controls.Add(f);
            this.panelContent.Tag = f;
            f.Show();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadForm(new frmDashboardDetails());
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            loadForm(new frmCollectionManagement());
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            loadForm(new frmDashboardDetails());
        }

        private void btnStudents_Click(object sender, EventArgs e)
        {
            loadForm(new frmUserManagement());
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            loadForm(new frmAuthorList());
        }

        private void btnPublisher_Click(object sender, EventArgs e)
        {
            loadForm(new frmPublisherList());
        }

        private void btnRakLocation_Click(object sender, EventArgs e)
        {
            loadForm(new frmLocationList());
        }

        private void btnIssuedList_Click(object sender, EventArgs e)
        {
            loadForm(new frmManageIssueList());
        }

        private void btnIssueMaterial_Click(object sender, EventArgs e)
        {
            loadForm(new frmIssueManagements());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            loadForm(new frmCategoryList());
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            loadForm(new frmDepartmentList());
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            loadForm(new frmUserManagement());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {

        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            loadForm(new frmRequestList());
        }
    }
}
