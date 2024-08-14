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
using System.Xml.Linq;

namespace LibraryManagementSystem.Form_Categoy_Management
{
    public partial class frmAddCategory : Form
    {
        Library1 category = new Library1("localhost", "librarymanagementstudent", "root", "");
       
        public frmAddCategory()
        {
            InitializeComponent();
        }

        private void validateCategory()
        {
            DataTable dt = category.GetData("SELECT * FROM category WHERE Category = '" + txtCategory.Text + "'");
            if (txtCategory.Text == "")
            {
                errorProvider1.SetError(txtCategory, "Category is Required");
            }
            else if (dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtCategory, "Category should be Unique");
            }
            else
            {
                errorProvider1.SetError(txtCategory, "");

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
            validateCategory();
            ErrorCounts();
            if (errorCount == 0)
            {
                try
                {
                    DialogResult answer = MessageBox.Show("Are you sure you want to save this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        category.executeSQL("INSERT INTO category (Category) VALUES('" + txtCategory.Text + "') ");
                        if (category.rowAffected > 0)
                        {
                            MessageBox.Show("New Category added!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            category.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Category','" + txtCategory.Text + " added in category')");
                            frmCategoryList newFrm = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Add new Category!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCategory.Clear();
        }

        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
