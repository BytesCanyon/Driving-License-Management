using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace Driving_License_Management.People
{
    public partial class frmListPeople : Form
    {
        private static DataTable _dtAllpepole = clsPerson.GetAllPepole();
        private DataTable _dtPepole = _dtAllpepole.DefaultView.ToTable(false, "PersonID", "FirstName", "SecondName", "ThirdName",
            "LastName", "DateOfBirth", "Gendor", "Address", "Phone", "Email", "ImagePath");
        private void _RefreshPeopleList()
        {

        }
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dtPepole;
            cbFilter.SelectedIndex = 0;
            lblCount.Text = dataGridView1.Rows.Count.ToString();
            if (dataGridView1.Rows.Count > 0) 
            {
                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[1].HeaderText = "First Name";
                dataGridView1.Columns[2].HeaderText = "Second Name";
                dataGridView1.Columns[3].HeaderText = "Third Name";
                dataGridView1.Columns[4].HeaderText = "Last Name";
                dataGridView1.Columns[5].HeaderText = "Date of Birth";
                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[7].HeaderText = "Address";
                dataGridView1.Columns[8].HeaderText = "Phone";
                dataGridView1.Columns[9].HeaderText = "Email";
                dataGridView1.Columns[10].HeaderText = "Image Path";
                // Format columns if needed (e.g., column width, alignment, etc.)
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                // Optionally, set the DateOfBirth column to display only the date
                dataGridView1.Columns["DateOfBirth"].DefaultCellStyle.Format = "yyyy-MM-dd";
                // Center-align the Person ID and Gender columns for better readability
                dataGridView1.Columns["PersonID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["Gendor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                // Add alternating row styles for better visual distinction
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                // Ensure no row is selected initially
                dataGridView1.ClearSelection();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personid = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmShowPersonInfo frmShowPersonInfo = new frmShowPersonInfo(personid);
            frmShowPersonInfo.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePepole frmAdd = new frmAddUpdatePepole();
            frmAdd.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int personid = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmAddUpdatePepole frmAdd = new frmAddUpdatePepole(personid);
            frmAdd.ShowDialog();
            _RefreshPeopleList();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person with ID [" +
      dataGridView1.CurrentRow.Cells[0].Value.ToString() + "]?", "Confirm Deletion",
      MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                int personId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                bool isDeleted = clsPerson.DeletePerson(personId);
                if (isDeleted)
                {
                    MessageBox.Show("Person with ID [" + personId + "] has been successfully deleted.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
                else
                {
                    MessageBox.Show("Failed to delete the person. Please try again.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            switch (cbFilter.Text)
            {
                case "Person ID":
                    filterColumn = "PersonID";
                    break;
                case "First Name":
                    filterColumn = "FirstName";
                    break;
                case "Second Name":
                    filterColumn = "SecondName";
                    break;
                case "Third Name":
                    filterColumn = "ThirdName";
                    break;
                case "Last Name":
                    filterColumn = "LastName";
                    break;
                case "Date of Birth":
                    filterColumn = "DateOfBirth";
                    break;
                case "Gender":
                    filterColumn = "Gendor";
                    break;
                case "Address":
                    filterColumn = "Address";
                    break;
                case "Phone":
                    filterColumn = "Phone";
                    break;
                case "Email":
                    filterColumn = "Email";
                    break;
                case "Image Path":
                    filterColumn = "ImagePath";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }
            if (textBox1.Text.Trim() == "" || filterColumn == "None") {
                _dtPepole.DefaultView.RowFilter = "";
                lblCount.Text = dataGridView1.Rows.Count.ToString();
                return;
            }
            if (filterColumn == "PersonID") 
            {
                _dtPepole.DefaultView.RowFilter = string.Format("[{0}] = {1}", filterColumn, textBox1.Text.Trim());
            }
            else
            {
                _dtPepole.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", filterColumn, textBox1.Text.Trim());
            }
            lblCount.Text = dataGridView1.Rows.Count.ToString();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = (cbFilter.Text != "None");
            if (textBox1.Visible) {
                textBox1.Text = "";
                textBox1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePepole frmAddUpdatePepole = new frmAddUpdatePepole();
            frmAddUpdatePepole.ShowDialog();
        }
    }
}
