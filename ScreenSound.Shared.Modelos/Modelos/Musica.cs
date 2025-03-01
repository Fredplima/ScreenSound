using ScreenSound.Core.Artistas;

namespace ScreenSound.Core.Modelos;

public class Musica: IEntity
{
    public Musica(string nome)
    {
        Nome = nome;
    }

    public Musica(string nome, int? anoLancamento, int artistaId)
    {
        Nome = nome;
        AnoLancamento = anoLancamento;
        ArtistaId = artistaId;
    }

    public int Id { get; set; }
    public string Nome { get; set; }
    public int? AnoLancamento { get; set; }
    public int ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");

    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Nome}";
    }
}