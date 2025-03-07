using Microsoft.AspNetCore.Identity;

namespace ScreenSound.Core.Authorizations
{
    public class PessoaComAcesso : IdentityUser<int>
    {
        public string? Instagram { get; set; }
        public string? Spotify { get; set; }
    }
}
