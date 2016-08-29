using CsvHelper;
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


        static void Main2()
        {
            //
            // Process records from CSV file, maintaining cumulative information required for
            // reports
            //
            var nameTallies = new Dictionary<string, int>();
            var uniqueAddresses = new HashSet<string>();
            using (var textReader = File.OpenText(csvPath))
            {
                foreach (var p in new CsvReader(textReader).GetRecords<Person>())
                {
                    // Track names and their frequencies
                    if (!nameTallies.ContainsKey(p.FirstName)) nameTallies.Add(p.FirstName, 0);
                    nameTallies[p.FirstName]++;
                    if (!nameTallies.ContainsKey(p.LastName)) nameTallies.Add(p.LastName, 0);
                    nameTallies[p.LastName]++;

                    // Track unique addresses
                    uniqueAddresses.Add(p.Address);
                }
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
                    .Select(a => new {
                        Address = a.Number.ToString(CultureInfo.InvariantCulture) + " " + a.Street });

            //
            // Produce reports
            //
            using (var textWriter = new StreamWriter(frequencyReportPath))
            {
                new CsvWriter(textWriter).WriteRecords(sortedNameFrequencies);
            }
            
            using (var textWriter = new StreamWriter(addressReportPath))
            {
                new CsvWriter(textWriter).WriteRecords(sortedAddresses);
            }
        }

    }
}
