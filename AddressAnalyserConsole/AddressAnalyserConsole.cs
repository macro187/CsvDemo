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


        static int Main()
        {
            //
            // Route trace/debug output to stderr
            //
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
                            StreetNumber = int.Parse(fields[2], CultureInfo.InvariantCulture),
                            StreetName = fields[3],
                            City = fields[4],
                            State = fields[5],
                            Postcode = fields[6] });

            //
            // Process records, maintaining cumulative information required for reports
            //
            var firstNameTallies = new Dictionary<string, int>();
            var lastNameTallies = new Dictionary<string, int>();
            foreach (var r in records)
            {
                // ...first names and their frequencies
                if (!firstNameTallies.ContainsKey(r.FirstName)) firstNameTallies.Add(r.FirstName, 0);
                firstNameTallies[r.FirstName]++;

                // ...last names and their frequencies
                if (!lastNameTallies.ContainsKey(r.LastName)) lastNameTallies.Add(r.LastName, 0);
                lastNameTallies[r.LastName]++;
            }

            //
            // Summarise first and last names along with their frequencies, in order of decreasing frequency then
            // increasing name alphabetically
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

            //
            // Produce frequency report
            //
            File.WriteAllLines(
                frequencyReportPath,
                new string[] {}
                    .Concat(new string[] { "FirstName,Frequency" })
                    .Concat(sortedFirstNameFrequencies.Select(t =>
                        t.FirstName + "," + t.Frequency.ToString(CultureInfo.InvariantCulture)))
                    .Concat(new string[] { "" })
                    .Concat(new string[] { "LastName,Frequency" })
                    .Concat(sortedLastNameFrequencies.Select(t =>
                        t.LastName + "," + t.Frequency.ToString(CultureInfo.InvariantCulture))));
        }

    }
}
