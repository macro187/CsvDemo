using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace CsvDemo.Reporting.Tests
{
    [TestClass]
    public class NameFrequencyReporterTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateReport_Null_NameTallies_Throws_ArgumentNullException()
        {
            NameFrequencyReporter.GenerateReport(null, new StringWriter());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateReport_Null_Destination_Throws_ArgumentNullException()
        {
            NameFrequencyReporter.GenerateReport(new Dictionary<string,int>(), null);
        }


        [TestMethod]
        public void GenerateReport_Generates_Correct_Report()
        {
            var tally =
                new Dictionary<string, int>() {
                    { "a", 1 },
                    { "c", 3 },
                    { "b", 3 },
                    { "d", 4 }};

            var correctReport =
                string.Join(
                    Environment.NewLine,
                    "Name,Frequency",
                    "d,4",
                    "b,3",
                    "c,3",
                    "a,1");

            var stringWriter = new StringWriter();

            NameFrequencyReporter.GenerateReport(tally, stringWriter);

            Assert.AreEqual(correctReport.Trim(), stringWriter.ToString().Trim());
        }

    }
}
