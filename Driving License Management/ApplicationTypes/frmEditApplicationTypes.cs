using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driving_License_Management.People.Controles;
using DVLD_Business;

namespace Driving_License_Management.ApplicationTypes
{
    public partial class frmEditApplicationTypes : Form
    {
        private int _ID;

        clsApplicationType _ApplicationType;
        public frmEditApplicationTypes(int ID)
        {
            InitializeComponent();
            this._ID = ID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _ApplicationType.Fees =int.Parse(txtFees.Text);
            _ApplicationType.Title = txtTitle.Text;
            lblID.Text = txtTitle.Text;


            if (_ApplicationType.Save())
            {
                lblID.Text = _ApplicationType.ID.ToString();

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void editApplicationTypes_Load(object sender, EventArgs e)
        {
            _ResetDefualtVales();
            _LoadData();
        }
        void _LoadData()
        {
            _ApplicationType = clsApplicationType.Find(this._ID);
            if (_ApplicationType == null)
            {
                MessageBox.Show("No Application Type with ID = " + _ID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            txtFees.Text = _ApplicationType.Fees.ToString();
            txtTitle.Text = _ApplicationType.Title;
            lblID.Text = _ApplicationType.ID.ToString();
        }
        void _ResetDefualtVales()
        {
            txtFees.Text = string.Empty;
            txtTitle.Text = string.Empty;
            lblID.Text = "???";
        }
    }
}
