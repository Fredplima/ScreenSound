using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Banco;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Repository<Artista> artistaDal, Repository<Musica> musicaDal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
