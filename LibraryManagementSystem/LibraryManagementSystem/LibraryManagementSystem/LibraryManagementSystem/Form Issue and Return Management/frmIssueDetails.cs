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
    public partial class frmIssueDetails : Form
    {
        string Library_ID, User, Title, Accession, Issue_Date, Expected_Return, Date_Return, Status;

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public frmIssueDetails(string Library_ID, string User, string Title, string Accession, string Issue_Date, string Expected_Return, string Date_Return, string Status)
        {
            InitializeComponent();
            this.Library_ID = Library_ID;
            this.User = User;
            this.Title = Title;
            this.Accession = Accession;
            this.Issue_Date = Issue_Date;
            this.Expected_Return = Expected_Return;
            this.Date_Return = Date_Return;
            this.Status = Status;
        }
        private void frmIssueDetails_Load(object sender, EventArgs e)
        {
            txtLibraryID.Text = Library_ID;
            txtUser.Text = User;
            txtTitle.Text = Title;
            txtAccession.Text = Accession;
            txtIssueDate.Text = Issue_Date;
            txtExpectedReturn.Text = Expected_Return;
            txtDateReturn.Text = Date_Return;
            txtStatus.Text = Status;
        }
    }
}
