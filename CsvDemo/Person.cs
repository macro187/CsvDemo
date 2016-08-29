using System;

namespace CsvDemo
{
    /// <summary>
    /// A person with address and phone number contact information
    /// </summary>
    public class Person
    {

        /// <summary>
        /// Initialise a new person
        /// </summary>
        public Person()
        {
            FirstName = "";
            LastName = "";
            Address = "";
            PhoneNumber = "";
        }


        /// <summary>
        /// The person's first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("FirstName");
                firstName = value;
            }
        }
        string firstName;


        /// <summary>
        /// The person's last name
        /// </summary>
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("LastName");
                lastName = value;
            }
        }
        string lastName;


        /// <summary>
        /// The person's street address
        /// </summary>
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("Address");
                address = value;
            }
        }
        Address address;


        /// <summary>
        /// The person's phone number
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("PhoneNumber");
                phoneNumber = value;
            }
        }
        string phoneNumber;

    }
}
