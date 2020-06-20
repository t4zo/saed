using System.Collections.Generic;

namespace SAED.ApplicationCore.Constants
{
    public class AuthorizationConstants
    {
        public static class PermissionsAndRoles
        {
            public const string SUPERUSER = "SUPERUSER";

            public const string ADMINISTRADOR = "ADMINISTRADOR";

            public const string APLICADOR = "APLICADOR";

            public static IEnumerable<string> ALL = new List<string> { ADMINISTRADOR, APLICADOR };
        }

        public static class Areas
        {
            public const string Administrador = "Administrador";
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

            public static class Avaliacoes
            {
                public const string View = "Permissions.Avaliacoes.View";
            }

            public static class Escolas
            {
                public const string View = "Permissions.Escolas.View";
                public const string Create = "Permissions.Escolas.Create";
                public const string Update = "Permissions.Escolas.Update";
                public const string Delete = "Permissions.Escolas.Delete";
            }
        }

        public static int DatabaseIdStartValue = 100;

        public const string REMEMBER = "rmb";

        public const string DEFAULT_PASSWORD = "123qwe";
    }
}
