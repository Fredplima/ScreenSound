namespace ScreenSound.Web.Services.Authorization.Responses;

public class InfoPessoaResponse
{
    public int Id { get; set; }
    public string? Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
}