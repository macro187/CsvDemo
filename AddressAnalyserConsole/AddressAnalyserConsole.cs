using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CsvDemo.AddressAnalyserConsole
{
    class AddressAnalyserConsole
    {
        
        /// <summary>
        /// Full path the to the CSV input file
        /// </summary>
        static string csvPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "test-data.csv");


        /// <summary>
        /// Full path the to frequency report output file
        /// </summary>
        static string frequencyReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "frequency-report.txt");


        /// <summary>
        /// Full path the to address report output file
        /// </summary>
        static string addressReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "address-report.txt");


        static int Main()
        {
            //
            // Route trace/debug output to stderr
            //
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new ConsoleTraceListener(true));

            //
            // Handle exceptions and return appropriate process exit codes
            //
            try
            {
                Main2();
                return 0;
            }
            catch (Exception e)
            {
                Debug.Print("");
                Debug.Print("An unhandled exception occurred:");
                Debug.Print(e.ToString());
                return 1;
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Maintainability",
            "CA1502:AvoidExcessiveComplexity",
            Justification = "It's an MVP, don't worry about it")]
        static void Main2()
        {
            //
            // Build an enumerable of records
            //
            var records =
                // ...that lazily reads lines from the CSV file
                File.ReadLines(csvPath)
                    // ...skipping the header
                    .Skip(1)
                    // ...splits apart the individual fields
                    .Select(line => line.Split(','))
                    // ...and packs them up as record objects based on a known field order
                    .Select(fields =>
                        new {
                            FirstName = fields[0],
                            LastName = fields[1],
                            Address = new Address(
                                int.Parse(fields[2], CultureInfo.InvariantCulture),
                                fields[3],
                                fields[4],
                                fields[5],
                                fields[6])});

            //
            // Process records, maintaining cumulative information required for reports
            //
            var firstNameTallies = new Dictionary<string, int>();
            var lastNameTallies = new Dictionary<string, int>();
            var uniqueAddresses = new HashSet<Address>();
            foreach (var r in records)
            {
                // ...first names and their frequencies
                if (!firstNameTallies.ContainsKey(r.FirstName)) firstNameTallies.Add(r.FirstName, 0);
                firstNameTallies[r.FirstName]++;

                // ...last names and their frequencies
                if (!lastNameTallies.ContainsKey(r.LastName)) lastNameTallies.Add(r.LastName, 0);
                lastNameTallies[r.LastName]++;

                // ...unique addresses
                uniqueAddresses.Add(r.Address);
            }

            //
            // Compute summary information for reports
            //
            var sortedFirstNameFrequencies =
                firstNameTallies
                    .Select(kvp => new { FirstName = kvp.Key, Frequency = kvp.Value })
                    .OrderByDescending(tally => tally.Frequency)
                    .ThenBy(tally => tally.FirstName);

            var sortedLastNameFrequencies =
                lastNameTallies
                    .Select(kvp => new { LastName = kvp.Key, Frequency = kvp.Value })
                    .OrderByDescending(tally => tally.Frequency)
                    .ThenBy(tally => tally.LastName);

            var sortedAddresses =
                uniqueAddresses
                    .OrderBy(a => a.StreetName)
                    .ThenBy(a => a.StreetNumber)
                    .ThenBy(a => a.City)
                    .ThenBy(a => a.State)
                    .ThenBy(a => a.Postcode);

            //
            // Produce frequency report
            //
            File.WriteAllLines(
                frequencyReportPath,
                new string[] {}
                    .Concat(new[] { "FirstName,Frequency" })
                    .Concat(sortedFirstNameFrequencies.Select(t =>
                        string.Join(
                            ",",
                            t.FirstName,
                            t.Frequency.ToString(CultureInfo.InvariantCulture))))
                    .Concat(new[] { "" })
                    .Concat(new[] { "LastName,Frequency" })
                    .Concat(sortedLastNameFrequencies.Select(t =>
                        string.Join(
                            ",",
                            t.LastName,
                            t.Frequency.ToString(CultureInfo.InvariantCulture)))));
            
            //
            // Produce address report
            //
            File.WriteAllLines(
                addressReportPath,
                new string[] {}
                    .Concat(new[] { "StreetNumber,StreetName,City,State,Postcode" })
                    .Concat(sortedAddresses.Select(a =>
                        string.Join(
                            ",",
                            a.StreetNumber.ToString(CultureInfo.InvariantCulture),
                            a.StreetName,
                            a.City,
                            a.State,
                            a.Postcode))));
        }

    }
}
