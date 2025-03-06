using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Banco;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(Repository<Artista> artistaDAL)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
