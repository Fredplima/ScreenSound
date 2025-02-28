using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelaMusicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento", "ArtistaId"], ["Oceano", 1999, 1]);
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento", "ArtistaId"], ["Flor de Lis", 1976, 1]);
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento", "ArtistaId"], ["Samurai", 1982, 1]);
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento", "ArtistaId"], ["Se", 1992, 1]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Musicas");
        }
    }
}
