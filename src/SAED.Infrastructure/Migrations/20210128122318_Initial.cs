using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace SAED.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    Name = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastLogin = table.Column<DateTime>("datetime2", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>("bit", nullable: false),
                    PasswordHash = table.Column<string>("nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>("bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>("bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>("bit", nullable: false),
                    AccessFailedCount = table.Column<int>("int", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Avaliacoes",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>("nvarchar(max)", nullable: false),
                    Status = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Avaliacoes", x => x.Id); });

            migrationBuilder.CreateTable(
                "Cpfs",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Cpfs", x => x.Id); });

            migrationBuilder.CreateTable(
                "Cursos",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    Sigla = table.Column<string>("nvarchar(8)", maxLength: 8, nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Cursos", x => x.Id); });

            migrationBuilder.CreateTable(
                "Disciplinas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                    Sigla = table.Column<string>("nvarchar(8)", maxLength: 8, nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Disciplinas", x => x.Id); });

            migrationBuilder.CreateTable(
                "Distritos",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(32)", maxLength: 32, nullable: false),
                    Zona = table.Column<int>("int", nullable: false),
                    Distancia = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Distritos", x => x.Id); });

            migrationBuilder.CreateTable(
                "Formas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(16)", maxLength: 16, nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Formas", x => x.Id); });

            migrationBuilder.CreateTable(
                "Turnos",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(32)", maxLength: 32, nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Turnos", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>("int", nullable: false),
                    ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>("int", nullable: false),
                    ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                    UserId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId = table.Column<int>("int", nullable: false),
                    RoleId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId = table.Column<int>("int", nullable: false),
                    LoginProvider = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Segmentos",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    Sigla = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: false),
                    CursoId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segmentos", x => x.Id);
                    table.ForeignKey(
                        "FK_Segmentos_Cursos_CursoId",
                        x => x.CursoId,
                        "Cursos",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Temas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplinaId = table.Column<int>("int", nullable: false),
                    Nome = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temas", x => x.Id);
                    table.ForeignKey(
                        "FK_Temas_Disciplinas_DisciplinaId",
                        x => x.DisciplinaId,
                        "Disciplinas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Escolas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inep = table.Column<int>("int", nullable: true),
                    MatrizId = table.Column<int>("int", nullable: true),
                    Nome = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    DistritoId = table.Column<int>("int", nullable: false),
                    Bairro = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: true),
                    Rua = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                    Numero = table.Column<int>("int", nullable: true),
                    Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    Telefone = table.Column<string>("nvarchar(11)", maxLength: 11, nullable: true),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escolas", x => x.Id);
                    table.ForeignKey(
                        "FK_Escolas_Distritos_DistritoId",
                        x => x.DistritoId,
                        "Distritos",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Escolas_Escolas_MatrizId",
                        x => x.MatrizId,
                        "Escolas",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Etapas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>("nvarchar(64)", maxLength: 64, nullable: false),
                    SegmentoId = table.Column<int>("int", nullable: false),
                    Normativa = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapas", x => x.Id);
                    table.ForeignKey(
                        "FK_Etapas_Segmentos_SegmentoId",
                        x => x.SegmentoId,
                        "Segmentos",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Descritores",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemaId = table.Column<int>("int", nullable: false),
                    Nome = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descritores", x => x.Id);
                    table.ForeignKey(
                        "FK_Descritores_Temas_TemaId",
                        x => x.TemaId,
                        "Temas",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Salas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscolaId = table.Column<int>("int", nullable: false),
                    Nome = table.Column<string>("nvarchar(max)", nullable: false),
                    Capacidade = table.Column<int>("int", nullable: false),
                    Area = table.Column<double>("float", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                    table.ForeignKey(
                        "FK_Salas_Escolas_EscolaId",
                        x => x.EscolaId,
                        "Escolas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AvaliacaoDisciplinasEtapas",
                table => new
                {
                    AvaliacaoId = table.Column<int>("int", nullable: false),
                    DisciplinaId = table.Column<int>("int", nullable: false),
                    EtapaId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoDisciplinasEtapas", x => new { x.DisciplinaId, x.AvaliacaoId, x.EtapaId });
                    table.ForeignKey(
                        "FK_AvaliacaoDisciplinasEtapas_Avaliacoes_AvaliacaoId",
                        x => x.AvaliacaoId,
                        "Avaliacoes",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_AvaliacaoDisciplinasEtapas_Disciplinas_DisciplinaId",
                        x => x.DisciplinaId,
                        "Disciplinas",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_AvaliacaoDisciplinasEtapas_Etapas_EtapaId",
                        x => x.EtapaId,
                        "Etapas",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Questoes",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescritorId = table.Column<int>("int", nullable: false),
                    EtapaId = table.Column<int>("int", nullable: false),
                    Item = table.Column<string>("nvarchar(8)", maxLength: 8, nullable: false),
                    Descricao = table.Column<string>("nvarchar(max)", nullable: true),
                    Enunciado = table.Column<string>("nvarchar(max)", nullable: false),
                    Habilitada = table.Column<bool>("bit", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questoes", x => x.Id);
                    table.ForeignKey(
                        "FK_Questoes_Descritores_DescritorId",
                        x => x.DescritorId,
                        "Descritores",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Questoes_Etapas_EtapaId",
                        x => x.EtapaId,
                        "Etapas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Turmas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaId = table.Column<int>("int", nullable: false),
                    EtapaId = table.Column<int>("int", nullable: false),
                    TurnoId = table.Column<int>("int", nullable: false),
                    FormaId = table.Column<int>("int", nullable: false),
                    Nome = table.Column<string>("nvarchar(32)", maxLength: 32, nullable: false),
                    Extinta = table.Column<bool>("bit", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turmas", x => x.Id);
                    table.ForeignKey(
                        "FK_Turmas_Etapas_EtapaId",
                        x => x.EtapaId,
                        "Etapas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Turmas_Formas_FormaId",
                        x => x.FormaId,
                        "Formas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Turmas_Salas_SalaId",
                        x => x.SalaId,
                        "Salas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Turmas_Turnos_TurnoId",
                        x => x.TurnoId,
                        "Turnos",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Alternativas",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>("nvarchar(max)", nullable: false),
                    Correta = table.Column<bool>("bit", nullable: false),
                    QuestaoId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativas", x => x.Id);
                    table.ForeignKey(
                        "FK_Alternativas_Questoes_QuestaoId",
                        x => x.QuestaoId,
                        "Questoes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AvaliacaoQuestao",
                table => new
                {
                    AvaliacoesId = table.Column<int>("int", nullable: false),
                    QuestoesId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoQuestao", x => new { x.AvaliacoesId, x.QuestoesId });
                    table.ForeignKey(
                        "FK_AvaliacaoQuestao_Avaliacoes_AvaliacoesId",
                        x => x.AvaliacoesId,
                        "Avaliacoes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AvaliacaoQuestao_Questoes_QuestoesId",
                        x => x.QuestoesId,
                        "Questoes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Alunos",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaId = table.Column<int>("int", nullable: false),
                    Nome = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: false),
                    CpfId = table.Column<int>("int", nullable: true),
                    Nascimento = table.Column<DateTime>("datetime2", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                    table.ForeignKey(
                        "FK_Alunos_Cpfs_CpfId",
                        x => x.CpfId,
                        "Cpfs",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Alunos_Turmas_TurmaId",
                        x => x.TurmaId,
                        "Turmas",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "RespostaAlunos",
                table => new
                {
                    AvaliacaoId = table.Column<int>("int", nullable: false),
                    AlunoId = table.Column<int>("int", nullable: false),
                    AlternativaId = table.Column<int>("int", nullable: false),
                    CreatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>("datetime2", nullable: false),
                    UpdatedBy = table.Column<string>("nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>("datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaAlunos", x => new { x.AvaliacaoId, x.AlunoId, x.AlternativaId });
                    table.ForeignKey(
                        "FK_RespostaAlunos_Alternativas_AlternativaId",
                        x => x.AlternativaId,
                        "Alternativas",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_RespostaAlunos_Alunos_AlunoId",
                        x => x.AlunoId,
                        "Alunos",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_RespostaAlunos_Avaliacoes_AvaliacaoId",
                        x => x.AvaliacaoId,
                        "Avaliacoes",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_Alternativas_QuestaoId",
                "Alternativas",
                "QuestaoId");

            migrationBuilder.CreateIndex(
                "IX_Alunos_CpfId",
                "Alunos",
                "CpfId");

            migrationBuilder.CreateIndex(
                "IX_Alunos_TurmaId",
                "Alunos",
                "TurmaId");

            migrationBuilder.CreateIndex(
                "IX_AspNetRoleClaims_RoleId",
                "AspNetRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_UserId",
                "AspNetUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_UserId",
                "AspNetUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_RoleId",
                "AspNetUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_AvaliacaoDisciplinasEtapas_AvaliacaoId",
                "AvaliacaoDisciplinasEtapas",
                "AvaliacaoId");

            migrationBuilder.CreateIndex(
                "IX_AvaliacaoDisciplinasEtapas_EtapaId",
                "AvaliacaoDisciplinasEtapas",
                "EtapaId");

            migrationBuilder.CreateIndex(
                "IX_AvaliacaoQuestao_QuestoesId",
                "AvaliacaoQuestao",
                "QuestoesId");

            migrationBuilder.CreateIndex(
                "IX_Descritores_TemaId",
                "Descritores",
                "TemaId");

            migrationBuilder.CreateIndex(
                "IX_Escolas_DistritoId",
                "Escolas",
                "DistritoId");

            migrationBuilder.CreateIndex(
                "IX_Escolas_MatrizId",
                "Escolas",
                "MatrizId");

            migrationBuilder.CreateIndex(
                "IX_Etapas_SegmentoId",
                "Etapas",
                "SegmentoId");

            migrationBuilder.CreateIndex(
                "IX_Questoes_DescritorId",
                "Questoes",
                "DescritorId");

            migrationBuilder.CreateIndex(
                "IX_Questoes_EtapaId",
                "Questoes",
                "EtapaId");

            migrationBuilder.CreateIndex(
                "IX_RespostaAlunos_AlternativaId",
                "RespostaAlunos",
                "AlternativaId");

            migrationBuilder.CreateIndex(
                "IX_RespostaAlunos_AlunoId",
                "RespostaAlunos",
                "AlunoId");

            migrationBuilder.CreateIndex(
                "IX_Salas_EscolaId",
                "Salas",
                "EscolaId");

            migrationBuilder.CreateIndex(
                "IX_Segmentos_CursoId",
                "Segmentos",
                "CursoId");

            migrationBuilder.CreateIndex(
                "IX_Temas_DisciplinaId",
                "Temas",
                "DisciplinaId");

            migrationBuilder.CreateIndex(
                "IX_Turmas_EtapaId",
                "Turmas",
                "EtapaId");

            migrationBuilder.CreateIndex(
                "IX_Turmas_FormaId",
                "Turmas",
                "FormaId");

            migrationBuilder.CreateIndex(
                "IX_Turmas_SalaId",
                "Turmas",
                "SalaId");

            migrationBuilder.CreateIndex(
                "IX_Turmas_TurnoId",
                "Turmas",
                "TurnoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AspNetRoleClaims");

            migrationBuilder.DropTable(
                "AspNetUserClaims");

            migrationBuilder.DropTable(
                "AspNetUserLogins");

            migrationBuilder.DropTable(
                "AspNetUserRoles");

            migrationBuilder.DropTable(
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "AvaliacaoDisciplinasEtapas");

            migrationBuilder.DropTable(
                "AvaliacaoQuestao");

            migrationBuilder.DropTable(
                "RespostaAlunos");

            migrationBuilder.DropTable(
                "AspNetRoles");

            migrationBuilder.DropTable(
                "AspNetUsers");

            migrationBuilder.DropTable(
                "Alternativas");

            migrationBuilder.DropTable(
                "Alunos");

            migrationBuilder.DropTable(
                "Avaliacoes");

            migrationBuilder.DropTable(
                "Questoes");

            migrationBuilder.DropTable(
                "Cpfs");

            migrationBuilder.DropTable(
                "Turmas");

            migrationBuilder.DropTable(
                "Descritores");

            migrationBuilder.DropTable(
                "Etapas");

            migrationBuilder.DropTable(
                "Formas");

            migrationBuilder.DropTable(
                "Salas");

            migrationBuilder.DropTable(
                "Turnos");

            migrationBuilder.DropTable(
                "Temas");

            migrationBuilder.DropTable(
                "Segmentos");

            migrationBuilder.DropTable(
                "Escolas");

            migrationBuilder.DropTable(
                "Disciplinas");

            migrationBuilder.DropTable(
                "Cursos");

            migrationBuilder.DropTable(
                "Distritos");
        }
    }
}