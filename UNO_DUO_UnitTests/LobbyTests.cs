using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDuo.Pages.Tests
{
    [TestClass()]
    public class LobbyTests
    {
        int openLobby;
        [TestMethod()]
        public void CreateNewPartySuccessTest()
        {
            Lobby lobby = new Lobby();
            openLobby = lobby.CreateNewParty("host");
            Assert.IsNotNull(openLobby);
        }

        [TestMethod()]
        public void ClosePartySuccessTest()
        {
            Lobby Lobby = new Lobby();
            Lobby.CloseParty(openLobby);
        }
    }
}