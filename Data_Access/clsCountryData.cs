using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        public enum  Gender
        {
            Male = 0,
            FeMale = 1,
        }

        public static bool GetCountryInfoByID(int id, ref string CountryName)
        {
            return true;
        }
        public static bool GetCountryInfoByName(string CountryName,ref int id)
        {
            return true;
        }

        public static void GetCountryList() { }
    }
}
