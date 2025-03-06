using ScreenSound.Shared.API.Generos.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Shared.API.Musicas.Dtos;

public record MusicaInputt([Required] string Nome, [Required] int ArtistaId, int AnoLancamento, ICollection<GeneroInput>? Generos = null);

