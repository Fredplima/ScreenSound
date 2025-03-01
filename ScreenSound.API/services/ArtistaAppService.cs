using ScreenSound.Core.Artistas;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Repositories;

namespace ScreenSound.Web.Host.services
{
    public class ArtistaAppService : AppServiceBase<Artista>
    {
        private readonly Repository<Artista> _artistaRepository;
        private readonly Repository<Musica> _musicaRepository;

        public ArtistaAppService(Repository<Artista> artistaRepository, Repository<Musica> musicaRepository) :
            base(artistaRepository)
        {
            _artistaRepository = artistaRepository;
            _musicaRepository = musicaRepository;
        }

        public IResult RecuperarArtistaComMusicas(string nome)
        {
            var artista = _artistaRepository.RecuperarPor(x => x.Nome.ToUpper().Equals(nome.Trim().ToUpper()));
            if (artista == null)
            {
                return Results.NotFound();
            }

            var musicas = _musicaRepository.RecuperarTodosPor(x => x.ArtistaId.Equals(artista.Id));
            if (musicas is not null)
                artista.Musicas = musicas;

            return Results.Ok(artista);
        }
        
    }
}
