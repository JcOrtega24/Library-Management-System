using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace LibraryManagementSystem
{
    public partial class frmEmail : Form
    {
        string Email_id;
        public frmEmail(string Email_id)
        {
            InitializeComponent();
            this.Email_id = Email_id;
        }
        Library1 email = new Library1("localhost", "librarymanagementstudent", "root", "");
        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(txtFrom.Text);
                mail.To.Add(txtTo.Text);
                mail.Subject = txtTitle.Text;
                mail.Body = txtBody.Text;

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(lbLocation.Text);
                mail.Attachments.Add(attachment);

                smtp.Port = 587;
                txtFrom.Text = "renzelle.apolinario@arellano.edu.ph";
                txtPass.Text = "arellano123";
                smtp.Credentials = new System.Net.NetworkCredential(txtFrom.Text, txtPass.Text);
                smtp.EnableSsl = true;
                smtp.Send(mail);
                MessageBox.Show("Mail has been sent successfully!", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmUserManagement newFrm = (frmUserManagement)Application.OpenForms["frmUserManagement"];
                newFrm.autoRefresh();
                this.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Button send Email!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
            lbLocation.Text = openFileDialog1.FileName;
        }

        private void frmEmail_Load(object sender, EventArgs e)
        {
            txtTo.Text = Email_id;
            txtFrom.Text = "renzelle.apolinario@arellano.edu.ph";
            txtPass.Text = "arellano123";
        }
    }
}
