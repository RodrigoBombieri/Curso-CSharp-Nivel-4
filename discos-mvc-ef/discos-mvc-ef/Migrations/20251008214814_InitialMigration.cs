using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace discos_mvc_ef.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estilos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposEdicion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposEdicion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Discos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaLanzamiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadCanciones = table.Column<int>(type: "int", nullable: false),
                    UrlTapa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstiloId = table.Column<int>(type: "int", nullable: false),
                    TipoEdicionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discos_Estilos_EstiloId",
                        column: x => x.EstiloId,
                        principalTable: "Estilos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Discos_TiposEdicion_TipoEdicionId",
                        column: x => x.TipoEdicionId,
                        principalTable: "TiposEdicion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discos_EstiloId",
                table: "Discos",
                column: "EstiloId");

            migrationBuilder.CreateIndex(
                name: "IX_Discos_TipoEdicionId",
                table: "Discos",
                column: "TipoEdicionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Discos");

            migrationBuilder.DropTable(
                name: "Estilos");

            migrationBuilder.DropTable(
                name: "TiposEdicion");
        }
    }
}
