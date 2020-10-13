using Microsoft.EntityFrameworkCore.Migrations;

namespace SAED.Infrastructure.Migrations
{
    public partial class EscolaRua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Escolas");

            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Escolas",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Escolas");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Escolas",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }
    }
}
