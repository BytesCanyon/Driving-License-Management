using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1}
        public enMode mode = enMode.AddNew;

        public int PersonId {  get; set; }
        public string FirstName{  get; set; }
        public string SecondName{  get; set; }
        public string ThirdName{  get; set; }
        public string LastName{  get; set; }
        public string NationalNo{  get; set; }
        public DateTime DateOfBirth{  get; set; }
        public short Gendor{  get; set; }
        public string Address{  get; set; }
        public string Phone{  get; set; }
        public string Email{  get; set; }
        public int NationalityCountyID{  get; set; }
        public string ImagePath { get; set; }

        public clsCountry countryInfo;
        public clsPerson() { 
            this.PersonId = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountyID = -1;
            this.ImagePath = "";
            this.NationalNo = "";
            this.Gendor = -1;
            mode = enMode.AddNew;
        }
        public clsPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth,
            short Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath, int personID)
        {
            this.PersonId = personID;
            this.FirstName = FirstName;
            this.SecondName =SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountyID = NationalityCountryID;
            this.ImagePath = ImagePath;
            this.NationalNo = NationalNo;
            this.Gendor = Gendor;
            this.countryInfo = clsCountry.Find(NationalityCountryID);
            mode = enMode.Update;
        }

        private bool AddNewPerson()
        {
            this.PersonId = clsPersonData.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address,
                this.Phone, this.Email, this.NationalityCountyID, this.ImagePath);
            return this.PersonId != -1;
        }

        private bool UpdatePerson()
        {
            return clsPersonData.UpdatePersonByID(this.PersonId,this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address,
                this.Phone, this.Email, this.NationalityCountyID, this.ImagePath);
           
        }

        public static clsPerson Find(int personId)
        {
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            string Address = "";
            string Phone = "";
            short gendor = 2;
            string Email = "";
            int NationalityCountyID = -1;
            string ImagePath = "";
            string NationalNo = "";
            bool isFound = clsPersonData.GetPersonByID(personId, ref FirstName, ref SecondName, ref ThirdName, 
                ref LastName,ref NationalNo, ref DateOfBirth, ref gendor, ref Address,
                ref Phone, ref Email, ref NationalityCountyID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth,
             gendor, Address, Phone, Email,
             NationalityCountyID, ImagePath, personId);
            }
            else
            {
                return null;
            }
        }
        public static clsPerson Find(string nationalNo)
        {
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            string Address = "";
            string Phone = "";
            short gendor = 2;
            string Email = "";
            int NationalityCountyID = -1;
            string ImagePath = "";
            int personID = -1;
            bool isFound = clsPersonData.GetPersonByNationalNo(nationalNo, ref personID, ref FirstName, ref SecondName, ref ThirdName,
                ref LastName, ref DateOfBirth, ref gendor, ref Address,
                ref Phone, ref Email, ref NationalityCountyID, ref ImagePath);
            if (isFound)
            {
                return new clsPerson(nationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth,
             gendor, Address, Phone, Email,
             NationalityCountyID, ImagePath, personID);
            }
            else
            {
                return null;
            }
        }

        public  bool Save()
        {
            switch (mode)
            {
                case enMode.AddNew:
                    if (AddNewPerson())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return UpdatePerson();
            }
            return false;
        }

        public static DataTable GetAllPepole()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int id)
        {
           return  clsPersonData.DeletePerson(id);
        }

        public static bool isPersonExiste(int id)
        {
            return clsPersonData.IsPersonExist(id);
        }

        public static bool isPersonExiste(string nationalNo)
        {
            return clsPersonData.IsPersonExist(nationalNo);
        }
    }
}
