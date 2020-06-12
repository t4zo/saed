using System.Collections.Generic;

namespace SAED.ApplicationCore.Constants
{
    public class AuthorizationConstants
    {
        public static class Roles
        {
            public const string ADMINISTRATOR = "Administrator";

            public const string APLICADOR = "APLICADOR";

            public static IEnumerable<string> ALL = new List<string> { ADMINISTRATOR, APLICADOR };
        }

        public static int DatabaseIdStartValue = 100;

        public const string REMEMBER = "rmb";

        public const string DEFAULT_PASSWORD = "123qwe";
    }
}
