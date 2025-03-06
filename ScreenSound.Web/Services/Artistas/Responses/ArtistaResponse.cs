using ScreenSound.Web.Services.Musicas.Responses;

namespace ScreenSound.Web.Services.Artistas.Responses;

public record ArtistaResponse(int Id, string Nome, string Bio, string? FotoPerfil)
{
    public double? Classificacao { get; set; }
    public ICollection<MusicaResponse>? Musicas { get; set; }
    public override string ToString()
    {
        return $"{Nome}";
    }
};