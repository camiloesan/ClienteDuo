using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using ClienteDuo.DataService;
using System.Security.Principal;

namespace ClienteDuo.Pages.Tests
{
    [TestClass()]
    public class JoinPartyTests
    {
        int _partyCode;
        readonly string _hostUsername = "camilo";
        private Lobby _lobby;

        [TestInitialize]
        public void Init()
        {
            _lobby = new Lobby();
            _partyCode = _lobby.CreateNewParty(_hostUsername);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _lobby.CloseParty(_partyCode);
        }

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
            Assert.IsTrue(joinParty.IsPartyCodeExistent(_partyCode));
        }

        [TestMethod()]
        public void IsPartyCodeNotExistentTest()
        {
            JoinParty joinParty = new JoinParty();
            int nonExistentPartyCode = _partyCode - 1;
            Assert.IsFalse(joinParty.IsPartyCodeExistent(nonExistentPartyCode));
        }

        [TestMethod()]
        public void IsPartySpaceAvailableTest()
        {
            JoinParty joinParty = new JoinParty();
            Assert.IsTrue(joinParty.IsSpaceAvailable(_partyCode));
        }

        [TestMethod()]
        public void IsPartyFullTest()
        {
            JoinParty joinParty = new JoinParty();
            string player1 = "pepe1109";
            string player2 = "jorgejuan22";
            string player3 = "towngameplay";

            var inviteeLobby = new Lobby();
            inviteeLobby.JoinGame(_partyCode, player1);

            var inviteeLobby2 = new Lobby();
            inviteeLobby2.JoinGame(_partyCode, player2);

            var inviteeLobby3 = new Lobby();
            inviteeLobby3.JoinGame(_partyCode, player3);

            Assert.IsFalse(joinParty.IsSpaceAvailable(_partyCode));
        }

        [TestMethod()]
        public void IsUserBlockedByPlayerInLobbyTrueTest()
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            NewAccount newAccount = new NewAccount();
            string player0Username = "pepe0142";
            string player1Username = "pepe1109";
            newAccount.AddUserToDatabase(player0Username, "host@gmail.com", "Tokyo11!23");
            newAccount.AddUserToDatabase(player1Username, "player1mail@gmail.com", "Tokyo11!23");

            Lobby lobby = new Lobby();
            lobby.JoinGame(_partyCode, player0Username);

            usersManagerClient.BlockUserByUsername(player0Username, player1Username);
            JoinParty joinParty = new JoinParty();
            bool result = joinParty.IsUserBlockedByPlayerInParty(player1Username, _partyCode);

            usersManagerClient.DeleteUserFromDatabaseByUsername(player0Username);
            usersManagerClient.DeleteUserFromDatabaseByUsername(player1Username);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsUserBlockedByPlayerInLobbyFalseTest()
        {
            JoinParty joinParty = new JoinParty();
            bool result = joinParty.IsUserBlockedByPlayerInParty("romeo", _partyCode);

            Assert.IsFalse(result);
        }
    }
}