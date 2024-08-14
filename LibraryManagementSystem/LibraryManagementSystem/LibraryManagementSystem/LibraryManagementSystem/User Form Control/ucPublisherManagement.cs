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
    public partial class ucPublisherManagement : UserControl
    {
        public ucPublisherManagement()
        {
            InitializeComponent();
        }
        Library1 bookList = new Library1("localhost", "librarymanagementstudent", "root", "");
        public void autoRefresh()
        {
            try
            {
                DataTable dt = bookList.GetData("SELECT id , Name, Gender FROM publisher ");
                dataGridView1.DataSource = dt;
                this.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Author info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void publisherManagement_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddPublisher addPublisher = new frmAddPublisher();
            addPublisher.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            publisherManagement_Load(sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult answer = MessageBox.Show("Are you sure you want to delete this data?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    bookList.executeSQL("DELETE FROM publisher WHERE id ='" + dataGridView1.Rows[rowSelected].Cells[0].Value.ToString() + "' ");
                    if (bookList.rowAffected > 0)
                    {
                        MessageBox.Show("Publisher deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);                      
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int rowSelected;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            rowSelected = dataGridView1.CurrentCell.RowIndex;
            (sender as DataGridView).CurrentRow.DefaultCellStyle.SelectionBackColor = Color.LightSkyBlue;
        }
    }
}
