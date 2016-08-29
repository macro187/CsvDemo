using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CsvDemo.Tests
{
    [TestClass]
    public class AddressTests
    {

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Null_Throws_ArgumentNullException()
        {
            new Address(null);
        }


        [TestMethod]
        public void Constructor_Empty_Results_In_Correct_State()
        {
            var a = new Address("");
            Assert.AreEqual("", a.ToString());
            Assert.AreEqual(null, a.StreetNumber);
            Assert.AreEqual("", a.StreetName);
        }


        [TestMethod]
        public void Constructor_WithoutNumber_Results_In_Correct_State()
        {
            var a = new Address("Apple Ave");
            Assert.AreEqual("Apple Ave", a.ToString());
            Assert.AreEqual(null, a.StreetNumber);
            Assert.AreEqual("Apple Ave", a.StreetName);
        }


        [TestMethod]
        public void Constructor_WithNumber_Results_In_Correct_State()
        {
            var a = new Address("1 Apple Ave");
            Assert.AreEqual("1 Apple Ave", a.ToString());
            Assert.AreEqual(1, a.StreetNumber);
            Assert.AreEqual("Apple Ave", a.StreetName);
        }


        [TestMethod]
        public void Equals_Null_Is_False()
        {
            Assert.IsFalse(new Address("").Equals((Address)null));
        }


        [TestMethod]
        public void Equals_Same_Is_True()
        {
            Assert.IsTrue(new Address("a").Equals(new Address("a")));
        }


        [TestMethod]
        public void Equals_Different_Is_False()
        {
            Assert.IsFalse(new Address("a").Equals(new Address("b")));
        }


        [TestMethod]
        public void ToString_Is_Correct()
        {
            Assert.AreEqual("a", new Address("a").ToString());
        }


        [TestMethod]
        public void ObjectEquals_Null_Is_False()
        {
            Assert.IsFalse(new Address("").Equals((object)null));
        }


        [TestMethod]
        public void ObjectEquals_Same_Is_True()
        {
            Assert.IsTrue(new Address("a").Equals((object)new Address("a")));
        }


        [TestMethod]
        public void ObjectEquals_Different_Is_False()
        {
            Assert.IsFalse(new Address("a").Equals((object)new Address("b")));
        }


        [TestMethod]
        public void GetHashCode_Of_Equal_Are_Equal()
        {
            Assert.AreEqual(new Address("a").GetHashCode(), new Address("a").GetHashCode());
        }


        [TestMethod]
        public void Null_OperatorEquals_Null_Is_True()
        {
            Assert.IsTrue((Address)null == (Address)null);
        }


        [TestMethod]
        public void NonNull_OperatorEquals_Null_Is_False()
        {
            Assert.IsFalse(new Address("a") == (Address)null);
        }


        [TestMethod]
        public void OperatorEquals_Same_Is_True()
        {
            Assert.IsTrue(new Address("a") == new Address("a"));
        }


        [TestMethod]
        public void OperatorEquals_Different_Is_False()
        {
            Assert.IsFalse(new Address("a") == new Address("b"));
        }


        [TestMethod]
        public void Implicit_To_String_Works()
        {
            string s = new Address("a");
            Assert.AreEqual("a", s);
        }


        [TestMethod]
        public void Implicit_From_String_Works()
        {
            Address a = "a";
            Assert.AreEqual("a", a.ToString());
        }

    }
}
