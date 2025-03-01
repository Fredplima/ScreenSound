using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest([Required] string Nome, [Required] int ArtistaId, int AnoLancamento);

    public record MusicaRequestEdit(int Id, string Nome, int ArtistaId, int AnoLancamento)
    : MusicaRequest(Nome, ArtistaId, AnoLancamento);
}
