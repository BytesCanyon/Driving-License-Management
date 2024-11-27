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

namespace Driving_License_Management.People.Controles
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            txtSearch.Focus();
        }

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonId) 
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null) 
            {
                handler(PersonId);
            }
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilter.Visible = _FilterEnabled;
            }
        }

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtSearch.Text = PersonID.ToString();
            FindNow();
        }
        public void FindNow()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadPersonIfo(int.Parse(txtSearch.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadPersonIfo(txtSearch.Text);
                    break;
                default:
                    break;
            }
            if(OnPersonSelected != null && FilterEnabled)
            {
                OnPersonSelected(ctrlPersonCard1.PersonID);
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePepole frmAddUpdatePepole = new frmAddUpdatePepole();
            frmAddUpdatePepole.DataBack += DataBackEvent;
            frmAddUpdatePepole.Show();
        }
        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilter.SelectedIndex = 1;
            txtSearch.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonIfo(PersonID);
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtSearch, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtSearch, null);
            }
        }


        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnSeach.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();
        }
    }
}
