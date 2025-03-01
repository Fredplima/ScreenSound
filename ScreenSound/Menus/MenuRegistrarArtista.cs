using ScreenSound.Core.Artistas;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Repositories;

namespace ScreenSound.Menus;

internal class MenuRegistrarArtista : Menu
{
    public override void Executar(Repository<Artista> artistaDal, Repository<Musica> musicaDal)
    {
        base.Executar(artistaDal, musicaDal);
        ExibirTituloDaOpcao("Registro dos Artistas");
        Console.Write("Digite o nome do artista que deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        Console.Write("Digite a bio do artista que deseja registrar: ");
        string bioDoArtista = Console.ReadLine()!;
        Artista artista = new(nomeDoArtista, bioDoArtista);
        artistaDal.Adicionar(artista);
        Console.WriteLine($"O artista {nomeDoArtista} foi registrado com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
