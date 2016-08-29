using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvDemo.Reporting
{

    /// <summary>
    /// Name-frequency report generator
    /// </summary>
    /// <remarks>
    /// Generates name-frequency reports in CSV format with results sorted by descending frequency and then by ascending
    /// name.
    /// </remarks>
    public static class NameFrequencyReporter
    {

        /// <summary>
        /// Generate a report
        /// </summary>
        /// <param name="nameTallies">
        /// The name-count tally to base the report on
        /// </param>
        /// <param name="destination">
        /// The <see cref="TextWriter"/> to write the report to
        /// </param>
        public static void GenerateReport(IReadOnlyDictionary<string, int> nameTallies, TextWriter destination)
        {
            if (nameTallies == null) throw new ArgumentNullException("nameTallies");
            if (destination == null) throw new ArgumentNullException("destination");

            var sortedNameFrequencies =
                nameTallies
                    .Select(kvp => new { Name = kvp.Key, Frequency = kvp.Value })
                    .OrderByDescending(tally => tally.Frequency)
                    .ThenBy(tally => tally.Name);

            new CsvWriter(destination).WriteRecords(sortedNameFrequencies);
        }

    }
}
