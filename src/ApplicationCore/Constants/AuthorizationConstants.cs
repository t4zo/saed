using System.Collections.Generic;

namespace SAED.ApplicationCore.Constants
{
    public class AuthorizationConstants
    {
        public static class Roles
        {
            public const string SUPERUSER = "SUPERUSER";

            public const string ADMINISTRADOR = "ADMINISTRADOR";

            public const string APLICADOR = "APLICADOR";

            public static IEnumerable<string> ALL = new List<string> { ADMINISTRADOR, APLICADOR };
        }

        public static class CustomClaimTypes
        {
            public const string Permission = "permission";
        }

        public static class Permissions
        {
            public static class Users
            {
                public const string View = "Permissions.Users.View";
                public const string Test = "Permissions.Users.Test";
            }
        }

        public static int DatabaseIdStartValue = 100;

        public const string REMEMBER = "rmb";

        public const string DEFAULT_PASSWORD = "123qwe";
    }
}
