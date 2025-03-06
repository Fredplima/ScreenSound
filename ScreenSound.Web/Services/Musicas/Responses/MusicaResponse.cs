namespace ScreenSound.Web.Services.Musicas.Responses;

public record MusicaResponse(int Id, string Nome, int ArtistaId, string NomeArtista, int? AnoLancamento);