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
using Driving_License_Management.Global;
using Driving_License_Management.Properties;
using DVLD_Business;

namespace Driving_License_Management.People
{
    public partial class frmAddUpdatePepole : Form
    {
        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;

        public enum enMode
        {
            Add,
            Update
        }
        public enum enGender
        {
            Male = 0,
            Female = 1,
        }
        clsPerson _Person;
        int _PersonID = -1;

        enMode _Mode = enMode.Add;
        public frmAddUpdatePepole()
        {
            InitializeComponent();
            _Mode = enMode.Add;
        }

        public frmAddUpdatePepole(int PersonId)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonId;
        }

        private void frmAddUpdatePepole_Load(object sender, EventArgs e)
        {
            _ResetDefultValue();
            if (_Mode == enMode.Update) 
                _LoadData();
        }

        void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            
            if( _Person == null)
            {
                MessageBox.Show("No Person with id = " + _PersonID, " Person Not Found", MessageBoxButtons.OK);
                this.Close();
                return;
            }

            txtFirstName.Text = _Person.FirstName ;
            txtLastName.Text = _Person.LastName;
            txtEmail.Text = _Person.Email;
            txtNationalNo.Text = _Person.NationalNo;
            txtPhone.Text = _Person.Phone;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            rtbAdress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if(_Person.Gendor == 0)
            {
                rdbMale.Checked = true;
            }
            else
            {
                rdbFemale.Checked = true;
            }
            cbxCountry.SelectedIndex = cbxCountry.FindString(_Person.countryInfo.Name);

            if(_Person.ImagePath  != "")
            {
                pcbProfile.ImageLocation = _Person.ImagePath;
            }
            lblRemove.Visible = (_Person.ImagePath != "");
        }

        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbxCountry.Items.Add(row["CountryName"]);
            }
        }

        void _ResetDefultValue() {
            _FillCountriesInComoboBox();
            if (_Mode == enMode.Add) 
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtNationalNo.Text = "";
            txtPhone.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            rtbAdress.Text = "";
            dtpDateOfBirth.Value = DateTime.Now;
            cbxCountry.SelectedIndex = cbxCountry.FindString("Jordan");
            rdbMale.Checked = true;

            if (rdbMale.Checked) {
                pcbProfile.Image = Resources.Male_512;
            }
            else{
                pcbProfile.Image = Resources.Female_512;
            }

            lblRemove.Visible = (pcbProfile.ImageLocation != null);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            if (!_HandelPersonImage())
                return;

            int nationalityCountryId = clsCountry.Find(cbxCountry.Text).ID;
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = rtbAdress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.NationalNo = txtNationalNo.Text.Trim();

            if (rdbMale.Checked) { 
                _Person.Gendor = (short)enGender.Male;
            }
            else{
                _Person.Gendor = (short)enGender.Female;
            }

            _Person.NationalityCountyID = nationalityCountryId;

            if (pcbProfile.ImageLocation != null)
                _Person.ImagePath = pcbProfile.ImageLocation.ToString();            
            else
                _Person.ImagePath = "";
         
            if (_Person.Save())
            {
                lblPersonId.Text = _Person.PersonId.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Trigger the event to send data back to the caller form.
                DataBack?.Invoke(this, _Person.PersonId);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private bool _HandelPersonImage()
        {
            if (_Person.ImagePath != pcbProfile.ImageLocation)
            {
                if (_Person.ImagePath != "") 
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK);
                    }
                }
            }
            if (pcbProfile.ImageLocation != null)
            {
                string sourceIlageFile = pcbProfile.ImageLocation.ToString();
                if (clsUtils.CopyImageToProjectImagesFolder(ref sourceIlageFile)) {
                    pcbProfile.ImageLocation = sourceIlageFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK);
                    return false;
                }
            }
            return true;
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {

            // First: set AutoValidate property of your Form to EnableAllowFocusChange in designer 
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            //no need to validate the email incase it's empty.
            if (txtEmail.Text.Trim() == "")
                return;

            //validate email format
            if (!clsValidation.ValidateEmail(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else 
            {
                errorProvider1.SetError(txtEmail, null);
            };

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This field is required!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }

            //Make sure the national number is not used by another person
            if (txtNationalNo.Text.Trim() != _Person.NationalNo && clsPerson.isPersonExiste(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number is used for another person!");

            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }


        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                pcbProfile.ImageLocation = selectedFilePath;
                lblRemove.Visible = true;
            }
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pcbProfile.ImageLocation = null;
            if (rdbMale.Checked)
                pcbProfile.Image = Resources.Male_512;
            else
                pcbProfile.Image = Resources.Female_512;

            lblRemove.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pcbSave_Click(object sender, EventArgs e)
        {

        }

        private void rdbMale_Click(object sender, EventArgs e)
        {
            if (pcbProfile.ImageLocation == null)
                pcbProfile.Image = Resources.Male_512;            
        }

        private void rdbFemale_Click(object sender, EventArgs e)
        {
            if (pcbProfile.ImageLocation == null)
                pcbProfile.Image = Resources.Female_512;            
        }
    }
}
