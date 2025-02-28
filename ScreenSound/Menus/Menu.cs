using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Banco;

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
    public virtual void Executar(Repository<Artista> artistaDal, Repository<Musica> musicaDal)
    {
        Console.Clear();
    }
}
