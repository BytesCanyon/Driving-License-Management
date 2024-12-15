using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Linq;
using System.Text;
using Data_Access;
using System;

namespace DVLD_Business
{
    public class clsManageTestTypes
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public  enum enTestType { VisionTest = 1, WrittenTest = 2,StreetTest=3 };

        public enTestType enTest { set; get; }
        public string Title { set; get; }
        public string Description { set; get; } 
        public float Fees { set; get; }
        public clsManageTestTypes()
        {
            this.enTest = enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddNew;
        }

        public clsManageTestTypes(enTestType test, string TestTypeTitel,string Description,float TestTypeFees)
        {
            this.enTest = test;
            this.Title = TestTypeTitel;
            this.Description = Description;
            this.Fees = TestTypeFees;
            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            this.enTest = (enTestType) clsDataManageTestTypes.AddNewTestType(this.Title,this.Description, this.Fees);              
            return (this.Title !="");
        }

        private bool _UpdateTestType()
        {
            return clsDataManageTestTypes.UpdateTestType((int) this.enTest, this.Title,this.Description,this.Fees);
        }

        public static clsManageTestTypes Find(enTestType TestTypeID)
        {
            string Title = "", Description=""; float Fees=0;
            if (clsDataManageTestTypes.GetTestTypeInfoByID((int) TestTypeID, ref Title,ref Description, ref Fees))
                return new clsManageTestTypes(TestTypeID, Title, Description,Fees);
            else
                return null;
        }

        public static DataTable GetAllTestTypes()
        {
            return clsDataManageTestTypes.GetAllTestTypes();
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestType();

            }

            return false;
        }
    }
}
