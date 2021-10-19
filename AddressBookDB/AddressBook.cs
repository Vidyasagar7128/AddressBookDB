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
        public void Repeat()
        {
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("1.Create Contact");
                int num = int.Parse(Console.ReadLine());
                switch (num)
                {
                    case 1:
                        CreateAddressBookContact();
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
