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

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sdsd.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(sdsd, "Current password cannot be blank");
                return;
            }

            errorProvider1.SetError(sdsd, null);

            if (_user.Password != sdsd.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(sdsd, "Current password is wrong!");
                return;
            }

            errorProvider1.SetError(sdsd, null);
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sd.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(sd, "New Password cannot be blank");
            }
            else
            {
                errorProvider1.SetError(sd, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (dssd.Text.Trim() != sd.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(dssd, "Password confirmation does not match new password!");
            }
            else
            {
                errorProvider1.SetError(dssd, null);
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

            _user.Password = sd.Text;

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
    }
}
