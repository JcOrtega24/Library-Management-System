using LibraryManagementSystem.Form_Author_Management;
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

namespace LibraryManagementSystem.Form_Location_Management
{
    public partial class frmLocationList : Form
    {
        public frmLocationList()
        {
            InitializeComponent();
        }

        Library1 locationList = new Library1("localhost", "librarymanagementstudent", "root", "");

        public void autoRefresh()
        {
            try
            {
                DataTable dt = locationList.GetData("SELECT LocationID , Location FROM location");
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

                this.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Author info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLocationList_Load(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddLocation addLocation = new frmAddLocation();
            addLocation.Show();
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                            string location = dataGridView1.Rows[i].Cells[2].Value.ToString();
                            locationList.executeSQL("DELETE FROM location WHERE LocationID = '" + ID + "'");
                            if (locationList.rowAffected > 0)
                            {
                                MessageBox.Show("Location deleted!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                locationList.executeSQL("INSERT INTO logs (date, time, module, action) VALUES('" + DateTime.Now.ToString("yyyy-MM-dd ") + "','"
                                        + DateTime.Now.ToLongTimeString() + "','Location','" + location + " deleted in location')");
                                autoRefresh();
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Location", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                string ID, location;
                ID = checkedRow.Cells[1].Value.ToString();
                location = checkedRow.Cells[2].Value.ToString();
                frmUpdateLocation locationUP = new frmUpdateLocation(ID, location);
                locationUP.Show();
            }
            // If no checkboxes are checked, show an error message
            else
            {
                MessageBox.Show("Please select a row to update.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            autoRefresh();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = locationList.GetData("SELECT LocationID, Location FROM location WHERE LocationID LIKE '%" + txtSearch.Text + "%' OR Location LIKe '%" + txtSearch.Text + "%'");
                dataGridView1.DataSource = dt;
                this.Show();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error on Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
