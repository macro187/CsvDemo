using CsvHelper;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using CsvDemo.Analysers;
using CsvDemo.Reporting;

namespace CsvDemo.Console
{
    class Program
    {
        
        /// <summary>
        /// Full path to the CSV input file
        /// </summary>
        static string csvPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "test-data.csv");


        /// <summary>
        /// Full path to the frequency report output file
        /// </summary>
        static string frequencyReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "frequency-report.txt");


        /// <summary>
        /// Full path to the address report output file
        /// </summary>
        static string addressReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "address-report.txt");


        /// <summary>
        /// Program entry point
        /// </summary>
        /// <remarks>
        /// Invokes program logic in <see cref="Main2()"/>, routing trace/debug output to stderr, handling exceptions,
        /// and returning an appropriate process exit code depending on success or failure.
        /// </remarks>
        /// <returns>
        /// Exit code 0 on success
        /// - OR -
        /// Non-zero exit code on failure
        /// </returns>
        static int Main()
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new ConsoleTraceListener(true));

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


        /// <summary>
        /// Program logic
        /// </summary>
        static void Main2()
        {
            // Use a CSV reader to read records about people from the CSV file, and a people analyser to collect
            // aggregate information about them
            var analyser = new PeopleAnalyser();
            using (var textReader = File.OpenText(csvPath))
            {
                foreach (var person in new CsvReader(textReader).GetRecords<Person>())
                {
                    analyser.Process(person);
                }
            }

            // Use report generator(s) to produce output report file(s) from information collected by the people
            // analyser
            using (var textWriter = new StreamWriter(frequencyReportPath))
            {
                NameFrequencyReporter.GenerateReport(analyser.NameTallies, textWriter);
            }
            using (var textWriter = new StreamWriter(addressReportPath))
            {
                AddressReporter.GenerateReport(analyser.UniqueAddresses, textWriter);
            }
        }

    }
}
