using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CsvDemo.Tests
{
    [TestClass]
    public class PersonTests
    {

        [TestMethod]
        public void Constructor_Works()
        {
            new Person();
        }


        [TestMethod]
        public void Initial_Property_Values_Are_Correct()
        {
            var person = new Person();
            Assert.AreEqual("", person.FirstName);
            Assert.AreEqual("", person.LastName);
            Assert.AreEqual("", person.Address.ToString());
            Assert.AreEqual("", person.PhoneNumber);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Setting_Null_FirstName_Throws_ArgumentNullException()
        {
            new Person().FirstName = null;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Setting_Null_LastName_Throws_ArgumentNullException()
        {
            new Person().LastName = null;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Setting_Null_Address_Throws_ArgumentNullException()
        {
            new Person().Address = null;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Setting_Null_PhoneNumber_Throws_ArgumentNullException()
        {
            new Person().PhoneNumber = null;
        }

    }
}
