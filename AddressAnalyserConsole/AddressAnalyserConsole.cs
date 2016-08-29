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
            var people =
                // ...that lazily reads lines from the CSV file
                File.ReadLines(csvPath)
                    // ...skipping the header
                    .Skip(1)
                    // ...splits apart the individual fields
                    .Select(line => line.Split(','))
                    // ...and packs them up as record objects based on a known field order
                    .Select(fields =>
                        new Person() {
                            FirstName = fields[0],
                            LastName = fields[1],
                            Address = fields[2],
                            PhoneNumber = fields[3]});

            //
            // Process records, maintaining cumulative information required for reports
            //
            var nameTallies = new Dictionary<string, int>();
            var uniqueAddresses = new HashSet<string>();
            foreach (var p in people)
            {
                // ...names and their frequencies
                if (!nameTallies.ContainsKey(p.FirstName)) nameTallies.Add(p.FirstName, 0);
                nameTallies[p.FirstName]++;
                if (!nameTallies.ContainsKey(p.LastName)) nameTallies.Add(p.LastName, 0);
                nameTallies[p.LastName]++;

                // ...unique addresses
                uniqueAddresses.Add(p.Address);
            }

            //
            // Compute summary information for reports
            //
            var sortedNameFrequencies =
                nameTallies
                    .Select(kvp => new { Name = kvp.Key, Frequency = kvp.Value })
                    .OrderByDescending(tally => tally.Frequency)
                    .ThenBy(tally => tally.Name);

            var sortedAddresses =
                uniqueAddresses
                    .Select(address => address.Split(new char[] { ' ' }, 2))
                    .Select(a => new {
                        Number = int.Parse(a[0], CultureInfo.InvariantCulture),
                        Street = a[1] })
                    .OrderBy(a => a.Street)
                    .ThenBy(a => a.Number)
                    .Select(a => a.Number.ToString(CultureInfo.InvariantCulture) + " " + a.Street);

            //
            // Produce frequency report
            //
            File.WriteAllLines(
                frequencyReportPath,
                new string[] {}
                    .Concat(new[] { "Name,Frequency" })
                    .Concat(sortedNameFrequencies.Select(t =>
                        string.Join(
                            ",",
                            t.Name,
                            t.Frequency.ToString(CultureInfo.InvariantCulture)))));
            
            //
            // Produce address report
            //
            File.WriteAllLines(
                addressReportPath,
                new string[] {}
                    .Concat(new[] { "Address" })
                    .Concat(sortedAddresses));
        }

    }
}
