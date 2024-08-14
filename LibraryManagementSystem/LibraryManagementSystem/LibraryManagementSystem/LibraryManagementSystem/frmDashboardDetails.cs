using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class frmDashboardDetails : Form
    {
        Library1 dashboard = new Library1("localhost", "librarymanagementstudent", "root", "");

        public frmDashboardDetails()
        {
            InitializeComponent();
        }

        public void loadAccount()
        {
            try
            {
                DataTable dt = dashboard.GetData("SELECT * from tbluseraccount");
                int accountCount = dt.Rows.Count;

                lblaccountCount.Text = accountCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadBooks()
        {
            try
            {
                int totalBooks = 0;
                int availableBooks = 0;
                DataTable dt = dashboard.GetData("SELECT Quantity from tblbooksinformation");
                
                foreach (DataRow dr in dt.Rows)
                {
                    totalBooks += Convert.ToInt32(dr["Quantity"].ToString());
                }

                DataTable dt2 = dashboard.GetData("SELECT Available from tblbooksinformation");

                foreach (DataRow dr2 in dt2.Rows)
                {
                    availableBooks += Convert.ToInt32(dr2["Available"].ToString());
                }

                lblbookstotal.Text = totalBooks.ToString();
                lblbooksavailable.Text = availableBooks.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load books", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadIssued()
        {
            try
            {
                DataTable dt = dashboard.GetData("SELECT * from tblbookassign");
                int issuedCount = dt.Rows.Count;

                lblissuecount.Text = issuedCount.ToString();

                DataTable dr = dashboard.GetData("SELECT * from tblbookassign WHERE Status = 'OUT'");
                int outCount = dr.Rows.Count;

                lblissueout.Text = outCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load account", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDashboardDetails_Load(object sender, EventArgs e)
        {
            loadAccount();
            loadBooks();
            loadIssued();
        }
    }
}
