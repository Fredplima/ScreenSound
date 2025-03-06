namespace ScreenSound.Shared.API.Artistas.Dtos;

public record ArtistaEditInput(int Id, string Nome, string Bio, string? FotoPerfil)
    : ArtistaInput(Nome, Bio, FotoPerfil);