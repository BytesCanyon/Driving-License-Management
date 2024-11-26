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

namespace Driving_License_Management
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdatePepole frmAddUpdatePepole = new frmAddUpdatePepole();
            frmAddUpdatePepole.Show();
        }
    }
}
