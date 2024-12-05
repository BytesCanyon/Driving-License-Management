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

namespace Driving_License_Management.Users.Controles
{
    public partial class ctrlUserCard : UserControl
    {
        clsUser clsUser;
        private int _id = -1;

        public int UserID
        {
            get {return _id;}
        }

        public ctrlUserCard()
        {
            InitializeComponent();
        }
        public void LoadUserInfo(int userid)
        {
            clsUser = clsUser.FindByUserID(userid);
            if (clsUser == null) {
                _ResetPersonInfo();
                MessageBox.Show("No User with ID = " + userid, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _FillUserInfo();
        }
        void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonIfo(clsUser.PersonID);
            lblUserid.Text = clsUser.UserID.ToString();
            lblUsername.Text = clsUser.UserName.ToString();
            if (clsUser.isActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }
        void _ResetPersonInfo()
        {
            lblUserid.Text = "???";
            lblUsername.Text = "???";
            lblIsActive.Text = "???";
        }
    }
}
