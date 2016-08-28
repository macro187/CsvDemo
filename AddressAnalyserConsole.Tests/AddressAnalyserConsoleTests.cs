using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace CsvDemo.AddressAnalyserConsole.Tests
{
    [TestClass]
    public class AddressAnalyserConsoleTests
    {

        /// <summary>
        /// Full path to the hand-written known-correct frequency report file
        /// </summary>
        static readonly string testFrequencyReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "test-frequency-report.txt");


        /// <summary>
        /// Full path to the frequency report file output by the .exe
        /// </summary>
        static readonly string frequencyReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "frequency-report.txt");


        /// <summary>
        /// Full path to the hand-written known-correct address report file
        /// </summary>
        static readonly string testAddressReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "test-address-report.txt");


        /// <summary>
        /// Full path to the address report file output by the .exe
        /// </summary>
        static readonly string addressReportPath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "address-report.txt");


        /// <summary>
        /// Full path to the .exe
        /// </summary>
        static readonly string exePath =
            Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "AddressAnalyserConsole.exe");


        /// <summary>
        /// Mutex to serialise access to the .exe because tests could potentially run in parallel
        /// </summary>
        /// <remarks>
        /// This wouldn't work if tests were somehow run in parallel across multiple processes
        /// </remarks>
        static object ExeLock = new object();


        [TestMethod]
        public void Program_Produces_Exit_Code_0()
        {
            lock (ExeLock)
            {
                using (var proc = Process.Start(exePath))
                {
                    proc.WaitForExit();
                    Assert.AreEqual(0, proc.ExitCode);
                }
            }
        }


        [TestMethod]
        public void Program_Produces_Correct_Frequency_Report()
        {
            lock (ExeLock)
            {
                using (var proc = Process.Start(exePath))
                {
                    proc.WaitForExit();
                    Assert.AreEqual(
                        File.ReadAllText(testFrequencyReportPath).Trim(),
                        File.ReadAllText(frequencyReportPath).Trim());
                }
            }
        }


        [TestMethod]
        public void Program_Produces_Correct_Address_Report()
        {
            lock (ExeLock)
            {
                using (var proc = Process.Start(exePath))
                {
                    proc.WaitForExit();
                    Assert.AreEqual(
                        File.ReadAllText(testAddressReportPath).Trim(),
                        File.ReadAllText(addressReportPath).Trim());
                }
            }
        }

    }
}
