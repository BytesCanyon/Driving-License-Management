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

namespace Driving_License_Management.ApplicationTypes
{
    public partial class frmListApplicationTypes : Form
    {
        private DataTable _dtList;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtList = clsApplicationType.GetAllApplicationTypes();
            dataGridView1.DataSource = _dtList;

            lblRecords.Text = dataGridView1.Rows.Count.ToString();
            // Set header texts for individual columns.
            if (dataGridView1.Columns.Count > 0) // Ensure there are enough columns
            {
                dataGridView1.Columns[0].HeaderText = "User ID";
                dataGridView1.Columns[1].HeaderText = "Title";
                dataGridView1.Columns[2].HeaderText = "Fees";
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].FillWeight = 25;
            dataGridView1.Columns[1].FillWeight = 50;
            dataGridView1.Columns[2].FillWeight = 25; 
        }
    }
}
