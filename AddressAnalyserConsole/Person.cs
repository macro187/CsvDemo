using System;

namespace CsvDemo.AddressAnalyserConsole
{

    /// <summary>
    /// A person with address and phone number contact information
    /// </summary>
    class Person
    {

        public Person()
        {
            FirstName = "";
            LastName = "";
            Address = "";
            PhoneNumber = "";
        }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }
}
