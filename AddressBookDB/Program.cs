using System;

namespace AddressBookDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AddressBook DB!");
            AddressBook addressBook = new AddressBook();
            addressBook.Repeat();
        }
    }
}
