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
        int partyCode;
        readonly string hostUser = "camilo";
        Lobby lobby;

        [TestInitialize]
        public void Init()
        {
            lobby = new Lobby();
            partyCode = lobby.CreateNewParty(hostUser);
        }

        [TestCleanup]
        public void Cleanup()
        {
            lobby.CloseParty(partyCode);
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
            Assert.IsTrue(joinParty.IsPartyCodeExistent(partyCode));
        }

        [TestMethod()]
        public void IsPartyCodeNotExistentTest()
        {
            JoinParty joinParty = new JoinParty();
            int nonExistentPartyCode = partyCode - 1;
            Assert.IsFalse(joinParty.IsPartyCodeExistent(nonExistentPartyCode));
        }

        [TestMethod()]
        public void IsPartySpaceAvailableTest()
        {
            JoinParty joinParty = new JoinParty();
            Assert.IsTrue(joinParty.IsSpaceAvailable(partyCode));
        }

        [TestMethod()]
        public void IsPartyFullTest()
        {
            JoinParty joinParty = new JoinParty();
            string player1 = "pepe1109";
            string player2 = "jorgejuan22";
            string player3 = "towngameplay";

            InviteeLobby inviteeLobby = new InviteeLobby();
            inviteeLobby.JoinGame(partyCode, player1);

            InviteeLobby inviteeLobby2 = new InviteeLobby();
            inviteeLobby2.JoinGame(partyCode, player2);

            InviteeLobby inviteeLobby3 = new InviteeLobby();
            inviteeLobby3.JoinGame(partyCode, player3);

            Assert.IsFalse(joinParty.IsSpaceAvailable(partyCode));
        }
    }
}