using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode mode = enMode.AddNew;
        public int UserID {  get; set; }
        public int PersonID {  get; set; }
        public string Password {  get; set; }
        public string UserName {  get; set; }
        public bool isActive {  get; set; }
        public clsPerson personInfo;

        public clsUser() { 
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.isActive = true;
            mode = enMode.AddNew;
        }

        public clsUser(int userId, string username, int personID,string password, bool isActive)
        {
            this.UserID = userId;
            this.UserName = username;
            this.Password = password;
            this.PersonID = personID;
            this.personInfo = clsPerson.Find(personID);
            this.isActive = isActive;
            mode = enMode.Update;
        }
        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.isActive);
            return  this.UserID != -1;
        }

        private bool _UpdateUser()
        { 
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.isActive);
        }

        public static clsUser FindByUserID(int UserID)
        {
            bool isFound = false;
            string UserName = "";
            string Password = "";
            bool isActive = false;
            int personid = -1;
            isFound = clsUserData.GetUserInfoByUserID(UserID,ref personid,ref UserName,ref Password,ref isActive);
            if (isFound) {
                return new clsUser(UserID, UserName, personid, Password, isActive);
            } else {
                return null;
            }
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            bool isFound = false;
            string UserName = "";
            string Password = "";
            bool isActive = false;
            int UserID = -1;
            isFound = clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref isActive);
            if (isFound)
            {
                return new clsUser(UserID, UserName, PersonID, Password, isActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUser FindByUsernameAndPassword(string username, string password)
        {
            bool isFound = false;
            bool isActive = false;
            int UserID = -1;
            int PersonID = -1;
            isFound = clsUserData.GetUserInfoByUsernameAndPassword(username,password,ref PersonID, ref UserID, ref isActive);
            if (isFound)
            {
                return new clsUser(UserID, username, PersonID, password, isActive);
            }
            else
            {
                return null;
            }
        }
        public bool Save()
        {
            switch (mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers() { 
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID) 
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(string  username)
        {
            return clsUserData.IsUserExist(username);
        }
        public static bool isUserExistForPersonID(int personID)
        {
            return clsUserData.IsUserExistForPersonID(personID);
        }

    }
}
