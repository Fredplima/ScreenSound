namespace ScreenSound.Shared.API.Musicas.Dtos;

public record MusicaRequestEdit(int Id, string Nome, int ArtistaId, int AnoLancamento)
    : MusicaInputt(Nome, ArtistaId, AnoLancamento);