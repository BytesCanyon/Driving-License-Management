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

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            ResetFields();

            _user = clsUser.FindByUserID(_userID);
            if (_user == null)
            {
                ShowError("Could not find user with ID = " + _userID, "Error");
                Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_userID);
        }

        private void ResetFields()
        {
            txtCurrentPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
            txtCurrentPassword.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                ShowError("Some fields are not valid. Hover over the red icon(s) to see the error.", "Validation Error");
                return;
            }

            UpdatePassword();
        }

        private void UpdatePassword()
        {
            _user.Password = txtNewPassword.Text;

            if (_user.Save())
            {
                ShowMessage("Password changed successfully.", "Success");
                ResetFields();
            }
            else
            {
                ShowError("An error occurred. Password did not change.", "Error");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(
                txtCurrentPassword,
                () => !string.IsNullOrWhiteSpace(txtCurrentPassword.Text),
                "Current password cannot be blank",
                e
            );

            if (!e.Cancel)
            {
                ValidateField(
                    txtCurrentPassword,
                    () => _user.Password.Trim() == txtCurrentPassword.Text.Trim(),
                    "Current password is wrong!",
                    e
                );
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(
                txtNewPassword,
                () => !string.IsNullOrWhiteSpace(txtNewPassword.Text),
                "New Password cannot be blank",
                e
            );
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateField(
                txtConfirmPassword,
                () => txtNewPassword.Text.Trim() == txtConfirmPassword.Text.Trim(),
                "Password confirmation does not match new password!",
                e
            );
        }

        private void ValidateField(TextBox field, Func<bool> validationLogic, string errorMessage, CancelEventArgs e)
        {
            if (!validationLogic())
            {
                e.Cancel = true;
                errorProvider1.SetError(field, errorMessage);
            }
            else
            {
                errorProvider1.SetError(field, null);
            }
        }

        private void ShowMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
