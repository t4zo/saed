using System.Collections.Generic;

namespace SAED.ApplicationCore.Constants
{
    public class AuthorizationConstants
    {
        public static class Roles
        {
            public const string Superuser = "Superuser";

            public const string Administrador = "Administrador";

            public const string Aplicador = "Aplicador";

            public static IEnumerable<string> ALL = new List<string> { Administrador, Aplicador };
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

        public static class Database
        {
            public static int StartValueId = 100;

            public const string DefaultPassword = "123qwe";
        }

        public const string Remember = "rmb";
    }
}
