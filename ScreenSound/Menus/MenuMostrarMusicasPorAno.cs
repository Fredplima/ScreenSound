using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus
{
    internal class MenuMostrarMusicasPorAno : Menu
    {
        public override void Executar(Dal<Artista> artistaDal, Dal<Musica> musicaDal)
        {
            base.Executar(artistaDal, musicaDal);
            ExibirTituloDaOpcao("Exibir detalhes do artista");
            Console.Write("Digite o ano de lançamento das músicas: ");
            string anoLancamento = Console.ReadLine()!;
            var isInt = int.TryParse(anoLancamento, out int ano);
            if (!isInt)
            {
                Console.WriteLine("Ano de lançamento inválido!");
                AwaitToBack();
                return;
            }

            var musicas = musicaDal.RecuperarTodosPor(x => x.AnoLancamento == int.Parse(anoLancamento));
            if (musicas is not null)
            {
                Console.WriteLine("\nDiscografia:");
                if (musicas.Count == 0)
                {
                    Console.WriteLine($"Nenhuma música lançada no ano {anoLancamento} foi registrada no sistema.");
                    AwaitToBack();
                    return;
                }

                Console.WriteLine($"Discografias lançadas no ano {anoLancamento}");
                foreach (var musica in musicas)
                {
                    Console.WriteLine($"Música: {musica.Nome}  ({musica.AnoLancamento})");
                }
                AwaitToBack();
            }
            else
            {
                Console.WriteLine($"\nNenhuma música registrada para o ano informado!");
                AwaitToBack();
            }
        }

        private static void AwaitToBack()
        {
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
