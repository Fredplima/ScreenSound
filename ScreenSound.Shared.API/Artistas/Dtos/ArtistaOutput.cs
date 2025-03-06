using ScreenSound.Shared.API.Musicas.Dtos;

namespace ScreenSound.Shared.API.Artistas.Dtos;

public record ArtistaOutput(int Id, string Nome, string Bio, string? FotoPerfil)
{
    public double? Classificacao { get; set; }
    public ICollection<MusicaOutput>? Musicas { get; set; }
    public override string ToString()
    {
        return $"{Nome}";
    }
};