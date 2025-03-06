using Microsoft.AspNetCore.Identity;

namespace ScreenSound.Shared.Dados.Modelos
{
    public class PessoaComAcesso : IdentityUser<int>
    {
        public string? Instagram { get; set; }
        public string? Spotify { get; set; }
    }
}
