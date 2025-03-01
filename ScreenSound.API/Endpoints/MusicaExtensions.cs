using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Repositories;
using ScreenSound.Web.Host.services;

namespace ScreenSound.Web.Host.Endpoints
{
    public static class MusicaExtensions
    {
        public static void AddMusicasEndpoints(this WebApplication app)
        {
            // Musicas Services
            app.MapGet("/Musicas", ([FromServices] AppServiceBase<Musica> musicaAppService) =>
            {
                return musicaAppService.Listar();
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] AppServiceBase<Musica> musicaAppService, string nome) =>
            {
                return musicaAppService.RecuperarPor(m => m.Nome.ToLower().Equals(nome.ToLower()));
            });

            app.MapPost("/Musicas", ([FromServices] AppServiceBase<Musica> musicaAppService, [FromBody] MusicaRequest musicaRequest) =>
            {
                var musica = new Musica(musicaRequest.Nome, musicaRequest.AnoLancamento, musicaRequest.ArtistaId);
                return musicaAppService.Adicionar(musica);
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] AppServiceBase<Musica> musicaAppService, int id) =>
            {
                return musicaAppService.Deletar(id);
            });

            app.MapPut("/Musicas", ([FromServices] AppServiceBase<Musica> musicaAppService,
                                    [FromServices] Repository<Musica> musicaRepository,
                                    [FromBody] MusicaRequestEdit musicaRequest) =>
            {

                var musicaAAtualizar = musicaRepository.Get(musicaRequest.Id);
                if (musicaAAtualizar == null)
                {
                    return Results.NotFound();
                }

                musicaAAtualizar.Nome = musicaRequest.Nome;
                musicaAAtualizar.AnoLancamento = musicaRequest.AnoLancamento;
                musicaAAtualizar.ArtistaId = musicaRequest.ArtistaId;
                musicaRepository.Atualizar(musicaAAtualizar);
                return Results.Ok();
            });
        }
    }
}
