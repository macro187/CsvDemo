using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace CsvDemo.Reporting.Tests
{
    [TestClass]
    public class AddressReporterTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateReport_Null_Addresses_Throws_ArgumentNullException()
        {
            AddressReporter.GenerateReport(null, new StringWriter());
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateReport_Null_Destination_Throws_ArgumentNullException()
        {
            AddressReporter.GenerateReport(new string[0], null);
        }


        [TestMethod]
        public void GenerateReport_Generates_Correct_Report()
        {
            var addresses =
                new[] {
                    "1 Cantaloupe Cres",
                    "3 Banana Blvd",
                    "2 Banana Blvd",
                    "4 Apple Ave" };
            
            var correctReport =
                string.Join(
                    Environment.NewLine,
                    "Address",
                    "4 Apple Ave",
                    "2 Banana Blvd",
                    "3 Banana Blvd",
                    "1 Cantaloupe Cres");

            var stringWriter = new StringWriter();

            AddressReporter.GenerateReport(addresses, stringWriter);

            Assert.AreEqual(correctReport.Trim(), stringWriter.ToString().Trim());
        }

    }
}
