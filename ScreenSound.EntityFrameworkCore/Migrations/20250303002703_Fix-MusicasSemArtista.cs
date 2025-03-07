using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class FixMusicasSemArtista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Só tem músicas do Djavan em "PopularMusicas"
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Voltar para o estado anterior
            migrationBuilder.Sql("UPDATE FROM Musicas SET ArtistaId = NULL");
        }
    }
}
