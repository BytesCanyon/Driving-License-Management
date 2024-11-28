using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving_License_Management.People
{
    public partial class frmShowPersonInfo : Form
    {
        public frmShowPersonInfo(int id)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonIfo(id);
        }
        public frmShowPersonInfo(string no)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonIfo(no);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
