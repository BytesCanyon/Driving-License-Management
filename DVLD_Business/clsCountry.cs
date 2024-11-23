using System;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsCountry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public clsCountry() { }
        private clsCountry(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public static clsCountry Find(int nationalCountryId)
        {
            string name = string.Empty;
            bool isFound = clsCountryData.GetCountryInfoByID(nationalCountryId, ref name);

            return isFound ? new clsCountry(nationalCountryId, name) : null;
        }
        public static clsCountry Find(string countryName)
        {
            int id = -1;
            bool isFound = clsCountryData.GetCountryInfoByName(countryName, ref id);

            return isFound ? new clsCountry(id, countryName) : null;
        }
    }
}
