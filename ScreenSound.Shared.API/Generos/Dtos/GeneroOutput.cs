namespace ScreenSound.Shared.API.Generos.Dtos;
public record GeneroOutput(int Id, string Nome, string Descricao)
{
    public override string ToString()
    {
        return $"{Nome}";
    }
};