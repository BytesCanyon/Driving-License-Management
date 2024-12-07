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

namespace Driving_License_Management.Users
{
    public partial class frmUserAddEdit : Form
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        enMode mode;
        int _UserID = -1;
        clsUser _User;

        public frmUserAddEdit()
        {
            InitializeComponent();

            mode = enMode.AddNew;
        }

        public frmUserAddEdit(int UserID)
        {
            InitializeComponent();

            mode = enMode.Update;
            _UserID = UserID;
        }

        private void frmUserAddEdit_Load(object sender, EventArgs e)
        {
            _ResetDefualtVales();
            if (mode == enMode.Update)            
                _LoadData();          
        }

        void _ResetDefualtVales()
        {
            if (mode == enMode.AddNew) 
            { 
                lblUserID.Text = "";
                this.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
            }
            else
            {
                this.Text = "Update User";
            }
            lblUserID.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            chkIsActive.Checked = true;
            txtConfirmPassword.Text = "";
        }
        void _LoadData()
        {
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.isActive;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);          
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (mode == enMode.Update) {
                tpLoginInfo.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                txtUserName.Text = _User.UserName;
                txtPassword.Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;
                chkIsActive.Checked = _User.isActive;
                return;
            }
            if(ctrlPersonCardWithFilter1.PersonID != -1)
            {
                if (clsUser.isUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                    tpLoginInfo.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.Focus();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.isActive = chkIsActive.Checked;


            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                //change form mode to update.
                mode = enMode.Update;
                this.Text = "Update User";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };

        }

        private void tcUserInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tcUserInfo.SelectedTab == tcUserInfo.TabPages["tpPersonInfo"])
            {
                _ResetDefualtVales();
            }
        }
    }
}
