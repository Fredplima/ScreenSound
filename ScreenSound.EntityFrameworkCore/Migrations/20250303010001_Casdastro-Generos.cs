using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class CasdastroGeneros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO Generos (Nome, Descricao) VALUES ('Rock', 'Rock');
            INSERT INTO Generos (Nome, Descricao) VALUES ('MPB', 'Música popular brasileira');
            INSERT INTO Generos (Nome, Descricao) VALUES ('Sertanejo', 'Sertanejo');
            INSERT INTO Generos (Nome, Descricao) VALUES ('Funk', 'Funk');
            INSERT INTO Generos (Nome, Descricao) VALUES ('Clássica', 'Clássica');
            INSERT INTO Generos (Nome, Descricao) VALUES ('POP', 'POP');
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Generos;");
        }
    }
}
