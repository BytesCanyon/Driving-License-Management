using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        public enum Gender
        {
            Male = 0,
            Female = 1,
        }

        public static bool GetCountryInfoByID(int id, ref string countryName)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT CountryName FROM Countries WHERE CountryID = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                countryName = (string)reader["CountryName"];
                                isFound = true;
                            }
                        }
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool GetCountryInfoByName(string countryName, ref int id)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT CountryID FROM Countries WHERE CountryName = @CountryName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CountryName", countryName);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                id = (int)reader["CountryID"];
                                isFound = true;
                            }
                        }
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static DataTable GetCountryList()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Countries";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader sqlDataReader = command.ExecuteReader())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            dt.Load(sqlDataReader);
                        }
                    }
                }
                catch
                {

                }
            }

            return dt;
        }
    }
}
