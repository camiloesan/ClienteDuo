using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteDuo.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteDuo.DataService;
using ClienteDuo.Utilities;

namespace ClienteDuo.Pages.Tests
{
    [TestClass()]
    public class ModifyProfileTests
    {
        string _initializedUsername = "demonslayer77";
        string _initializedEmail = "dprk@gmail.com";
        string _initializedPassword = "Tokyo2023!";

        [TestInitialize]
        public void Init()
        {
            UsersManager.AddUserToDatabase(_initializedUsername, _initializedEmail, _initializedPassword);
            SessionDetails.IsGuest = false;
            SessionDetails.UserId = UsersManager.AreCredentialsValid(_initializedUsername, _initializedPassword).ID;
        }

        [TestCleanup]
        public void Cleanup()
        {
            UsersManagerClient usersManagerClient = new UsersManagerClient();
            usersManagerClient.DeleteUserFromDatabaseByUsername(_initializedUsername);
            SessionDetails.UserId = 0;
            SessionDetails.IsGuest = true;
        }

        [TestMethod()]
        public void UpdateProfilePictureExistentUserTest()
        {
            int pictureId = 0;
            bool result = UsersManager.UpdateProfilePicture(SessionDetails.UserId, pictureId);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateProfilePictureNonExistentUserTest()
        {
            int pictureId = 0;
            bool result = UsersManager.UpdateProfilePicture(0, pictureId);
            Assert.IsFalse(result);
        }
    }
}