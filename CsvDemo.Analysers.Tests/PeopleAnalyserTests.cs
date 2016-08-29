using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CsvDemo.Analysers.Tests
{
    [TestClass]
    public class PeopleAnalyserTests
    {

        [TestMethod]
        public void Constructor_Works()
        {
            new PeopleAnalyser();
        }


        [TestMethod]
        public void Initial_NameTallies_Is_Correct()
        {
            Assert.AreEqual(0, new PeopleAnalyser().NameTallies.Count);
        }


        [TestMethod]
        public void Initial_UniqueAddresses_Is_Correct()
        {
            Assert.AreEqual(0, new PeopleAnalyser().UniqueAddresses.Count);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Process_Null_Throws_ArgumentNullException()
        {
            new PeopleAnalyser().Process(null);
        }


        [TestMethod]
        public void Process_Maintains_NameTallies_Correctly()
        {
            var analyser = new PeopleAnalyser();

            analyser.Process(new Person() { FirstName = "a", LastName = "b" });
            Assert.AreEqual(2, analyser.NameTallies.Count);
            Assert.AreEqual(1, analyser.NameTallies["a"]);
            Assert.AreEqual(1, analyser.NameTallies["b"]);

            analyser.Process(new Person() { FirstName = "a", LastName = "c" });
            Assert.AreEqual(3, analyser.NameTallies.Count);
            Assert.AreEqual(2, analyser.NameTallies["a"]);
            Assert.AreEqual(1, analyser.NameTallies["b"]);
            Assert.AreEqual(1, analyser.NameTallies["c"]);
        }


        [TestMethod]
        public void Process_Maintains_UniqueAddresses_Correctly()
        {
            var analyser = new PeopleAnalyser();

            analyser.Process(new Person() { Address = "a" });
            Assert.IsTrue(
                analyser.UniqueAddresses.AsEnumerable().OrderBy(a => a.ToString())
                .SequenceEqual(new Address[] { "a" }));

            analyser.Process(new Person() { Address = "a" });
            Assert.IsTrue(
                analyser.UniqueAddresses.AsEnumerable().OrderBy(a => a.ToString())
                .SequenceEqual(new Address[] { "a" }));

            analyser.Process(new Person() { Address = "b" });
            Assert.IsTrue(
                analyser.UniqueAddresses.AsEnumerable().OrderBy(a => a.ToString())
                .SequenceEqual(new Address[] { "a", "b" }));
        }

    }
}
