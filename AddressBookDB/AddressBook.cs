using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AddressBookDB
{
    class AddressBook
    {
        private string connectionPath = ConfigurationManager.ConnectionStrings["dbConString"].ConnectionString;
        private SqlConnection con = new SqlConnection();
        /// <summary>
        /// Create Contact in AddressBook
        /// </summary>
        public void CreateAddressBookContact()
        {
            Console.Write("FirstName: ");
            string fname = Console.ReadLine();
            Console.Write("LastName: ");
            string lname = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("City: ");
            string city = Console.ReadLine();
            Console.Write("State: ");
            string state = Console.ReadLine();
            Console.Write("ZIP: ");
            string zip = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            con.ConnectionString = connectionPath;
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = con;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "createRecord";

            sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = fname;
            sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = lname;
            sqlCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = address;
            sqlCommand.Parameters.Add("@City", SqlDbType.VarChar).Value = city;
            sqlCommand.Parameters.Add("@State", SqlDbType.VarChar).Value = state;
            sqlCommand.Parameters.Add("@Zip", SqlDbType.VarChar).Value = zip;
            sqlCommand.Parameters.Add("@Phone", SqlDbType.VarChar).Value = phone;
            sqlCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
            
            try
            {
                con.Open();
                int count = sqlCommand.ExecuteNonQuery();
                if (count == -1)
                    Console.WriteLine($"Contact Created Succesfully...");
                else
                    Console.WriteLine($"Failed to Create AddressBook...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Edit AddressBook Contact 
        /// </summary>
        public void EditAddressBookContact()
        {
            Console.Write("Enter Name to Update Contact: ");
            string uname = Console.ReadLine();
            Console.Write("FirstName: ");
            string fname = Console.ReadLine();
            Console.Write("LastName: ");
            string lname = Console.ReadLine();
            Console.Write("Address: ");
            string address = Console.ReadLine();
            Console.Write("City: ");
            string city = Console.ReadLine();
            Console.Write("State: ");
            string state = Console.ReadLine();
            Console.Write("ZIP: ");
            string zip = Console.ReadLine();
            Console.Write("Phone: ");
            string phone = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();
            con.ConnectionString = connectionPath;
            SqlCommand sqlCommand = new SqlCommand($"UPDATE addressBook SET FirstName='{fname}', LastName='{lname}', Address='{address}', City='{city}',State='{state}', Zip='{zip}', Phone='{phone}',Email='{email}' where FirstName='{uname}'", con);
            try
            {
                con.Open();
                int count = sqlCommand.ExecuteNonQuery();
                if (count == 1)
                    Console.WriteLine($"Contact Updated Succesfully...");
                else
                    Console.WriteLine($"Failed to Update Contact...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Delete Contact using Name 
        /// </summary>
        public void DeleteAddressBookContact()
        {
            Console.Write("Enter Name for Delete Contact: ");
            string name = Console.ReadLine();
            con.ConnectionString = connectionPath;
            SqlCommand sqlCommand = new SqlCommand($"delete from addressBook where Firstname = '{name}'", con);
            try
            {
                con.Open();
                int count = sqlCommand.ExecuteNonQuery();
                if (count == 1)
                    Console.WriteLine($"Contact Deleted Succesfully...");
                else
                    Console.WriteLine($"Failed to Delete Contact...");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                con.Close();
            }
        }
        /// <summary>
        /// Search Record using City
        /// </summary>
        public void SearchAddressBookContact()
        {
            Console.Write("Enter City or State to Find Contact: ");
            string find = Console.ReadLine();
            int count = 0;
            con.ConnectionString = connectionPath;
            SqlCommand sqlCommand = new SqlCommand($"select * from addressBook where City = '{find}' OR State = '{find}'", con);
            try
            {
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Console.WriteLine("------------------------------------------------------------------------------------");
                while (reader.Read())
                {
                    count++;
                    Console.WriteLine($"Counts {count}: {reader["FirstName"]} {reader["LastName"]} {reader["Address"]} {reader["City"]} {reader["State"]} {reader["Zip"]} {reader["Phone"]} {reader["Email"]}");
                    Console.WriteLine("------------------------------------------------------------------------------------");
                }
            }
            catch (Exception e)
            {
                Console.Write($"Error: {e}");
            }
            finally
            {
                con.Close();
            }
        }
        public void ShowAddressBookContacts()
        {
            con.ConnectionString = connectionPath;
            SqlCommand sqlCommand = new SqlCommand($"select * from addressBook ORDER BY FirstName",con);
            try
            {
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                Console.WriteLine("--------------------------------------Contacts--------------------------------------");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]} {reader["Address"]} {reader["City"]} {reader["State"]} {reader["Zip"]} {reader["Phone"]} {reader["Email"]}");
                    Console.WriteLine("------------------------------------------------------------------------------------");
                }
            }
            catch(Exception e)
            {
                Console.Write($"Error: {e}");
            }
            finally
            {
                con.Close();
            }
        }
        public void Repeat()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("1.Create Contact 2.Edit Contact 3.Delete Contact 4.Search Contact 5. Show All Contacts 0.Exit");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        CreateAddressBookContact();
                        Repeat();
                        break;
                    case 2:
                        EditAddressBookContact();
                        Repeat();
                        break;
                    case 3:
                        DeleteAddressBookContact();
                        Repeat();
                        break;
                    case 4:
                        SearchAddressBookContact();
                        Repeat();
                        break;
                    case 5:
                        ShowAddressBookContacts();
                        Repeat();
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        repeat = false;
                        break;
                }
            }
        }
    }
}
