namespace ScreenSound.Shared.API.Musicas.Dtos;

public record MusicaOutput(int Id, string Nome, int ArtistaId, string NomeArtista, int? AnoLancamento);