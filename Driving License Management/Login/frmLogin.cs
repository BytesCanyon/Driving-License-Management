using System;
using System.Windows.Forms;
using Driving_License_Management.Global;
using DVLD_Business;

namespace Driving_License_Management.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                cbRememberMe.Checked = true;
            }
            else
                cbRememberMe.Checked = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes the login form
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            clsUser user = clsUser.FindByUsernameAndPassword(username, password);

            if (user == null)
            {
                ShowError("Invalid Username/Password.", "Wrong Credentials");
                txtUserName.Focus();
                return;
            }

            HandleRememberMe(username, password);

            if (!user.isActive)
            {
                ShowError("Your account is not active. Contact the administrator.", "Inactive Account");
                txtUserName.Focus();
                return;
            }

            ProceedToMainForm(user);
        }

        private void HandleRememberMe(string username, string password)
        {
            if (cbRememberMe.Checked)
            {
                // Store username and password
                clsGlobal.RememberUsernameAndPassword(username, password);
            }
            else
            {
                // Clear stored username and password
                clsGlobal.RememberUsernameAndPassword(string.Empty, string.Empty);
            }
        }

        private void ShowError(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ProceedToMainForm(clsUser user)
        {
            clsGlobal.CurrentUser = user;
            this.Hide();
            using (frmMain mainForm = new frmMain(this))
            {
                mainForm.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
