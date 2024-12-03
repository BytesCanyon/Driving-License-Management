using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driving_License_Management.People;
using Driving_License_Management.Users;

namespace Driving_License_Management
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
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
            frmUserAddEdit users = new frmUserAddEdit();
            users.Show();
        }
    }
}
