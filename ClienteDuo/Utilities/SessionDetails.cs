namespace ClienteDuo.Utilities
{
    public sealed class SessionDetails
    {
        private SessionDetails()
        {
        }

        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static bool IsGuest { get; set; } = true;
        public static string Email { get; set; }
        public static int PartyCode { get; set; }
    }
}
