namespace ScreenSound.API.Requests
{
    public record ArtistaRequest(string Nome, string Bio);
    public record ArtistaRequestEdit(int Id, string Nome, string Bio)
        : ArtistaRequest(Nome, Bio);

}
