using ScreenSound.Core.Artistas;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Repositories;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
    public override void Executar(Repository<Artista> artistaDal, Repository<Musica> musicaDal)
    {
        base.Executar(artistaDal, musicaDal);
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        foreach (var artista in artistaDal.Listar())
        {
            Console.WriteLine($"Artista: {artista}");
        }

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
