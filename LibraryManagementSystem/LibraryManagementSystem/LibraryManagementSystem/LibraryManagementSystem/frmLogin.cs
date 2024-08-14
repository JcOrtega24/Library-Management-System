using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LibraryManagementSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        Library1 login = new Library1("localhost", "librarymanagementstudent", "root", "");

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //create a datatable containing your SQL SELECT command
                DataTable dt = login.GetData("SELECT * FROM  tblaccounts WHERE userName = '" + TxtUser.Text + "' AND passWord = '" + TxtPass.Text + "'AND userType = 'Librarian'");
                //login successful
                if (dt.Rows.Count > 0)
                {
                    Dashboard main = new Dashboard(TxtUser.Text, dt.Rows[0].Field<string>("userType"));
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username or password or usertype is not Librarian ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on login button!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar(e.KeyChar) == 13)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = true;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            TxtPass.UseSystemPasswordChar = false;
        }

        private void TxtPass_Click(object sender, EventArgs e)
        {
            TxtPass.BackColor = Color.White;
            panel4.BackColor = Color.White;
            TxtUser.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
        }

        private void TxtUser_Click(object sender, EventArgs e)
        {
            TxtUser.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            TxtPass.BackColor = SystemColors.Control;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            TxtUser.Clear();
            TxtPass.Clear();
        }
    }
}
