using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoHBSIS.Migrations
{
    public partial class BDHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Contribuinte",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Contribuinte",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SalarioMinimo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<double>(nullable: false),
                    Ano = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalarioMinimo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImpostodeRenda",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Valor = table.Column<double>(nullable: false),
                    ContribuinteId = table.Column<int>(nullable: false),
                    SalarioMinimoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpostodeRenda", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImpostodeRenda_Contribuinte_ContribuinteId",
                        column: x => x.ContribuinteId,
                        principalTable: "Contribuinte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImpostodeRenda_SalarioMinimo_SalarioMinimoId",
                        column: x => x.SalarioMinimoId,
                        principalTable: "SalarioMinimo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImpostodeRenda_ContribuinteId",
                table: "ImpostodeRenda",
                column: "ContribuinteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ImpostodeRenda_SalarioMinimoId",
                table: "ImpostodeRenda",
                column: "SalarioMinimoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpostodeRenda");

            migrationBuilder.DropTable(
                name: "SalarioMinimo");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Contribuinte",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Contribuinte",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
