using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLD_Business;

namespace Driving_License_Management.Users
{
    public partial class frmChangePassword : Form
    {
        private readonly int _userID;
        private clsUser _user;

        public frmChangePassword(int userID)
        {
            InitializeComponent();
            _userID = userID;
        }

        private void ResetDefaultValues()
        {
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            txtCurrentPassword.Clear();
            txtCurrentPassword.Focus();
        }


        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password cannot be blank");
                return;
            }

            errorProvider1.SetError(txtCurrentPassword, null);

            if (_user.Password.Trim() != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current password is wrong!");
                return;
            }

            errorProvider1.SetError(txtCurrentPassword, null);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "New Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password confirmation does not match new password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid. Hover over the red icon(s) to see the error.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user.Password = txtNewPassword.Text;

            if (_user.Save())
            {
                MessageBox.Show("Password changed successfully.",
                    "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("An error occurred. Password did not change.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();

            _user = clsUser.FindByUserID(_userID);

            if (_user == null)
            {
                MessageBox.Show("Could not find user with ID = " + _userID,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_userID);
        }
    }
}
