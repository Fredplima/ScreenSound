namespace ScreenSound.Modelos;

public class Musica(string nome)
{
    public int Id { get; set; }
    public string Nome { get; set; } = nome;
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