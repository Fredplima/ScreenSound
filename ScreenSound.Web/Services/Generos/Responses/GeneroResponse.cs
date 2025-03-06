namespace ScreenSound.Web.Services.Generos.Responses;
public record GeneroResponse(int Id, string Nome, string Descricao)
{
    public override string ToString()
    {
        return $"{Nome}";
    }
};