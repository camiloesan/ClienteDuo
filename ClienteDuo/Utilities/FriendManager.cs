﻿using ClienteDuo.DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDuo.Utilities
{
    internal class FriendManager
    {
        readonly UsersManagerClient _usersManagerClient;

        public FriendManager() 
        {
            _usersManagerClient = new UsersManagerClient();
        }

        public bool SendFriendRequest(string usernameSender, string usernameReceiver)
        {
            usernameSender = usernameSender.Trim();
            usernameReceiver = usernameReceiver.Trim();

            bool result = false;
            if (usernameSender.Length > 0 && usernameReceiver.Length > 0)
            {
                result = _usersManagerClient.SendFriendRequest(usernameSender, usernameReceiver);
            }

            return result;
        }

        public bool AcceptFriendRequest(FriendRequest friendRequest)
        {
            return _usersManagerClient.AcceptFriendRequest(friendRequest);
        }

        public bool DeclineFriendRequest(FriendRequest friendRequest)
        {
            return _usersManagerClient.RejectFriendRequest(friendRequest.FriendRequestID);
        }

        public FriendRequest[] GetFriendRequestsByUserID(int userID)
        {
            return _usersManagerClient.GetFriendRequestsList(userID);
        }

        public Friendship[] GetFriendsListByUserID(int userID)
        {
            return _usersManagerClient.GetFriendsList(userID);
        }
    }
}