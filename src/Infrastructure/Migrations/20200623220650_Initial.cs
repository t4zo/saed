using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    Nascimento = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(3722)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(4120))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 331, DateTimeKind.Local).AddTicks(863)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 335, DateTimeKind.Local).AddTicks(8073))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(4657)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(5154))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(4523)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(4904))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    Sigla = table.Column<string>(maxLength: 8, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(6948)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(7317))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 128, nullable: false),
                    Sigla = table.Column<string>(maxLength: 8, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(8610)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(8999))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Distrito",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 32, nullable: false),
                    Zona = table.Column<string>(maxLength: 8, nullable: false),
                    Distancia = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(9461)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(9827))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distrito", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 16, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(3702)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(4074))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 32, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(2827)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(3221))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 335, DateTimeKind.Local).AddTicks(8815)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 335, DateTimeKind.Local).AddTicks(9320))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 335, DateTimeKind.Local).AddTicks(9711)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(88))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(453)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(811))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(1166)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(1476))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(1912)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(2231))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segmento",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    Sigla = table.Column<string>(maxLength: 10, nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(7895)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(8273))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segmento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segmento_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoDisciplina",
                columns: table => new
                {
                    AvaliacaoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(5296)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(5657))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoDisciplina", x => new { x.DisciplinaId, x.AvaliacaoId });
                    table.ForeignKey(
                        name: "FK_AvaliacaoDisciplina_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvaliacaoDisciplina_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisciplinaId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(8742)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(9187))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tema_Disciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoDistrito",
                columns: table => new
                {
                    AvaliacaoId = table.Column<int>(nullable: false),
                    DistritoId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(6143)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(6513))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoDistrito", x => new { x.AvaliacaoId, x.DistritoId });
                    table.ForeignKey(
                        name: "FK_AvaliacaoDistrito_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AvaliacaoDistrito_Distrito_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Inep = table.Column<int>(nullable: true),
                    MatrizId = table.Column<int>(nullable: true),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    DistritoId = table.Column<int>(nullable: false),
                    Endereco = table.Column<string>(maxLength: 256, nullable: true),
                    Bairro = table.Column<string>(maxLength: 128, nullable: true),
                    Numero = table.Column<int>(nullable: true),
                    Complemento = table.Column<string>(maxLength: 128, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: false),
                    Telefone = table.Column<string>(maxLength: 11, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(709)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(1558))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escola_Distrito_DistritoId",
                        column: x => x.DistritoId,
                        principalTable: "Distrito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escola_Escola_MatrizId",
                        column: x => x.MatrizId,
                        principalTable: "Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Etapa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 64, nullable: false),
                    SegmentoId = table.Column<int>(nullable: false),
                    Normativa = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(2028)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(2453))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etapa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etapa_Segmento_SegmentoId",
                        column: x => x.SegmentoId,
                        principalTable: "Segmento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Descritor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemaId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(7808)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(8224))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descritor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descritor_Tema_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Tema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(nullable: false),
                    EscolaId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(7009)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(7449))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sala_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtapaDescritor",
                columns: table => new
                {
                    EtapaId = table.Column<int>(nullable: false),
                    DescritorId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(2844)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(3282))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtapaDescritor", x => new { x.EtapaId, x.DescritorId });
                    table.ForeignKey(
                        name: "FK_EtapaDescritor_Descritor_DescritorId",
                        column: x => x.DescritorId,
                        principalTable: "Descritor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EtapaDescritor_Etapa_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "Etapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescritorId = table.Column<int>(nullable: false),
                    Item = table.Column<string>(maxLength: 8, nullable: false),
                    Descricao = table.Column<string>(nullable: false),
                    Html = table.Column<string>(nullable: true),
                    Habilitada = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(4548)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(4940))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questao_Descritor_DescritorId",
                        column: x => x.DescritorId,
                        principalTable: "Descritor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaId = table.Column<int>(nullable: false),
                    EtapaId = table.Column<int>(nullable: false),
                    TurnoId = table.Column<int>(nullable: false),
                    FormaId = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 32, nullable: false),
                    QtdAlunos = table.Column<int>(nullable: false),
                    Extinta = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(609)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(1312))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Turma_Etapa_EtapaId",
                        column: x => x.EtapaId,
                        principalTable: "Etapa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turma_Forma_FormaId",
                        column: x => x.FormaId,
                        principalTable: "Forma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turma_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Turma_Turno_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Turno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alternativa",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false),
                    Correta = table.Column<bool>(nullable: false),
                    QuestaoId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(2735)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 336, DateTimeKind.Local).AddTicks(3304))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alternativa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alternativa_Questao_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestaoAvaliacao",
                columns: table => new
                {
                    AvaliacaoId = table.Column<int>(nullable: false),
                    QuestaoId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(5429)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(5822))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestaoAvaliacao", x => new { x.AvaliacaoId, x.QuestaoId });
                    table.ForeignKey(
                        name: "FK_QuestaoAvaliacao_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestaoAvaliacao_Questao_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "Questao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurmaAluno",
                columns: table => new
                {
                    TurmaId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(1876)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(2368))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaAluno", x => new { x.TurmaId, x.AlunoId });
                    table.ForeignKey(
                        name: "FK_TurmaAluno_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaAluno_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioTurmaAvaliacao",
                columns: table => new
                {
                    ApplicationUserId = table.Column<int>(nullable: false),
                    TurmaId = table.Column<int>(nullable: false),
                    AvaliacaoId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(3700)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 338, DateTimeKind.Local).AddTicks(4081))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioTurmaAvaliacao", x => new { x.ApplicationUserId, x.TurmaId, x.AvaliacaoId });
                    table.ForeignKey(
                        name: "FK_UsuarioTurmaAvaliacao_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioTurmaAvaliacao_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RespostaAluno",
                columns: table => new
                {
                    AvaliacaoId = table.Column<int>(nullable: false),
                    AlunoId = table.Column<int>(nullable: false),
                    AlternativaId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(6214)),
                    UpdatedDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 6, 23, 19, 6, 49, 337, DateTimeKind.Local).AddTicks(6615))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RespostaAluno", x => new { x.AvaliacaoId, x.AlunoId, x.AlternativaId });
                    table.ForeignKey(
                        name: "FK_RespostaAluno_Alternativa_AlternativaId",
                        column: x => x.AlternativaId,
                        principalTable: "Alternativa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostaAluno_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RespostaAluno_Avaliacoes_AvaliacaoId",
                        column: x => x.AvaliacaoId,
                        principalTable: "Avaliacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Aluno",
                columns: new[] { "Id", "Nascimento", "Nome" },
                values: new object[,]
                {
                    { 1, new DateTime(2002, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria Luz" },
                    { 2, new DateTime(1999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "João carlos" }
                });

            migrationBuilder.InsertData(
                table: "Avaliacoes",
                columns: new[] { "Id", "Codigo", "Status" },
                values: new object[,]
                {
                    { 1, "2020", 1 },
                    { 2, "2021", 0 }
                });

            migrationBuilder.InsertData(
                table: "Curso",
                columns: new[] { "Id", "Nome", "Sigla" },
                values: new object[,]
                {
                    { 1, "Educação Infantil", "EI" },
                    { 2, "Ensino Fundamental", "EF" },
                    { 3, "Educação de Jovens e Adultos", "EJA" }
                });

            migrationBuilder.InsertData(
                table: "Disciplina",
                columns: new[] { "Id", "Nome", "Sigla" },
                values: new object[,]
                {
                    { 1, "Português", "Port" },
                    { 2, "Matemática", "Mat" },
                    { 3, "Ciências", "Cie" }
                });

            migrationBuilder.InsertData(
                table: "Distrito",
                columns: new[] { "Id", "Distancia", "Nome", "Zona" },
                values: new object[,]
                {
                    { 10, 10, "Mandacaru", "Rural" },
                    { 9, 70, "Massaroca", "Rural" },
                    { 8, 35, "Junco", "Rural" },
                    { 7, 75, "Pinhões", "Rural" },
                    { 6, 40, "Maniçoba", "Rural" },
                    { 4, 50, "Juremal", "Rural" },
                    { 3, 75, "Itamotinga", "Rural" },
                    { 2, 110, "Abóbora", "Rural" },
                    { 1, 0, "Sede", "Urbana" },
                    { 5, 25, "Carnaíba", "Rural" }
                });

            migrationBuilder.InsertData(
                table: "Forma",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Seriada" },
                    { 2, "Multi" }
                });

            migrationBuilder.InsertData(
                table: "Turno",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 4, "Integral" },
                    { 1, "Manhã" },
                    { 2, "Tarde" },
                    { 3, "Noite" },
                    { 5, "Não Informado" }
                });

            migrationBuilder.InsertData(
                table: "AvaliacaoDisciplina",
                columns: new[] { "DisciplinaId", "AvaliacaoId" },
                values: new object[,]
                {
                    { 3, 2 },
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "AvaliacaoDistrito",
                columns: new[] { "AvaliacaoId", "DistritoId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Escola",
                columns: new[] { "Id", "Bairro", "Complemento", "DistritoId", "Email", "Endereco", "Inep", "MatrizId", "Nome", "Numero", "Telefone" },
                values: new object[,]
                {
                    { 108, null, null, 1, "ESCOLADINORAH@MSN.COM", null, 29025036, null, "PROFª DINORAH ALBERNAZ MELO DA SILVA", null, "" },
                    { 5, null, null, 4, "", null, 29026148, null, "AMÉRICO TANURI - MANIÇOBA", null, "74988067161" },
                    { 10, null, null, 4, "ESCOLA_BOMJESUSBARAUNA@HOTMAIL.COM", null, 29026156, null, "BOM JESUS - BARAÚNA", null, "7436187055" },
                    { 11, null, null, 4, "BOMJESUS_NH01@HOTMAIL.COM", null, 29025907, null, "BOM JESUS - NH1", null, "" },
                    { 15, null, null, 4, "EMPMANDACARU@GMAIL.COM", null, 29025346, null, "CELSO CAVALCANTE DE CARVALHO", null, "7436177211" },
                    { 17, null, null, 4, "", null, 29025974, null, "CORAÇÃO DE JESUS - JUREMA VERMELHA", null, "" },
                    { 18, null, null, 4, "", null, 29026164, null, "CORAÇÃO DE JESUS - SERRA DA MADEIRA", null, "7496357165" },
                    { 21, null, null, 4, "AMILTONGOMES2016@HOTMAIL.COM", null, 29026024, null, "DOUTOR EDSON RIBEIRO", null, "74988133633" },
                    { 35, null, null, 4, "SUENI-SANTOS@YAHOO.COM.BR", null, 29402170, null, "E.M.E.I. BOM JESUS DOS NAVEGANTES", null, "7436173029" },
                    { 45, null, null, 4, "VALDATDB12@GMAIL.COM", null, 29461227, null, "E.M.E.I. MANOEL ALVES DA MOTA", null, "" },
                    { 61, null, null, 4, "", null, 29026261, null, "E.M.E.I. PROFª JOANA RAMOS NETA", null, "7499346352" },
                    { 63, null, null, 4, "ANACCN2007@YAHOO.COM.BR", null, 29469112, null, "E.M.R.T.I. SÃO JOSÉ", null, "74999522470" },
                    { 67, null, null, 4, "ESCOLAMUNICIPALELISEUSANTOS@HOTMAIL.COM", null, 29026113, null, "ELISEU SANTOS", null, "7488078555" },
                    { 68, null, null, 4, "EURIDICERIBEIROVIANA.QUIPA@HOTMAIL.COM", null, 29026199, null, "EURÍDICE RIBEIRO VIANA", null, "" },
                    { 69, null, null, 4, "KLERISSON@YAHOO.COM.BR", null, 29026202, null, "FAMÍLIA UNIDA", null, "7488367939" },
                    { 73, null, null, 4, "AMILTONGOMES2016@HOTMAIL.COM", null, 29025583, null, "JATOBÁ", null, "" },
                    { 3, null, null, 4, "ESCOLA25.DEJULHO@OUTLOOK.COM", null, 29026130, null, "25 DE JULHO", null, "7488068024" },
                    { 2, null, null, 4, "QUINZEDEJULHO2014@OUTLOOK.COM", null, 29024994, null, "15 DE JULHO", null, "7436113278" },
                    { 117, null, null, 3, "ESCOLA_MATILDECOSTA@HOTMAIL.COM", null, 29025770, null, "PROFª MATILDE COSTA MEDRADO", null, "74988526535" },
                    { 81, null, null, 4, "", null, 29025923, null, "LINDAURA MARIA DE JESUS", null, "7436172000" },
                    { 114, null, null, 1, "ESCOLAMARIALOURDESDUARTE@HOTMAIL.COM", null, 29024943, null, "PROFª MARIA DE LOURDES DUARTE", null, "" },
                    { 115, null, null, 1, "", null, 29024986, null, "PROFª MARIA FRANCA PIRES", null, "7436137115" },
                    { 116, null, null, 1, "WILZAMIRANDA@HOTMAIL.COM", null, 29362504, null, "PROFª MARIA JOSÉ LIMA DA ROCHA", null, "" },
                    { 119, null, null, 1, "ESCOLATEREZINHA.ETFO@HOTMAIL.COM", null, 29025397, null, "PROFª TEREZINHA FERREIRA DE OLIVEIRA", null, "" },
                    { 120, null, null, 1, "ESCOLACARLOSCOSTA_2012@HOTMAIL.COM", null, 29415160, null, "PROFº CARLOS DA COSTA SILVA", null, "" },
                    { 121, null, null, 1, "MUNICIPALJOSEPEREIRA@BOL.COM.BR", null, 29405106, null, "PROFº JOSÉ PEREIRA DA SILVA", null, "" },
                    { 122, null, null, 1, "LUCYSOARES1@HOTMAIL.COM", null, 29024714, null, "PROFº LUIS CURSINO DA FRANÇA CARDOSO", null, "" },
                    { 1, null, null, 4, "DOISDEJULHOJUAZEIRO@HOTMAIL.COM", null, 29024935, null, "02 DE JULHO", null, "74988440798" },
                    { 124, null, null, 1, "RAIMUNDOMEDRADOPRIMO@HOTMAIL.COM", null, 29424445, null, "RAIMUNDO MEDRADO PRIMO", null, "" },
                    { 24, null, null, 2, "EMMSBONFIM@HOTMAIL.COM", null, 29025664, null, "E.M.E.I. ABÓBORA", null, "7436179072" },
                    { 84, null, null, 2, "EMMSBONFIM@HOTMAIL.COM", null, 29025672, null, "MANOEL DE SOUZA BONFIM", null, "7436179072" },
                    { 26, null, null, 3, "", null, 29025842, null, "E.M.E.I. AMÉLIA BORGES", null, "7488223271" },
                    { 54, null, null, 3, "ESCOLA_NSGROTAS@HOTMAIL.COM", null, 29025788, null, "E.M.E.I. NOSSA SENHORA DAS GROTAS - CARNAÍBA", null, "" },
                    { 71, null, null, 3, "ESCOLAGRACIOSAXAVIER@GMAIL.COM", null, 29025753, null, "GRACIOSA XAVIER RAMOS GOMES", null, "7436181029" },
                    { 96, null, null, 3, "ESCOLA_OSORIOTELES@HOTMAIL.COM", null, 29025869, null, "OSORIO TELES DE MENEZES", null, "7436181270" },
                    { 99, null, null, 3, "ESCOLAGRACIOSAXAVIER@GMAIL.COM", null, 29025834, null, "PEDRO DIAS", null, "" },
                    { 6, null, null, 2, "EMMSBONFIM@HOTMAIL.COM", null, 29025699, null, "AMÉRICO TANURY - ABÓBORA", null, "7436179072" },
                    { 86, null, null, 4, "JAQUELLINEASSESSORIA27@GMAIL.COM", null, 29026296, null, "MANOEL LUIZ DA SILVA", null, "7436139057" },
                    { 93, null, null, 4, "NOSSASENHORASAOFRANCISCO@GMAIL.COM", null, 29026105, null, "NOSSA SENHORA DAS GROTAS - BOQUEIRÃO", null, "7436178287" },
                    { 113, null, null, 1, "", null, 29024978, null, "PROFª LEOPOLDINA LEAL", null, "" },
                    { 89, null, null, 5, "", null, 29026652, null, "MARIA DO CARMO SÁ NOGUEIRA", null, "" },
                    { 90, null, null, 5, "", null, 29026660, null, "MARIA MONTEIRO BACELAR", null, "7436184001" },
                    { 92, null, null, 5, "", null, 29026679, null, "MIGUEL ÂNGELO DE SOUZA", null, "7499867445" },
                    { 97, null, null, 5, "JAELTON.OLIVEIRA@HOTMAIL.COM", null, 29461359, null, "PAULO FREIRE", null, "7499077828" },
                    { 105, null, null, 5, "", null, 29026695, null, "PROFª BERNADETE BRAGA", null, "" },
                    { 109, null, null, 5, "", null, 29026709, null, "PROFª EDUALDINA DAMÁSIO", null, "" },
                    { 118, null, null, 5, "", null, 29026725, null, "PROFª OSCARLINA TANURI", null, "" },
                    { 133, null, null, 5, "ESCOLAAMADEUSDAMASIO@GMAIL.COM", null, 29026520, null, "VEREADOR AMADEUS DAMÁSIO", null, "7435381883" },
                    { 19, null, null, 6, "ESCOLA_RAIMUNDOCUNHALEITE@HOTMAIL.COM", null, 29026890, null, "DEPUTADO RAIMUNDO DA CUNHA LEITE", null, "7436175001" },
                    { 23, null, null, 6, "ESCOLADURVALBARBOSA@GMAL.COM", null, 29026911, null, "DURVAL BARBOSA DA CUNHA", null, "7436175004" },
                    { 103, null, null, 6, "ESCOLAAFC@GMAIL.COM", null, 29026873, null, "PROFª ANTONILA DA FRANÇA CARDOSO", null, "" },
                    { 46, null, null, 7, "TULIOROZARORIZ@GMAIL.COM", null, 29469635, null, "E.M.E.I. MARIA FERREIRA DE SOUZA", null, "7436176145" },
                    { 104, null, null, 7, "", null, 29026989, null, "PROFª ATANILHA LUZ ARAÚJO", null, "" },
                    { 125, null, null, 7, "RURALMASSAROCA@HOTMAIL.COM", null, 29341779, null, "RURAL DE MASSAROCA - ERUM", null, "74994436003" },
                    { 62, null, null, 8, "ALINEDEFATIMA92@GMAIL.COM", null, 29027098, null, "E.M.E.I. SÃO FRANCISCO DE ASSIS", null, "74999563806" },
                    { 88, null, null, 5, "", null, 29026776, null, "MARIA AMÉLIA DE SOUZA OLIVEIRA", null, "7491044270" },
                    { 87, null, null, 5, "", null, 29341710, null, "MANOEL NUNES AMORIM", null, "7436115110" },
                    { 80, null, null, 5, "", null, 29026644, null, "LÚCIA CARMEM SOBREIRA", null, "" },
                    { 77, null, null, 5, "", null, 29026636, null, "JOSÉ DE AMORIM", null, "" },
                    { 95, null, null, 4, "ESCOLARAINHA@GMAIL.COM", null, 29025958, null, "NOSSA SENHORA RAINHA DOS ANJOS", null, "7491957370" },
                    { 100, null, null, 4, "", null, 29026431, null, "PONTAL", null, "" },
                    { 112, null, null, 4, "COLEGIOIRACYNUNES@GMAIL.COM", null, 29341809, null, "PROFª IRACY NUNES DA SILVA", null, "" },
                    { 126, null, null, 4, "NOSSASENHORASAOFRANCISCO@GMAIL.COM", null, 29026350, null, "SÃO FRANCISCO DE ASSIS - MULUNGÚ", null, "7436178287" },
                    { 127, null, null, 4, "NH2SAOFRANCISCO@GMAIL.COM", null, 29025982, null, "SÃO FRANCISCO DE ASSIS - NH2", null, "7436189018" },
                    { 128, null, null, 4, "", null, 29026377, null, "SÃO JOSÉ", null, "" },
                    { 129, null, null, 4, "", null, 29025931, null, "SÃO SEBASTIÃO", null, "" },
                    { 91, null, null, 4, "ESCOLAMARIANORODRIGUES@YAHOO.COM.BR", null, 29025230, null, "MARIANO RODRIGUES DE SOUZA", null, "7436183004" },
                    { 130, null, null, 4, "", null, 29026288, null, "SANTA INÊS", null, "" },
                    { 132, null, null, 4, "", null, 29025010, null, "SANTO ANTÔNIO", null, "7436187055" },
                    { 4, null, null, 5, "", null, 29026601, null, "AMÉRICO TANURI - JUNCO", null, "7498811198" },
                    { 8, null, null, 5, "VALDETEMSF@HOTMAIL.COM", null, 29026482, null, "ANTONIO FRANCISCO DE OLIVEIRA", null, "7488158634" },
                    { 40, null, null, 5, "SOLANGETIASOL@HOTMAIL.COM", null, 29026628, null, "E.M.E.I. HERBET MOUSE RODRIGUES", null, "" },
                    { 56, null, null, 5, "", null, 29026792, null, "E.M.E.I. PASSAGEM DO SARGENTO", null, "" },
                    { 74, null, null, 5, "", null, 29026741, null, "JOÃO DIAS FERREIRA", null, "" },
                    { 75, null, null, 5, "", null, 29025222, null, "JOÃO NEVES DE ALMEIDA", null, "74988111398" },
                    { 131, null, null, 4, "", null, 29026300, null, "SANTA TEREZINHA", null, "7488110653" },
                    { 111, null, null, 1, "ESCOLAHAYDEE@GMAIL.COM", null, 29024960, null, "PROFª HAYDÉE FONSECA FALCÃO", null, "74999858835" },
                    { 123, null, null, 8, "ALINEDEFATIMA92@GMAIL.COM", null, 29027055, null, "RAIMUNDO CLEMENTINO DE SOUZA", null, "74999759179" },
                    { 66, null, null, 8, "ALINEDEFATIMA92@GMAIL.COM", null, 29341744, null, "ELEOTÉRIO SOARES FONSÊCA", null, "" },
                    { 43, null, null, 1, "EMEILUANADASILVA@GMAIL.COM", null, 29467152, null, "E.M.E.I. LUANA DA SILVA NASCIMENTO", null, "74981300440" },
                    { 42, null, null, 1, "", null, 29461367, null, "E.M.E.I. LENI LOPES DE ARAÚJO SANTOS", null, "" },
                    { 41, null, null, 1, "", null, 29402210, null, "E.M.E.I. JANDIRA BORGES", null, "" },
                    { 39, null, null, 1, "", null, 29402100, null, "E.M.E.I. GENTIL DAMÁSIO DO NASCIMENTO", null, "7436133763" },
                    { 38, null, null, 1, "ROSALILAS_BISPO@HOTMAIL.COM", null, 29461189, null, "E.M.E.I. EDIVÂNIA SANTOS CARDOSO", null, "7488511345" },
                    { 37, null, null, 1, "JAMMYS.GUERRA@GMAIL.COM", null, 29402190, null, "E.M.E.I. DILMA CALMON DE OLIVEIRA", null, "" },
                    { 36, null, null, 1, "", null, 29932777, null, "E.M.E.I. CAIC MISAEL AGUILAR", null, "" },
                    { 34, null, null, 1, "SANTANABOL14@GMAIL.COM", null, 29026423, null, "E.M.E.I. BOLIVAR SANTANA", null, "7488054854" },
                    { 110, null, null, 1, "ESCOLAGUIOMARLUSTOSA@HOTMAIL.COM", null, 29024838, null, "PROFª GUIOMAR LUSTOSA RODRIGUES", null, "" },
                    { 32, null, null, 1, "JANE.SILVABARBOSA@HOTMAIL.COM", null, 29464064, null, "E.M.E.I. ARLINDA ALVES VARJÃO", null, "7488333869" },
                    { 31, null, null, 1, "EMAILPRIMAVEAARCENIOJOSE@GMAIL.COM", null, 29429790, null, "E.M.E.I. ARCENIO JOSÉ DA SILVA", null, "" },
                    { 44, null, null, 1, "", null, 29461219, null, "E.M.E.I. LUZINETE DE OLIVEIRA", null, "" },
                    { 30, null, null, 1, "", null, 29461340, null, "E.M.E.I. ANTÔNIO GUILHERMINO", null, "74988054502" },
                    { 28, null, null, 1, "ROSANGELA.ALMEIDA@HOTMAIL.COM", null, 29401220, null, "E.M.E.I. ANA MARIA MORGADO CHAVES", null, "7436123354" },
                    { 27, null, null, 1, "", null, 29402140, null, "E.M.E.I. AMÉLIA DUARTE", null, "" },
                    { 25, null, null, 1, "CLEIABARRETO02@OUTLOOK.COM", null, 29461375, null, "E.M.E.I. ADJALVA FERREIRA LIMA", null, "" },
                    { 22, null, null, 1, "ESCOLADRJOSEDEARAUJO@HOTMAIL.COM", null, 29025478, null, "DOUTOR JOSÉ DE ARAÚJO SOUZA", null, "7436130580" },
                    { 20, null, null, 1, "ESCOLADJR@GMAIL.COM", null, 29378893, null, "DOM JOSÉ RODRIGUES", null, "7488110737" },
                    { 16, null, null, 1, "ESCOLACSU.JUA@GMAIL.COM", null, 29024668, null, "CENTRO SOCIAL URBANO - CSU", null, "7436112744" },
                    { 14, null, null, 1, "ESCOLA_CAXANGA@HOTMAIL.COM", null, 29024650, null, "CAXANGÁ", null, "7436122900" },
                    { 13, null, null, 1, "ESCOLACAICJUA@GMAIL.COM", null, 29362415, null, "CAIC - MISAEL AGUILAR", null, "7436118041" },
                    { 12, null, null, 1, "MAZZARELLOROCHA@OUTLOOK.COM", null, 29469120, null, "C.R.A. PROFª MAZZARELLO CAVALCANTI REIS DA ROCHA", null, "" },
                    { 9, null, null, 1, "CEAJC1@HOTMAIL.COM", null, 29429536, null, "ARGEMIRO JOSE DA CRUZ", null, "7436110018" },
                    { 7, null, null, 1, "ANALIABARBOSA.EDU@GMAIL.COM", null, 29341256, null, "ANÁLIA BARBOSA DE SOUZA", null, "7491552384" },
                    { 29, null, null, 1, "CENTROANNAHILDA@GMAIL.COM", null, 29402120, null, "E.M.E.I. ANNA HILDA LEITE FARIAS", null, "7436124696" },
                    { 47, null, null, 1, "EMEIMARIAHELENA@HOTMAIL.COM.BR", null, 29461243, null, "E.M.E.I. MARIA HELENA DA SILVA PEREIRA", null, "7488342914" },
                    { 33, null, null, 1, "EMEI.DIRETORIA@GMAIL.COM", null, 29470820, null, "E.M.E.I. BEATRIZ ANGÉLICA MOTA", null, "" },
                    { 49, null, null, 1, "", null, 29461170, null, "E.M.E.I. MARIA JÚLIA RODRIGUES TANURI", null, "" },
                    { 107, null, null, 1, "E.CRENILDESBRANDAO@GMAIL.COM", null, 29024900, null, "PROFª CRENILDES LUIS BRANDÃO", null, "" },
                    { 48, null, null, 1, "CLAUDIANA.PROF@GMAIL.COM", null, 29461235, null, "E.M.E.I. MARIA HOZANA NUNES", null, "7436124696" },
                    { 102, null, null, 1, "", null, 29401600, null, "PRESIDENTE TANCREDO NEVES", null, "" },
                    { 101, null, null, 1, "", null, 29025362, null, "PREFEITO APRÍGIO DUARTE", null, "" },
                    { 98, null, null, 1, "PAULOVI_2511@HOTMAIL.COM", null, 29024587, null, "PAULO VI", null, "7436115675" },
                    { 94, null, null, 1, "ENSGSEDUC@GMAIL.COM", null, 29025311, null, "NOSSA SENHORA DAS GROTAS-SEDE", null, "" },
                    { 85, null, null, 1, "", null, 29026440, null, "MANOEL GOMES MARTINS", null, "" },
                    { 83, null, null, 1, "", null, 29025206, null, "MANDACARU", null, "" },
                    { 82, null, null, 1, "ESCOLALUDGERO@GMAIL.COM", null, 29025184, null, "LUDGERO DE SOUZA COSTA", null, "7436121731" },
                    { 79, null, null, 1, "ESCOLAJUDITELCOSTA@HOTMAIL.COM", null, 29025176, null, "JUDITE LEAL COSTA", null, "7436114939" },
                    { 78, null, null, 1, "ESCOLAJOSEPADILHA@HOTMAIL.COM", null, 29025370, null, "JOSÉ PADILHA DE SOUZA", null, "7436120372" },
                    { 76, null, null, 1, "JOCA.DIRETORIA@GMAIL.COM", null, 29025168, null, "JOCA DE SOUZA OLIVEIRA", null, "" },
                    { 106, null, null, 1, "", null, 29024692, null, "PROFª CARMEM COSTA SANTOS", null, "" },
                    { 70, null, null, 1, "ESCOLAPROMENOR@HOTMAIL.COM", null, 29025524, null, "FUNDAÇÃO JUAZEIRENSE PROMENOR", null, "7436113992" },
                    { 50, null, null, 1, "TRPAQUINO@HOTMAIL.COM", null, 29025338, null, "E.M.E.I. MARIA SUELY MEDRADO ARAÚJO", null, "7488419813" },
                    { 51, null, null, 1, "", null, 29402160, null, "E.M.E.I. MARIÁ VIANA TANURI", null, "" },
                    { 72, null, null, 1, "PATRICIACARLA01@HOTMAIL.COM", null, 29025575, null, "HELENA ARAÚJO PINHEIRO", null, "7436111626" },
                    { 53, null, null, 1, "EMEINAILDE@HOTMAIL.COM", null, 29402150, null, "E.M.E.I. NAILDE DE SOUZA COSTA", null, "" },
                    { 55, null, null, 1, "", null, 29445256, null, "E.M.E.I. NOSSO SENHOR DOS AFLITOS", null, "" },
                    { 52, null, null, 1, "", null, 29461197, null, "E.M.E.I. NÉLIA DE SOUZA COSTA", null, "7436123354" },
                    { 58, null, null, 1, "MARI_LUCA@MSN.COM", null, 29470811, null, "E.M.E.I. PREFEITO APRÍGIO DUARTE", null, "74988320498" },
                    { 59, null, null, 1, "EMAILPRIMAVERAARCENIOJOSE@GMAIL.COM", null, 29461251, null, "E.M.E.I. PRIMAVERA", null, "" },
                    { 60, null, null, 1, "EMEIHELOISAHELENA@HOTMAIL.COM", null, 29402110, null, "E.M.E.I. PROFª HELOISA HELENA BENEVIDES FARIAS", null, "" },
                    { 64, null, null, 1, "LINDY_CPC@HOTMAIL.COM", null, 29450004, null, "E.M.T.I. PROFª IRACEMA PEREIRA DA PAIXÃO", null, "74988090992" },
                    { 65, null, null, 1, "EDUCANDARIOJOAO23@HOTMAIL.COM", null, 29401610, null, "EDUCANDÁRIO JOÃO XXIII", null, "" },
                    { 57, null, null, 1, "JOSEFACRISTINAT@GMAIL.COM", null, 29469252, null, "E.M.E.I. PASTOR MANOEL MARQUES DE SOUZA", null, "74988136212" }
                });

            migrationBuilder.InsertData(
                table: "Segmento",
                columns: new[] { "Id", "CursoId", "Nome", "Sigla" },
                values: new object[,]
                {
                    { 2, 1, "Pré-Escola", "PE" },
                    { 3, 2, "Ensino Fundamental I", "EF I" },
                    { 4, 2, "Ensino Fundamental II", "EF II" },
                    { 7, 2, "Correção de Fluxo", "CF" },
                    { 5, 3, "Educação de Jovens e Adultos I", "EJA I" },
                    { 6, 3, "Educação de Jovens e Adultos II", "EJA II" },
                    { 1, 1, "Creche", "CR" }
                });

            migrationBuilder.InsertData(
                table: "Tema",
                columns: new[] { "Id", "DisciplinaId", "Nome" },
                values: new object[,]
                {
                    { 7, 2, "Tema 02 - Disciplina Mat" },
                    { 6, 2, "Tema 01 - Disciplina Mat" },
                    { 5, 1, "Tema 05 - Disciplina Port" },
                    { 4, 1, "Tema 04 - Disciplina Port" },
                    { 3, 1, "Tema 03 - Disciplina Port" },
                    { 2, 1, "Tema 02 - Disciplina Port" },
                    { 1, 1, "Tema 01 - Disciplina Port" },
                    { 9, 2, "Tema 04 - Disciplina Mat" },
                    { 11, 3, "Tema 01 - Disciplina Cie" },
                    { 12, 3, "Tema 02 - Disciplina Cie" },
                    { 13, 3, "Tema 03 - Disciplina Cie" },
                    { 14, 3, "Tema 04 - Disciplina Cie" },
                    { 15, 3, "Tema 05 - Disciplina Cie" },
                    { 8, 2, "Tema 03 - Disciplina Mat" },
                    { 10, 2, "Tema 05 - Disciplina Mat" }
                });

            migrationBuilder.InsertData(
                table: "Descritor",
                columns: new[] { "Id", "Nome", "TemaId" },
                values: new object[,]
                {
                    { 8, "Descritor 03 - Tema 2", 2 },
                    { 15, "Descritor 05 - Tema 3", 3 },
                    { 14, "Descritor 04 - Tema 3", 3 },
                    { 13, "Descritor 03 - Tema 3", 3 },
                    { 12, "Descritor 02 - Tema 3", 3 },
                    { 11, "Descritor 01 - Tema 3", 3 },
                    { 10, "Descritor 05 - Tema 2", 2 },
                    { 9, "Descritor 04 - Tema 2", 2 },
                    { 1, "Descritor 01 - Tema 1", 1 },
                    { 7, "Descritor 02 - Tema 2", 2 },
                    { 6, "Descritor 01 - Tema 2", 2 },
                    { 5, "Descritor 05 - Tema 1", 1 },
                    { 4, "Descritor 04 - Tema 1", 1 },
                    { 3, "Descritor 03 - Tema 1", 1 },
                    { 2, "Descritor 02 - Tema 1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Etapa",
                columns: new[] { "Id", "Nome", "Normativa", "SegmentoId" },
                values: new object[,]
                {
                    { 1, "Berçário", 1, 1 },
                    { 20, "Etapa V", 1, 6 },
                    { 19, "Etapa IV", 1, 6 },
                    { 18, "Etapa III", 1, 5 },
                    { 9, "3º Ano", 1, 3 },
                    { 4, "Infantil III", 1, 1 },
                    { 5, "Infantil IV", 1, 2 },
                    { 6, "Infantil V", 1, 2 },
                    { 7, "1º Ano", 1, 3 },
                    { 8, "2º Ano", 1, 3 },
                    { 17, "Etapa II", 1, 5 },
                    { 10, "4º Ano", 1, 3 },
                    { 11, "5º Ano", 1, 3 },
                    { 12, "6º Ano", 1, 4 },
                    { 13, "7º Ano", 1, 4 },
                    { 14, "8º Ano", 1, 4 },
                    { 15, "9º Ano", 1, 4 },
                    { 21, "Se Liga", 1, 7 },
                    { 22, "Acelera", 1, 7 },
                    { 16, "Etapa I", 1, 5 },
                    { 3, "Infantil II", 1, 1 },
                    { 2, "Infantil I", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "Sala",
                columns: new[] { "Id", "EscolaId", "Numero" },
                values: new object[,]
                {
                    { 2, 1, 2 },
                    { 1, 1, 1 },
                    { 3, 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "EtapaDescritor",
                columns: new[] { "EtapaId", "DescritorId" },
                values: new object[] { 11, 1 });

            migrationBuilder.InsertData(
                table: "Questao",
                columns: new[] { "Id", "Descricao", "DescritorId", "Habilitada", "Html", "Item" },
                values: new object[,]
                {
                    { 1, "Questao 01 - Descritor 1", 1, false, null, "P01" },
                    { 2, "Questao 02 - Descritor 1", 1, false, null, "P02" },
                    { 3, "Questao 03 - Descritor 1", 1, false, null, "P03" },
                    { 4, "Questao 04 - Descritor 1", 1, false, null, "P04" },
                    { 5, "Questao 01 - Descritor 2", 2, false, null, "P01" },
                    { 6, "Questao 02 - Descritor 2", 2, false, null, "P02" },
                    { 10, "Teste", 2, false, null, "P01" },
                    { 7, "Questao 01 - Descritor 3", 3, false, null, "P01" },
                    { 11, "Teste2", 3, false, null, "P02" },
                    { 8, "Questao 01 - Descritor 4", 4, false, null, "P01" },
                    { 9, "Questao 02 - Descritor 4", 4, false, null, "P02" }
                });

            migrationBuilder.InsertData(
                table: "Turma",
                columns: new[] { "Id", "EtapaId", "Extinta", "FormaId", "Nome", "QtdAlunos", "SalaId", "TurnoId" },
                values: new object[] { 1, 11, false, 1, "A", 15, 1, 1 });

            migrationBuilder.InsertData(
                table: "Alternativa",
                columns: new[] { "Id", "Correta", "Descricao", "QuestaoId" },
                values: new object[,]
                {
                    { 1, true, "20", 1 },
                    { 20, false, "40", 5 },
                    { 19, false, "30", 5 },
                    { 18, true, "10", 5 },
                    { 17, false, "20", 5 },
                    { 16, true, "Simone", 4 },
                    { 15, false, "Marcelo", 4 },
                    { 14, false, "Adriana", 4 },
                    { 13, false, "Ronaldo", 4 },
                    { 12, false, "4,2", 3 },
                    { 11, true, "5,4", 3 },
                    { 10, false, "2,9", 3 },
                    { 9, false, "1,5", 3 },
                    { 8, false, "Maria", 2 },
                    { 7, false, "José", 2 },
                    { 6, true, "Carlos", 2 },
                    { 5, false, "Pedro", 2 },
                    { 4, false, "17", 1 },
                    { 3, false, "12", 1 },
                    { 2, false, "15", 1 }
                });

            migrationBuilder.InsertData(
                table: "QuestaoAvaliacao",
                columns: new[] { "AvaliacaoId", "QuestaoId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "TurmaAluno",
                columns: new[] { "TurmaId", "AlunoId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "RespostaAluno",
                columns: new[] { "AvaliacaoId", "AlunoId", "AlternativaId" },
                values: new object[,]
                {
                    { 1, 1, 5 },
                    { 1, 2, 7 },
                    { 1, 2, 10 },
                    { 1, 1, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alternativa_QuestaoId",
                table: "Alternativa",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoDisciplina_AvaliacaoId",
                table: "AvaliacaoDisciplina",
                column: "AvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoDistrito_DistritoId",
                table: "AvaliacaoDistrito",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Descritor_TemaId",
                table: "Descritor",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Escola_DistritoId",
                table: "Escola",
                column: "DistritoId");

            migrationBuilder.CreateIndex(
                name: "IX_Escola_MatrizId",
                table: "Escola",
                column: "MatrizId");

            migrationBuilder.CreateIndex(
                name: "IX_Etapa_SegmentoId",
                table: "Etapa",
                column: "SegmentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EtapaDescritor_DescritorId",
                table: "EtapaDescritor",
                column: "DescritorId");

            migrationBuilder.CreateIndex(
                name: "IX_Questao_DescritorId",
                table: "Questao",
                column: "DescritorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestaoAvaliacao_QuestaoId",
                table: "QuestaoAvaliacao",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAluno_AlternativaId",
                table: "RespostaAluno",
                column: "AlternativaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RespostaAluno_AlunoId",
                table: "RespostaAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_EscolaId",
                table: "Sala",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Segmento_CursoId",
                table: "Segmento",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tema_DisciplinaId",
                table: "Tema",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_EtapaId",
                table: "Turma",
                column: "EtapaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_FormaId",
                table: "Turma",
                column: "FormaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_SalaId",
                table: "Turma",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_TurnoId",
                table: "Turma",
                column: "TurnoId");

            migrationBuilder.CreateIndex(
                name: "IX_TurmaAluno_AlunoId",
                table: "TurmaAluno",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTurmaAvaliacao_AvaliacaoId",
                table: "UsuarioTurmaAvaliacao",
                column: "AvaliacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioTurmaAvaliacao_TurmaId",
                table: "UsuarioTurmaAvaliacao",
                column: "TurmaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AvaliacaoDisciplina");

            migrationBuilder.DropTable(
                name: "AvaliacaoDistrito");

            migrationBuilder.DropTable(
                name: "EtapaDescritor");

            migrationBuilder.DropTable(
                name: "QuestaoAvaliacao");

            migrationBuilder.DropTable(
                name: "RespostaAluno");

            migrationBuilder.DropTable(
                name: "TurmaAluno");

            migrationBuilder.DropTable(
                name: "UsuarioTurmaAvaliacao");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Alternativa");

            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Questao");

            migrationBuilder.DropTable(
                name: "Etapa");

            migrationBuilder.DropTable(
                name: "Forma");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Descritor");

            migrationBuilder.DropTable(
                name: "Segmento");

            migrationBuilder.DropTable(
                name: "Escola");

            migrationBuilder.DropTable(
                name: "Tema");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Distrito");

            migrationBuilder.DropTable(
                name: "Disciplina");
        }
    }
}
