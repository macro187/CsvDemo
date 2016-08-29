using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvDemo.Reporting
{

    /// <summary>
    /// Address reporter
    /// </summary>
    /// <remarks>
    /// Generates address reports in CSV format with results sorted by street name.
    /// </remarks>
    public static class AddressReporter
    {

        /// <summary>
        /// Generate a report
        /// </summary>
        /// <param name="addresses">
        /// The address list to base the report on
        /// </param>
        /// <param name="destination">
        /// The <see cref="TextWriter"/> to write the report to
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="addresses"/> was <c>null</c>
        /// - OR -
        /// <paramref name="destination"/> was <c>null</c>
        /// </exception>
        public static void GenerateReport(IEnumerable<string> addresses, TextWriter destination)
        {
            if (addresses == null) throw new ArgumentNullException("addresses");
            if (destination == null) throw new ArgumentNullException("destination");

            var sortedAddresses =
                addresses
                    .Select(address => address.Split(new char[] { ' ' }, 2))
                    .Select(a => new {
                        Number = int.Parse(a[0], CultureInfo.InvariantCulture),
                        Street = a[1] })
                    .OrderBy(a => a.Street)
                    .ThenBy(a => a.Number)
                    .Select(a => new {
                        Address = a.Number.ToString(CultureInfo.InvariantCulture) + " " + a.Street });

            new CsvWriter(destination).WriteRecords(sortedAddresses);
        }

    }
}
