using Microsoft.AspNetCore.Mvc;
using ScreenSound.Core.Musicas;
using ScreenSound.Shared.API.Generos.Dtos;
using ScreenSound.Shared.API.Musicas.Dtos;
using ScreenSound.Shared.Dados.Banco;

namespace ScreenSound.API.Endpoints;

public static class MusicasExtensions
{ 
    public static void AddEndPointsMusicas(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("musicas")
              .RequireAuthorization()
              .WithTags("Musicas");

        #region Endpoint Músicas
        groupBuilder.MapGet("", ([FromServices] Repository<Musica> dal) =>
        {
            var musicaList = dal.Listar();
            if (musicaList is null)
            {
                return Results.NotFound();
            }
            var musicaListResponse = EntityListToResponseList(musicaList);
            return Results.Ok(musicaListResponse);
        });

        groupBuilder.MapGet("{nome}", ([FromServices] Repository<Musica> dal, string nome) =>
        {
            var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(musica));

        });

        groupBuilder.MapPost("", ([FromServices] Repository<Musica> dal, [FromServices] Repository<Genero> dalGenero,[FromBody] MusicaInputt musicaRequest) =>
        {
            var musica = new Musica(musicaRequest.Nome) 
            {                 
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.AnoLancamento,
                Generos = musicaRequest.Generos is not null?GeneroRequestConverter(musicaRequest.Generos, dalGenero) :
                new List<Genero>()
                
            };
            dal.Adicionar(musica);
            return Results.Ok();
        });

        groupBuilder.MapDelete("{id}", ([FromServices] Repository<Musica> dal, int id) => {
            var musica = dal.RecuperarPor(a => a.Id == id);
            if (musica is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(musica);
            return Results.NoContent();

        });

        groupBuilder.MapPut("", ([FromServices] Repository<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) => {
            var musicaAAtualizar = dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id);
            if (musicaAAtualizar is null)
            {
                return Results.NotFound();
            }
            musicaAAtualizar.Nome = musicaRequestEdit.Nome;
            musicaAAtualizar.AnoLancamento = musicaRequestEdit.AnoLancamento;

            dal.Atualizar(musicaAAtualizar);
            return Results.Ok();
        });
        #endregion
    }

    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroInput> generos, Repository<Genero> dalGenero)
    {
       var listaDeGeneros = new List<Genero>();
        foreach (var item in generos)
        {
            var entity = RequestToEntity(item);
            var genero = dalGenero.RecuperarPor(g=>g.Nome.ToUpper().Equals(item.Nome.ToUpper()));
            if (genero is not null)
            {
                listaDeGeneros.Add(genero);
            }
            else
            {
                listaDeGeneros.Add(entity);
            }
        }

        return listaDeGeneros;
    }

    private static Genero RequestToEntity(GeneroInput genero)
    {
        return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
    }

    private static ICollection<MusicaOutput> EntityListToResponseList(IEnumerable<Musica> musicaList)
    {
        return musicaList.Select(a => EntityToResponse(a)).ToList();
    }

    private static MusicaOutput EntityToResponse(Musica musica)
    {
        return new MusicaOutput(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome, musica.AnoLancamento);
    }
}
