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
using static DVLD_Business.clsManageTestTypes;

namespace Driving_License_Management.TestTypes
{
    public partial class frmEditTestType : Form
    {
        clsManageTestTypes.enTestType _enTestTypeID;
        clsManageTestTypes _enTestType;

        public frmEditTestType(clsManageTestTypes.enTestType enTestTypeID)
        {
            _enTestTypeID = enTestTypeID;
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _enTestType.Title = txtTitle.Text.Trim();
            _enTestType.Description = txtDescription.Text.Trim();
            _enTestType.Fees = Convert.ToSingle(txtFees.Text.Trim());


            if (_enTestType.Save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _enTestType = clsManageTestTypes.Find(_enTestTypeID);

            if (_enTestType != null)
            {


                lblTestTypeID.Text = ((int)_enTestTypeID).ToString();
                txtTitle.Text = _enTestType.Title;
                txtDescription.Text = _enTestType.Description;
                txtFees.Text = _enTestType.Fees.ToString();
            }

            else

            {
                MessageBox.Show("Could not find Test Type with id = " + _enTestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
