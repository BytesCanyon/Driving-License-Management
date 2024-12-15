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

namespace Driving_License_Management.TestTypes
{
    public partial class frmManageTestTypes : Form
    {
        private static DataTable _dtTestTypes;

        public frmManageTestTypes()
        {
            InitializeComponent();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
            _dtTestTypes = clsManageTestTypes.GetAllTestTypes();
            dataGridView1.DataSource = _dtTestTypes;            
            lblRecords.Text = dataGridView1.Rows.Count.ToString();

            // Set header texts for individual columns.
            if (dataGridView1.Columns.Count > 0) // Ensure there are enough columns
            {
                dataGridView1.Columns[0].HeaderText = "ID";
                dataGridView1.Columns[1].HeaderText = "Tiile";
                dataGridView1.Columns[2].HeaderText = "Description";
                dataGridView1.Columns[3].HeaderText = "Fees";
            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 15;
            dataGridView1.Columns[2].FillWeight = 45;
            dataGridView1.Columns[3].FillWeight = 15;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frmEditTestType = new frmEditTestType((clsManageTestTypes.enTestType)dataGridView1.CurrentRow.Cells[0].Value);
            frmEditTestType.ShowDialog();
        }
    }
}
