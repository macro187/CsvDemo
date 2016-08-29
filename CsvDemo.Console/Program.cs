using CsvHelper;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using CsvDemo.Analysers;
using CsvDemo.Reporting;

namespace CsvDemo.Console
{
    class Program
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
            // Route trace and debug output to stderr
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new ConsoleTraceListener(true));

            // Run the rest of the program, handling exceptions and returning appropriate process exit codes
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
            // Use a people analyser to collection information about people in the input CSV file
            var analyser = new PeopleAnalyser();
            using (var textReader = File.OpenText(csvPath))
            {
                foreach (var person in new CsvReader(textReader).GetRecords<Person>())
                {
                    analyser.Process(person);
                }
            }

            // Use report generators to produce output report file(s) from information collected by the people analyser
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
