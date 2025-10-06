using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokemon_mvc_ef.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Elementos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elementos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoId = table.Column<int>(type: "int", nullable: true),
                    DebilidadId = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pokemons_Elementos_DebilidadId",
                        column: x => x.DebilidadId,
                        principalTable: "Elementos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pokemons_Elementos_TipoId",
                        column: x => x.TipoId,
                        principalTable: "Elementos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_DebilidadId",
                table: "Pokemons",
                column: "DebilidadId");

            migrationBuilder.CreateIndex(
                name: "IX_Pokemons_TipoId",
                table: "Pokemons",
                column: "TipoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pokemons");

            migrationBuilder.DropTable(
                name: "Elementos");
        }
    }
}
