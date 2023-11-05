using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace ClienteDuo.Pages.Tests
{


    [TestClass()]
    public class JoinPartyTests
    {
        [TestMethod()]
        public void InputIsNotIntegerTest()
        {
            JoinParty joinParty = new JoinParty();
            Assert.IsFalse(joinParty.IsInputInteger("inputString"));
        }

        [TestMethod()]
        public void InputIsIntegerTest()
        {
            JoinParty joinParty = new JoinParty();
            Assert.IsTrue(joinParty.IsInputInteger("1232"));
        }

        [TestMethod()]
        public void IsPartyCodeExistentTest()
        {
            JoinParty joinParty = new JoinParty();

        }

        [TestMethod()]
        public void IsPartyCodeNotExistentTest()
        {
            JoinParty joinParty = new JoinParty();

        }

        [TestMethod()]
        public void IsPartySpaceAvailableTest()
        {
            JoinParty joinParty = new JoinParty();

        }

        [TestMethod()]
        public void IsPartySpaceUnavailableTest()
        {
            JoinParty joinParty = new JoinParty();

        }
    }
}