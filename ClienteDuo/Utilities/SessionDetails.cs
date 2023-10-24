using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;

namespace ClienteDuo.Utilities
{
    public sealed class SessionDetails
    {
        public static int id;
        public static string username;
        public static string email;
        public static bool isGuest;
    }
}
