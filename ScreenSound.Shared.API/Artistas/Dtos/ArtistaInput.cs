using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Shared.API.Artistas.Dtos;
public record ArtistaInput([Required] string Nome, [Required] string Bio, string? FotoPerfil);

