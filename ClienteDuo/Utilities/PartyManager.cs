using ClienteDuo.DataService;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDuo.Utilities
{
    internal class PartyManager
    {
        readonly DataService.PartyManagerClient _partyManagerClient;

        public PartyManager() 
        {
            InstanceContext _instanceContext = new InstanceContext(this);
            _partyManagerClient = new DataService.PartyManagerClient(_instanceContext);
        }

        public int CreateNewParty(string username, IPartyManagerCallback callback)
        {
            Random rand = new Random();
            SessionDetails.PartyCode = rand.Next(0, 10000);
            SessionDetails.Username = username;
            _partyManagerClient.NewParty(SessionDetails.PartyCode, SessionDetails.Username);
            if (callback != null)
            {
                MainWindow.ShowMessageBox("receibed");
            }

            return SessionDetails.PartyCode;
        }
    }
}
