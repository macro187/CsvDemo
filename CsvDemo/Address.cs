using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CsvDemo
{

    /// <summary>
    /// A person's address
    /// </summary>
    /// <remarks>
    /// This type is designed to represent the entire address as a single string, reflecting the example CSV format, but
    /// also to have additional properties that provide access to individual address components where available.
    /// </remarks>
    public class Address
        : IEquatable<Address>
    {

        /// <summary>
        /// Initialise a new address object
        /// </summary>
        /// <param name="value">
        /// </param>
        public Address(string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            this.value = value;
            this.StreetName = value;
            this.StreetNumber = null;
            Parse(value);
        }


        /// <summary>
        /// The street number, if one is present in the full address
        /// </summary>
        public int? StreetNumber { get; private set; }


        /// <summary>
        /// The street name, if one is present in the full address
        /// </summary>
        public string StreetName { get; private set; }


        void Parse(string value)
        {
            var m = Regex.Match(value, @"^(\d+)\s+(.*)$");
            if (!m.Success) return;
            StreetNumber = int.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
            StreetName = m.Groups[2].Value;
        }


        string value;



        #region IEquatable<Address>

        public bool Equals(Address that)
        {
            return
                !object.ReferenceEquals(that, null) &&
                ToString() == that.ToString();
        }

        #endregion



        #region System.Object

        /// <summary>
        /// The full address
        /// </summary>
        public override string ToString()
        {
            return value;
        }


        /// <inheritdoc/>
        public override bool Equals(object that)
        {
            return Equals(that as Address);
        }


        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return
                typeof(Address).GetHashCode() ^
                ToString().GetHashCode();
        }

        #endregion



        #region Operators

        public static bool operator==(Address a1, Address a2)
        {
            if (object.ReferenceEquals(a1, a2)) return true;
            if (object.ReferenceEquals(a1, null)) return false;
            return a1.Equals(a2);
        }


        public static bool operator!=(Address a1, Address a2)
        {
            return !(a1 == a2);
        }


        public static implicit operator Address(string s)
        {
            if (s == null) return null;
            return new Address(s);
        }


        public static implicit operator string(Address a)
        {
            if (a == null) return null;
            return a.ToString();
        }

        #endregion

    }
}
