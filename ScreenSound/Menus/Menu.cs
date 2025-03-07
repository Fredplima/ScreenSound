using ScreenSound.Core.Artistas;
using ScreenSound.Core.Musicas;
using ScreenSound.Shared.Dados.Banco;

namespace ScreenSound.Menus;

internal class Menu
{
    public static void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }
    public virtual void Executar(Repository<Artista> artistaRepository, Repository<Musica> musicaRepository)
    {
        Console.Clear();
    }

    public virtual void Executar(Repository<Artista> artistaRepository)
    {
        Console.Clear();
    }

    public virtual void Executar()
    {
        Console.Clear();
    }
}
