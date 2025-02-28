using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Dal<Artista> artistaDal, Dal<Musica> musicaDal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
