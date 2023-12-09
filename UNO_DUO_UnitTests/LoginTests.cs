﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class LoginTests
    {
        [TestMethod()]
        public void IsUserNotLoggedInTest()
        {
            Login login = new Login(true);
            bool result = login.IsUserLoggedIn(1300);
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsUserLoggedInTest()
        {
            /*
            UserDTO user = new UserDTO
            {
                ID = 1
            };
            MainWindow mainWindow = new MainWindow();
            MainWindow.NotifyLogin(user);
            Login login = new Login(true);
            bool result = login.IsUserLoggedIn(1);
            MainWindow.NotifyLogOut(1, false);
            Assert.IsTrue(result);
            */
        }

        [TestMethod()]
        public void AreCredentialsValidCorrectTest()
        {
            string username = "demonslayer77";
            string email = "dprk@gmail.com";
            string password = "Tokyo2023!";

            NewAccount newAccount = new NewAccount();
            newAccount.AddUserToDatabase(username, email, password);

            Login login = new Login(true);
            UserDTO result = login.AreCredentialsValid(username, password);

            UsersManagerClient usersManagerClient = new UsersManagerClient();
            usersManagerClient.DeleteUserFromDatabaseByUsername(username);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void AreCredentialsValidIncorrectTest()
        {
            string username = "demonslayer77";
            string password = "Tokyo2023!";

            Login login = new Login(true);
            UserDTO result = login.AreCredentialsValid(username, password);

            Assert.IsNull(result);
        }
    }
}