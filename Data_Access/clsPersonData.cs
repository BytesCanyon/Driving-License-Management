using System;
using System.Data;
using System.ComponentModel;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static bool GetPersonByID(int PersonId, ref string FirstName, ref string SecondName,
        ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth,
        ref short Gendor, ref string Address, ref string Phone, ref string Email,
        ref int NationalityCountyID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection("");
            string query = "select * from People where PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountyID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        public static bool GetPersonByNationalNo(string NationalNo, ref int PersonId, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
        ref short Gendor, ref string Address, ref string Phone, ref string Email,
        ref int NationalityCountyID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection("");
            string query = "select * from People where NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];
                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        ThirdName = (string)reader["ThirdName"];
                    }
                    else
                    {
                        ThirdName = "";
                    }
                    LastName = (string)reader["LastName"];
                    NationalNo = (string)reader["NationalNo"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Gendor = (byte)reader["Gendor"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    if (reader["Email"] != DBNull.Value)
                    {
                        Email = (string)reader["Email"];
                    }
                    else
                    {
                        Email = "";
                    }
                    NationalityCountyID = (int)reader["NationalityCountryID"];
                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        ImagePath = (string)reader["ImagePath"];
                    }
                    else
                    {
                        ImagePath = "";
                    }
                    isFound = true;
                }
                else
                {
                    isFound = false;
                }
                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth,
            short Gendor, string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            // Connection string should be properly configured
            using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
            {
                // Corrected SQL query
                string query = @"
            INSERT INTO People (
                NationalNo, FirstName, SecondName, ThirdName, LastName,
                DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath
            )
            VALUES (
                @NationalNo, @FirstName, @SecondName, @ThirdName, @LastName,
                @DateOfBirth, @Gendor, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath
            );
            SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with null checks for optional fields
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SecondName", string.IsNullOrWhiteSpace(SecondName) ? (object)DBNull.Value : SecondName);
                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(ThirdName) ? (object)DBNull.Value : ThirdName);

                    try
                    {
                        connection.Open();

                        // ExecuteScalar returns the SCOPE_IDENTITY
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            PersonID = insertedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error (replace with proper logging mechanism)
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return PersonID;
        }

        public static bool UpdatePersonByID(
       int PersonID, string NationalNo, string FirstName, string SecondName,
       string ThirdName, string LastName, DateTime DateOfBirth,
       short Gendor, string Address, string Phone, string Email,
       int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            // Connection string should be properly configured
            using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
            {
                // Corrected SQL query for updating a record
                string query = @"
            UPDATE People
            SET
                NationalNo = @NationalNo,
                FirstName = @FirstName,
                SecondName = @SecondName,
                ThirdName = @ThirdName,
                LastName = @LastName,
                DateOfBirth = @DateOfBirth,
                Gendor = @Gendor,
                Address = @Address,
                Phone = @Phone,
                Email = @Email,
                NationalityCountryID = @NationalityCountryID,
                ImagePath = @ImagePath
            WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters with null checks for optional fields
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                    command.Parameters.AddWithValue("@Gendor", Gendor);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                    command.Parameters.AddWithValue("@ImagePath", ImagePath ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@SecondName", string.IsNullOrWhiteSpace(SecondName) ? (object)DBNull.Value : SecondName);
                    command.Parameters.AddWithValue("@ThirdName", string.IsNullOrWhiteSpace(ThirdName) ? (object)DBNull.Value : ThirdName);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            // Connection string should be properly configured
            using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
            {
                string query = @"
            SELECT 
                People.PersonID, 
                People.NationalNo,
                People.FirstName, 
                People.SecondName, 
                People.ThirdName, 
                People.LastName,
                People.DateOfBirth, 
                People.Gendor,
                CASE
                    WHEN People.Gendor = 0 THEN 'Male'
                    ELSE 'Female'
                END AS GendorCaption,
                People.Address, 
                People.Phone, 
                People.Email, 
                People.NationalityCountryID, 
                Countries.CountryName, 
                People.ImagePath
            FROM People 
            INNER JOIN Countries 
                ON People.NationalityCountryID = Countries.CountryID
            ORDER BY People.FirstName";

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
                    catch (Exception ex)
                    {
                        // Log the exception (replace with a proper logging mechanism)
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dt;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
            {
                string query = @"DELETE FROM People WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return false;
                    }
                }
            }
            return rowsAffected > 0;
        }
        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
            {
                string query = "SELECT 1 FROM People WHERE PersonID = @PersonID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    try
                    {
                        connection.Open();

                        // Use ExecuteScalar for a single value result
                        object result = command.ExecuteScalar();
                        isFound = result != null;
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection("YourConnectionStringHere"))
            {
                string query = "SELECT 1 FROM People WHERE NationalNo = @NationalNo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NationalNo", NationalNo);
                    try
                    {
                        connection.Open();

                        // Use ExecuteScalar for a single value result
                        object result = command.ExecuteScalar();
                        isFound = result != null;
                    }
                    catch (Exception)
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

    }
}
