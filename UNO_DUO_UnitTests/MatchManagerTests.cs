using ClienteDuo.DataService;
using ClienteDuo.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace UNO_DUO_UnitTests
{
    /// <summary>
    /// Summary description for MatchManagerTest
    /// </summary>
    [TestClass]
    public class MatchManagerTests
    {
        private static int _partyCode;
        private static string[] _players = { "Xavi", "Juan", "Alejandro" };
        private static Random _numberGenerator = new Random();
        private static TestGameTable _gameTable = new TestGameTable();
        private static InstanceContext _context = new InstanceContext(_gameTable);
        private static MatchManagerClient _client = new MatchManagerClient(_context);

        [ClassInitialize()]
        public void Init()
        {
            _partyCode = _numberGenerator.Next(0, 10000);
            TestGameTable.PartyCode = _partyCode;

            foreach (string player in _players)
            {
                _client.Subscribe(_partyCode, player);
            }
        }

        [ClassCleanup()]
        public void Cleanup()
        {
            _client.EndGame(_partyCode);
        }

        [TestMethod()]
        public void SubscribeTest()
        {
            _client.Subscribe(_partyCode, "Jorge");
            List<string> playerList = new List<string>(_client.GetPlayerList(_partyCode));

            Assert.IsTrue(playerList.Contains<string>("Jorge"));
        }

        [TestMethod()]
        public void KickPlayerFromGameTest()
        {
            _client.KickPlayerFromGame(_partyCode, "Xavi", "Testing Kick player Method");
            List<string> playerList = new List<string>(_client.GetPlayerList(_partyCode));

            Assert.IsFalse(playerList.Contains<string>("Xavi"));
        }

        [TestMethod()]
        public void EndTurnTest()
        {
            string currentTurn = _client.GetCurrentTurn(_partyCode);
            _client.EndTurn(_partyCode);

            Assert.AreNotEqual(currentTurn, TestGameTable.CurrentTurn);
        }

        [TestMethod()]
        public void GetCurrentTurnTest()
        {
            List<string> playerList = new List<string>(_client.GetPlayerList(_partyCode));
            string currentTurn = _client.GetCurrentTurn(_partyCode);

            Assert.IsTrue(playerList.Contains(currentTurn));
        }

        [TestMethod()]
        public void GetMatchResultsTest()
        {
            List<string> playerList = new List<string>(_client.GetPlayerList(_partyCode));

            foreach (string player in playerList)
            {
                _client.SetGameScore(_partyCode, player, 0);
            }

            Dictionary<string, int> matchResults = _client.GetMatchResults(_partyCode);
            Assert.AreEqual(playerList.Count, matchResults.Count);
        }
    }
}
