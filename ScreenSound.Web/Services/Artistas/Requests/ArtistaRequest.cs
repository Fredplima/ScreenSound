using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Services.Artistas.Requests;
public record ArtistaRequest([Required] string Nome, [Required] string Bio, string? FotoPerfil);

