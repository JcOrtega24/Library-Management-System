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
using MySqlConnector;

namespace LibraryManagementSystem
{
    public partial class ucListBook : UserControl
    {
        private string username = "ADMIN - CIRCULATION";
        public ucListBook()
        {
            InitializeComponent();
        }
        int currentPage = 1;
        int rowsPerPage = 10;
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        public void autoRefresh()
        {
            if(cmbType.SelectedIndex == 0)
            {
                int offset = (currentPage - 1) * rowsPerPage;
                try
                {
                    DataTable dt = bookList.GetData("SELECT Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity , PageNumber, Image FROM tblbooksinformation WHERE Type <> '" + username + "' AND(Accession LIKE '%" + txtSearch.Text + "%' OR Title LIKE '%" + txtSearch.Text + "%' OR Author LIKE '%" + txtSearch.Text + "%' OR Subject LIKE '%" + txtSearch.Text + "%'  OR Publisher LIKE '%" + txtSearch.Text + "%') ORDER BY Accession LIMIT " + offset + ", " + rowsPerPage);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].Visible = false;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Books info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                int offset = (currentPage - 1) * rowsPerPage;
                try
                {
                    DataTable dt = bookList.GetData("SELECT Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity , PageNumber, Image FROM tblbooksinformation WHERE Type = '" + cmbType.Text + "' AND(Accession LIKE '%" + txtSearch.Text + "%' OR Title LIKE '%" + txtSearch.Text + "%' OR Author LIKE '%" + txtSearch.Text + "%' OR Subject LIKE '%" + txtSearch.Text + "%'  OR Publisher LIKE '%" + txtSearch.Text + "%') ORDER BY Accession LIMIT " + offset + ", " + rowsPerPage);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[4].Visible = false;
                    dataGridView1.Columns[5].Visible = false;
                    dataGridView1.Columns[7].Visible = false;
                    dataGridView1.Columns[9].Visible = false;
                    dataGridView1.Columns[10].Visible = false;
                    dataGridView1.Columns[12].Visible = false;
                    dataGridView1.Columns[13].Visible = false;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error on Books info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }          
        }

        private void listBook_Load(object sender, EventArgs e)
        {
            //Author
            try
            {
                DataTable dt = bookList.GetData("SELECT * FROM category");
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string category;
                        category = dt.Rows[i]["Category"].ToString();
                        cmbType.Items.Add(category);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Category Form Load", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbType.Text = "Select material";
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                string Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, ImageText;
                Accession = dataGridView1.Rows[rowSelected].Cells[0].Value.ToString();
                Title = dataGridView1.Rows[rowSelected].Cells[1].Value.ToString();
                Author = dataGridView1.Rows[rowSelected].Cells[2].Value.ToString();
                Publisher = dataGridView1.Rows[rowSelected].Cells[3].Value.ToString();
                Language = dataGridView1.Rows[rowSelected].Cells[4].Value.ToString();
                DatePublish = dataGridView1.Rows[rowSelected].Cells[5].Value.ToString();
                Subject = dataGridView1.Rows[rowSelected].Cells[6].Value.ToString();
                Type = dataGridView1.Rows[rowSelected].Cells[7].Value.ToString();
                Department = dataGridView1.Rows[rowSelected].Cells[8].Value.ToString();
                Location = dataGridView1.Rows[rowSelected].Cells[9].Value.ToString();
                Status = dataGridView1.Rows[rowSelected].Cells[10].Value.ToString();
                Quantity = dataGridView1.Rows[rowSelected].Cells[11].Value.ToString();
                PageNumber = dataGridView1.Rows[rowSelected].Cells[12].Value.ToString();
                ImageText = dataGridView1.Rows[rowSelected].Cells[13].Value.ToString();
                frmBookInfoDetails viewBooks = new frmBookInfoDetails(Accession, Title, Author, Publisher, Language, DatePublish, Subject, Type, Department, Location, Status, Quantity, PageNumber, ImageText);
                viewBooks.Show();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Error on Viewing Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int rowSelected;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                rowSelected = dataGridView1.CurrentCell.RowIndex;
                (sender as DataGridView).CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Row selected undefined", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            frmAddMaterial addBook = new frmAddMaterial(username);
            addBook.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // Refresh the user control form
            this.Refresh(); 
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToChar(e.KeyChar) == 13)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void cmbType_TextChanged(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnLabelNext_Click(object sender, EventArgs e)
        {
            currentPage++;
            autoRefresh();
        }

        private void btnLabelBack_Click(object sender, EventArgs e)
        {
            currentPage--;
            if (currentPage < 1)
                currentPage = 1;
            autoRefresh();
        }

    }
}
