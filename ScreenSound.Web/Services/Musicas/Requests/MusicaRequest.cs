using ScreenSound.Web.Services.Generos.Requests;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Services.Musicas.Requests;

public record MusicaRequest([Required] string Nome, [Required] int ArtistaId, int AnoLancamento, ICollection<GeneroRequest>? Generos = null);

