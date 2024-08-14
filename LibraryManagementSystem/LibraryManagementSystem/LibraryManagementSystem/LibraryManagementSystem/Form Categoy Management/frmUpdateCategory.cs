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

namespace LibraryManagementSystem.Form_Categoy_Management
{
    public partial class frmUpdateCategory : Form
    {
        Library1 Category = new Library1("localhost", "librarymanagementstudent", "root", "");
        string ID, category;
        int errorCount;

        public frmUpdateCategory(string ID, string category)
        {
            InitializeComponent();
            this.ID = ID;
            this.category = category;
        }

        private void validateCategory()
        {
            DataTable dt = Category.GetData("SELECT * FROM category WHERE Category = '" + txtCategory.Text + "'");
            if (txtCategory.Text == "")
            {
                errorProvider1.SetError(txtCategory, "Category is Required");
            }
            else if (txtCategory.Text != category && dt.Rows.Count > 0)
            {
                errorProvider1.SetError(txtCategory, "Category should be Unique");
            }
            else
            {
                errorProvider1.SetError(txtCategory, "");
            }
        }

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
                        Category.executeSQL("UPDATE category SET Category = '" + txtCategory.Text + "' WHERE CategoryID = '" + ID + "'");
                        if (Category.rowAffected > 0)
                        {
                            MessageBox.Show("Category Updated!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmCategoryList newFrm = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                            newFrm.autoRefresh();
                            this.Close();
                        }
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Update Category!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCategory.Clear();
        }

        private void frmUpdateCategory_Load(object sender, EventArgs e)
        {
            txtCategory.Text = category;
        }

        private void txtX_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
