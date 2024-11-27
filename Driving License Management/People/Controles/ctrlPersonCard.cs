using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Driving_License_Management.Properties;
using DVLD_Business;

namespace Driving_License_Management.People.Controles
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;
        private int _PersonID = -1;

        public clsPerson SelectedPersonInfo { get => _Person; }

        public int PersonID { get => _PersonID; }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }
        private void lblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePepole frmAddUpdatePepole = new frmAddUpdatePepole(_PersonID);
            frmAddUpdatePepole.ShowDialog();
            LoadPersonIfo(_PersonID);
        }
        public void LoadPersonIfo(string No)
        {
            _Person = clsPerson.Find(No);
            if (_Person == null)
            {
                ResetPersonIfo();
                MessageBox.Show("No Person With National No = " + No.ToString(), "Error", MessageBoxButtons.OK);
            }
            _FillPersonInfo();
        }

        public void LoadPersonIfo(int id)
        {
            _Person = clsPerson.Find(id);
            if (_Person == null)
            {
                ResetPersonIfo();
                MessageBox.Show("No Person With PersonID = " + id.ToString(), "Error", MessageBoxButtons.OK);
                return;
            }
            _FillPersonInfo();
        }



        private void ResetPersonIfo()
        {
            lblPeronID.Text = "";
            lblName.Text = "";
            lblEmail.Text = "";
            lbladdress.Text = "";
            lblGender.Text = "";
            lblNationalNo.Text = "";
            lblCountry.Text ="";
            lblPhone.Text = "";
            lblDateOfBirth.Text = "";
            pcbProfile.Image = Resources.Male_512;
        }
        private void _FillPersonInfo()
        {
            lblPeronID.Text = _Person.PersonId.ToString();
            _PersonID = _Person.PersonId;
            lblName.Text = _Person.FirstName.ToString() + " " + _Person.LastName;
            lblEmail.Text = _Person.Email.ToString();
            lbladdress.Text = _Person.Address.ToString();
            lblGender.Text = _Person.Gendor == 0 ? "Male":"Female";
            lblNationalNo.Text = _Person.NationalNo.ToString();
            lblCountry.Text = clsCountry.Find(_Person.NationalityCountyID).Name.ToString();
            lblPhone.Text = _Person.Phone.ToString();
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString();
            _LoadPersonImgae();
        }

        void _LoadPersonImgae()
        {
            if (_Person.Gendor != 0) 
            {
                pcbProfile.Image = Resources.Female_512;
            }
            else
            {
                pcbProfile.Image = Resources.Male_512;
            }
            if(_Person.ImagePath != "")
            {
                if(File.Exists(_Person.ImagePath))
                    pcbProfile.ImageLocation = _Person.ImagePath;
                else
                    MessageBox.Show("Could Not Find This Image: = " +  _Person.ImagePath, "Error", MessageBoxButtons.OK);
            }
        }

        private void clsPersonCard_Load(object sender, EventArgs e)
        {

        }

  
    }
}
