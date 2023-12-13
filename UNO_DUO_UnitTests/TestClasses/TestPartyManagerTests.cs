using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteDuo.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClienteDuo.TestClasses.Tests
{
    [TestClass()]
    public class TestPartyManagerTests
    {
        [TestMethod()]
        public void PartyCreatedTest()
        {
            TestPartyManager testPartyManager = new TestPartyManager();
            int partyCode = 1111;

            testPartyManager.NotifyCreateParty(partyCode, "camilo");
            int expected = 1;
            Thread.Sleep(2000);
            int result = TestPartyManager.PlayersInParty.Count;
            testPartyManager.NotifyCloseParty(partyCode, "kick");
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void MessageReceivedTest()
        {
            TestPartyManager testPartyManager = new TestPartyManager();
            int partyCode = 1111;
            string expectedMessage = "helloWorld";
            testPartyManager.NotifyCreateParty(partyCode, "camilo");
            testPartyManager.NotifySendMessage("helloWorld");

            Thread.Sleep(2000);
            string result = TestPartyManager.ReceivedMessage;

            testPartyManager.NotifyCloseParty(partyCode, "kick");

            Assert.AreEqual(expectedMessage, result);
        }

        [TestMethod()]
        public void PlayerJoinedTest()
        {
            TestPartyManager testPartyManager = new TestPartyManager();
            int partyCode = 1111;
            testPartyManager.NotifyCreateParty(partyCode, "camilo");
            testPartyManager.NotifyJoinParty(partyCode, "christian");

            Thread.Sleep(2000);
            int result = TestPartyManager.PlayersInParty.Count;

            testPartyManager.NotifyCloseParty(partyCode, "kick");

            int expected = 2;
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void PlayerKickedTest()
        {
            TestPartyManager testPartyManager = new TestPartyManager();
            int partyCode = 1111;
            string playerToKick = "christian";
            testPartyManager.NotifyCreateParty(partyCode, "camilo");
            testPartyManager.NotifyJoinParty(partyCode, playerToKick);
            testPartyManager.NotifyKickPlayer(partyCode, playerToKick, "spam");

            Thread.Sleep(2000);
            string result = TestPartyManager.PlayerKickedReason;
            testPartyManager.NotifyCloseParty(partyCode, "kick");

            string expected = "spam";
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void PlayerLeftTest()
        {
            TestPartyManager testPartyManager = new TestPartyManager();
            int partyCode = 1111;
            string playerToKick = "christian";
            testPartyManager.NotifyCreateParty(partyCode, "camilo");
            testPartyManager.NotifyJoinParty(partyCode, playerToKick);
            testPartyManager.NotifyKickPlayer(partyCode, playerToKick, "spam");

            Thread.Sleep(2000);
            int result = TestPartyManager.PlayersInParty.Count;
            testPartyManager.NotifyCloseParty(partyCode, "kick");

            int expected = 1;
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GameStartedTest()
        {
            TestPartyManager testPartyManager = new TestPartyManager();
            int partyCode = 1111;
            string playerToKick = "christian";
            testPartyManager.NotifyCreateParty(partyCode, "camilo");
            testPartyManager.NotifyJoinParty(partyCode, playerToKick);
            testPartyManager.NotifyStartGame(partyCode);

            Thread.Sleep(2000);
            bool result = TestPartyManager.IsGameStarted;
            testPartyManager.NotifyCloseParty(partyCode, "kick");

            Assert.IsTrue(result);
        }
    }
}