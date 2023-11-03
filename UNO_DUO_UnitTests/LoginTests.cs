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
    public class LoginTests
    {
        string initializedUsername = "demonslayer77";
        string initializedEmail = "dprk@gmail.com";
        string initializedPassword = "Tokyo2023!";

        [TestInitialize]
        public void Init()
        {
            NewAccount newAccount = new NewAccount();
            newAccount.AddUserToDatabase(initializedUsername, initializedEmail, initializedPassword);
        }

        [TestCleanup]
        public void Cleanup()
        {
            NewAccount newAccount = new NewAccount();
            newAccount.DeleteUserFromDatabaseByUsername(initializedUsername);
        }

        [TestMethod()]
        public void CredentialsValidReturnsObjectTest()
        {
            Login login = new Login();
            Assert.IsNotNull(login.AreCredentialsValid(initializedUsername, initializedPassword));
        }

        [TestMethod()]
        public void CredentialsInvalidPasswordTest()
        {
            Login login = new Login();
            Assert.IsNull(login.AreCredentialsValid(initializedUsername, "robertBird"));
        }

        [TestMethod()]
        public void CredentialsInvalidUsername()
        {
            Login login = new Login();
            Assert.IsNull(login.AreCredentialsValid("theStrokesFan", initializedPassword));
        }
    }
}