using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driving_License_Management.ApplicationTypes;
using Driving_License_Management.Global;
using Driving_License_Management.Login;
using Driving_License_Management.People;
using Driving_License_Management.Users;

namespace Driving_License_Management
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        public frmMain(frmLogin login)
        {
            InitializeComponent();
            _frmLogin = login;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePepole frmAddUpdatePepole = new frmAddUpdatePepole();
            frmAddUpdatePepole.Show();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFindPerson frmFindPerson = new frmFindPerson();
            frmFindPerson.Show();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(1);
            frm.Show();
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople asd = new frmListPeople();
            asd.Show();
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListPeople frmListPeople = new frmListPeople();
            frmListPeople.Show();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListUsers users = new frmListUsers();
            users.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frmChangePassword = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frmChangePassword.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUserInfo = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frmUserInfo.ShowDialog();
        }

        private void manageApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frmListApplicationTypes = new frmListApplicationTypes();
            frmListApplicationTypes.ShowDialog();
        }
    }
}
