namespace ScreenSound.Modelos; 

public class Artista(string nome, string bio)
{
    public virtual ICollection<Musica> Musicas { get; set; } = [];
    public virtual ICollection<AvaliacaoArtista> Avaliacoes { get; set; } = [];

    public string Nome { get; set; } = nome;
    public string? FotoPerfil { get; set; }
    public string Bio { get; set; } = bio;
    public int Id { get; set; }

    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
    }

    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Nome}");
        foreach (var musica in Musicas)
        {
            Console.WriteLine($"Música: {musica.Nome} - Ano de Lançamento: {musica.AnoLancamento}");
        }
    }

    public void AdicionarNota(int pessoaId, int nota)
    {
        nota = Math.Clamp(nota, 1, 5);
        Avaliacoes.Add(new AvaliacaoArtista() { ArtistaId = this.Id, PessoaId = pessoaId, Nota = nota });
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Nome}
            Foto de Perfil: {FotoPerfil}
            Bio: {Bio}";
    }
}