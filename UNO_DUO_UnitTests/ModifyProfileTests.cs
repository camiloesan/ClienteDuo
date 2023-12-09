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
            NewAccount newAccount = new NewAccount();
            newAccount.AddUserToDatabase(_initializedUsername, _initializedEmail, _initializedPassword);
            Login login = new Login(true);
            SessionDetails.IsGuest = false;
            SessionDetails.UserId = login.AreCredentialsValid(_initializedUsername, _initializedPassword).ID;
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
            ModifyProfile modifyProfile = new ModifyProfile(true);
            int pictureId = 0;
            bool result = modifyProfile.UpdateProfilePicture(SessionDetails.UserId, pictureId);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void UpdateProfilePictureNonExistentUserTest()
        {
            ModifyProfile modifyProfile = new ModifyProfile(true);
            int pictureId = 0;
            bool result = modifyProfile.UpdateProfilePicture(0, pictureId);
            Assert.IsFalse(result);
        }
    }
}