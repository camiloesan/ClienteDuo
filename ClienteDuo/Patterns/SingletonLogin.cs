using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDuo.Patterns
{
    public sealed class SingletonLogin
    {
        private static SingletonLogin instance = null;
        private string username;
        private string email;

        private SingletonLogin() { }

        public static SingletonLogin Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SingletonLogin();
                }
                return instance;
            }
        }

        public static void CleanSession()
        {
            instance = null;
        }


    }
}
