using System;

namespace CsvDemo.AddressAnalyserConsole
{

    /// <summary>
    /// Address data structure that is immutable and value-comparable
    /// </summary>
    class Address
        : IEquatable<Address>
    {

        public Address(int streetNumber, string streetName, string city, string state, string postcode)
        {
            StreetNumber = streetNumber;
            StreetName = streetName ?? "";
            City = city ?? "";
            State = state ?? "";
            Postcode = postcode ?? "";
        }


        public int StreetNumber { get; }
        public string StreetName { get; }
        public string City { get; }
        public string State { get; }
        public string Postcode { get; }


        public bool Equals(Address that)
        {
            return
                !object.ReferenceEquals(that, null) &&
                StreetNumber == that.StreetNumber &&
                StreetName == that.StreetName &&
                City == that.City &&
                State == that.State &&
                Postcode == that.Postcode;
        }


        public override bool Equals(object that)
        {
            return Equals(that as Address);
        }


        public static bool operator==(Address a1, Address a2)
        {
            if (object.ReferenceEquals(a1, a2)) return true;
            if (object.ReferenceEquals(a1, null) || object.ReferenceEquals(a2, null)) return false;
            return a1.Equals(a2);
        }


        public static bool operator!=(Address a1, Address a2)
        {
            return !(a1 == a2);
        }


        public override int GetHashCode()
        {
            return
                typeof(Address).GetHashCode() ^
                StreetNumber.GetHashCode() ^
                StreetName.GetHashCode() ^
                City.GetHashCode() ^
                State.GetHashCode() ^
                Postcode.GetHashCode();
        }

    }
}
