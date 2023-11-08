using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ClienteDuo.DataService;

namespace ClienteDuo.Pages.Tests
{
    [TestClass()]
    public class LobbyTests
    {
        [TestMethod()]
        public void CreateNewPartyTest()
        {
            var mockNewParty = new Mock<IPartyManagerCallback>();
            Lobby lobby = new Lobby();
            lobby.CreateNewParty("host", mockNewParty.Object);
            mockNewParty.Verify(x => x.NotifyPartyCreated(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }
    }
}