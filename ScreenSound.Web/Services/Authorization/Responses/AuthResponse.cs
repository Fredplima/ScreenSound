namespace ScreenSound.Web.Services.Authorization.Responses
{
    public class AuthResponse
    {
        public bool Sucesso { get; set; }
        public string[] Erros { get; set; } = [];
    }
}
