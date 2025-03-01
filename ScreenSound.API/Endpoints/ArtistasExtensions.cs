using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Core.Artistas;
using ScreenSound.Core.Repositories;
using ScreenSound.EntityFrameworkCore.Repositories;
using ScreenSound.Web.Host.services;

namespace ScreenSound.Web.Host.Endpoints
{
    public static class ArtistasExtensions
    {
        public static void AddArtistasEndpoints(this WebApplication app)
        {
            // Artistas Services
            app.MapGet("/Artistas", ([FromServices] ArtistaAppService artistaAppService) =>
            {
                return artistaAppService.Listar();
            });
            app.MapGet("/Artistas/{nome}", ([FromServices] ArtistaAppService artistaAppService, string nome) =>
            {
                return artistaAppService.RecuperarArtistaComMusicas(nome);
            });
            app.MapPost("/Artistas", ([FromServices] ArtistaAppService artistaAppService, [FromBody] ArtistaRequest artistaRequest) =>
            {
                var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);
                return artistaAppService.Adicionar(artista);
            });
            app.MapDelete("/Artistas/{id}", ([FromServices] ArtistaAppService artistaAppService, int id) =>
            {
                return artistaAppService.Deletar(id);
            });
            app.MapPut("/Artistas", ([FromServices] ArtistaAppService artistaAppService,
                                     [FromServices] Repository<Artista> artistaRepository,
                                     [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
            {
                var artistaAAtualizar = artistaRepository.Get(artistaRequestEdit.Id);
                if (artistaAAtualizar is null)
                {
                    return Results.NotFound();
                }

                artistaAAtualizar.Nome = artistaRequestEdit.Nome;
                artistaAAtualizar.Bio = artistaRequestEdit.Bio;
                artistaRepository.Atualizar(artistaAAtualizar);
                return Results.Ok();
            });
        }
    }
}
