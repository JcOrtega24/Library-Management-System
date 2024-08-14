using LibraryManagementSystem.Form_Publisher_Management;
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
    public partial class frmPublisherList : Form
    {
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 1;
        Library1 publisherList = new Library1("localhost", "librarymanagementstudent", "root", "");

        public frmPublisherList()
        {
            InitializeComponent();
        }

        public void autoRefresh()
        {
            try
            {
                /*int startIndex = (currentPage - 1) * pageSize;
                DataTable dt = publisherList.GetData($"SELECT id, Name, Gender FROM publisher LIMIT {startIndex}, {pageSize}");
                dataGridView1.DataSource = dt;

                int recordCount = Convert.ToInt32(publisherList.GetData("SELECT COUNT(*) FROM publisher").Rows[0][0]);
                totalPages = (int)Math.Ceiling((double)recordCount / pageSize);

                lblPageInfo.Text = $"Page {currentPage} of {totalPages}";*/

                DataTable dt = publisherList.GetData("SELECT * FROM publisher");
                dataGridView1.DataSource = dt;

                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                foreach (DataGridViewColumn dc in dataGridView1.Columns)
                {
                    if (dc.Index.Equals(0))
                    {
                        dc.ReadOnly = false;
                    }
                    else
                    {
                        dc.ReadOnly = true;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Author info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void frmPublisherList_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                autoRefresh();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                autoRefresh();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddPublisher addPublisher = new frmAddPublisher();
            addPublisher.Show();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    // Deletion code goes here
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        DataGridViewCheckBoxCell checkbox = dataGridView1.Rows[i].Cells["checkboxColumn"] as DataGridViewCheckBoxCell;
                        if (checkbox.Value != null && (bool)checkbox.Value == true)
                        {
                            string ID = dataGridView1.Rows[i].Cells[1].Value.ToString();
                            string publisher = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            publisherList.executeSQL("DELETE FROM publisher WHERE id = '" + ID + "'");
                            if (publisherList.rowAffected > 0)
                            {
                                MessageBox.Show("Publisher deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                publisherList.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Publisher','" + publisher + " deleted in publisher')");
                                autoRefresh();
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = publisherList.GetData("SELECT id , Name, Gender FROM publisher WHERE id LIKE '%" + txtSearch.Text + "%' OR Name LIKe '%" + txtSearch.Text + "%' OR Gender LIKE '%" + txtSearch.Text + "%'");
                dataGridView1.DataSource = dt;
                this.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int checkedCount = 0; // Count of checked checkboxes
            DataGridViewRow checkedRow = null; // The row with the checked checkbox

            // Loop through the rows and check the checkboxes
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[0];
                if (chk.Value != null && (bool)chk.Value)
                {
                    checkedCount++;
                    checkedRow = dataGridView1.Rows[i];
                }
            }

            // If more than one checkbox is checked, show an error message
            if (checkedCount > 1)
            {
                MessageBox.Show("Please select only one row to update.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // If one checkbox is checked, open the form
            else if (checkedCount == 1)
            {
                string ID, name, gender;
                ID = checkedRow.Cells[1].Value.ToString();
                name = checkedRow.Cells[2].Value.ToString();
                gender = checkedRow.Cells[3].Value.ToString();
                frmUpdatePublisher publisherUP = new frmUpdatePublisher(ID, name, gender);
                publisherUP.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to update.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
