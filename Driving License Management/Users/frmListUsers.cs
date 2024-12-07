using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driving_License_Management.People;
using DVLD_Business;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Driving_License_Management.Users
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dataGridView1.DataSource = _dtAllUsers;
            cbActive.SelectedIndex = 0;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();

            // Set header texts for individual columns.
            if (dataGridView1.Columns.Count > 0) // Ensure there are enough columns
            {
                dataGridView1.Columns[0].HeaderText = "User ID";
                dataGridView1.Columns[1].HeaderText = "Person ID";
                dataGridView1.Columns[2].HeaderText = "Full Name";
                dataGridView1.Columns[3].HeaderText = "UserName";
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].FillWeight = 15; // 15% for User ID
            dataGridView1.Columns[1].FillWeight = 15; // 15% for Person ID
            dataGridView1.Columns[2].FillWeight = 45; // 45% for Full Name
            dataGridView1.Columns[3].FillWeight = 15; // 15% for UserName
            dataGridView1.Columns[4].FillWeight = 10; // 10% for Extra Column
            cbActive.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUserInfo frm = new frmUserInfo(userID);
            frm.ShowDialog();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserAddEdit frm = new frmUserAddEdit();  
            frm.Show();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUserAddEdit frm = new frmUserAddEdit(userID);
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user with ID [" +
                dataGridView1.CurrentRow.Cells[0].Value.ToString() + "]?", "Confirm Deletion",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int userid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                bool isDeleted = clsUser.DeleteUser(userid);
                if (isDeleted)
                {
                    MessageBox.Show("User with ID [" + userid + "] has been successfully deleted.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
                else
                {
                    MessageBox.Show("Failed to delete the User. Please try again.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmChangePassword frmChangePassword = new frmChangePassword(userID);
            frmChangePassword.ShowDialog();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "User ID":
                    filterColumn = "UserID";
                    break;
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "User Name":
                    filterColumn = "UserName";
                    break;
                case "Is Active":
                    filterColumn = "IsActive";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }
            if (txtFilter.Text.Trim() == "" || filterColumn == "None")
            {
                _dtAllUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
                return;
            }
            if (filterColumn == "Person ID")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilter.Text.Trim());
            }
            if (filterColumn == "UserID")
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, txtFilter.Text.Trim());
            }
            else
            {
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterColumn, txtFilter.Text.Trim());
            }
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void cbActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtAllUsers.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 4)
            {
                cbActive.Visible = true;
                txtFilter.Visible = false;
            }
            else
            {
                cbActive.Visible = false;
                txtFilter.Visible = true;
            }
        }
    }
}
