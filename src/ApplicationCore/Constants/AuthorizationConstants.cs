namespace SAED.ApplicationCore.Constants
{
    public class AuthorizationConstants
    {
        public static class Roles
        {
            public const string Superuser = "Superuser";

            public const string Administrador = "Administrador";

            public const string Aplicador = "Aplicador";
        }

        public static class Areas
        {
            public const string Administrador = "Administrador";
        }

        public static class CustomClaimTypes
        {
            public const string Permission = "Permission";
        }

        public static class Permissions
        {
            public static class Grupos
            {
                public const string View = "Permissions.Grupos.View";
                public const string Create = "Permissions.Grupos.Create";
                public const string Update = "Permissions.Grupos.Update";
                public const string Delete = "Permissions.Grupos.Delete";
            }

            public static class Usuarios
            {
                public const string View = "Permissions.Usuarios.View";
                public const string Create = "Permissions.Usuarios.Create";
                public const string Update = "Permissions.Usuarios.Update";
                public const string Delete = "Permissions.Usuarios.Delete";
            }

            public static class Users
            {
                public const string View = "Permissions.Users.View";
            }

            public static class Dashboard
            {
                public const string View = "Permissions.Dashboard.View";
            }

            public static class Avaliacoes
            {
                public const string View = "Permissions.Avaliacoes.View";
                public const string Create = "Permissions.Avaliacoes.Create";
                public const string Update = "Permissions.Avaliacoes.Update";
                public const string Delete = "Permissions.Avaliacoes.Delete";
            }

            public static class Distritos
            {
                public const string View = "Permissions.Distritos.View";
                public const string Create = "Permissions.Distritos.Create";
                public const string Update = "Permissions.Distritos.Update";
                public const string Delete = "Permissions.Distritos.Delete";
            }

            public static class Escolas
            {
                public const string View = "Permissions.Escolas.View";
                public const string Create = "Permissions.Escolas.Create";
                public const string Update = "Permissions.Escolas.Update";
                public const string Delete = "Permissions.Escolas.Delete";
            }

            public static class Disciplinas
            {
                public const string View = "Permissions.Disciplinas.View";
                public const string Create = "Permissions.Disciplinas.Create";
                public const string Update = "Permissions.Disciplinas.Update";
                public const string Delete = "Permissions.Disciplinas.Delete";
            }

            public static class Temas
            {
                public const string View = "Permissions.Temas.View";
                public const string Create = "Permissions.Temas.Create";
                public const string Update = "Permissions.Temas.Update";
                public const string Delete = "Permissions.Temas.Delete";
            }

            public static class Descritores
            {
                public const string View = "Permissions.Descritores.View";
                public const string Create = "Permissions.Descritores.Create";
                public const string Update = "Permissions.Descritores.Update";
                public const string Delete = "Permissions.Descritores.Delete";
            }

            public static class Questoes
            {
                public const string View = "Permissions.Questoes.View";
                public const string Create = "Permissions.Questoes.Create";
                public const string Update = "Permissions.Questoes.Update";
                public const string Delete = "Permissions.Questoes.Delete";
            }

            public static class Alternativas
            {
                public const string View = "Permissions.Alternativas.View";
                public const string Create = "Permissions.Alternativas.Create";
                public const string Update = "Permissions.Alternativas.Update";
                public const string Delete = "Permissions.Alternativas.Delete";
            }
        }

        public static class Database
        {
            public static int StartValueId = 100;

            public const string DefaultPassword = "123qwe";
        }

        public const string DefaultCorsPolicyName = "localhost";

        public const string Remember = "rmb";
    }
}
