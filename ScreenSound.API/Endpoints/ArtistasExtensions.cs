using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Core.Artistas;
using ScreenSound.Core.Authorizations;
using ScreenSound.Shared.API.Artistas.Dtos;
using ScreenSound.Shared.API.Musicas.Dtos;
using ScreenSound.Shared.Dados.Banco;
using System.Security.Claims;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndPointsArtistas(this WebApplication app)
    {

        var groupBuilder = app.MapGroup("artistas")
                              .RequireAuthorization()
                              .WithTags("Artistas");

        #region Endpoint Artistas


        groupBuilder.MapGet("", ([FromServices] Repository<Artista> dal) =>
        {
            var listaDeArtistas = dal.Listar();
            if (listaDeArtistas is null)
            {
                return Results.NotFound();
            }
            var listaDeArtistaResponse = EntityListToResponseList(listaDeArtistas);
            return Results.Ok(listaDeArtistaResponse);
        });

        groupBuilder.MapGet("{nome}", async ([FromServices] Repository<Artista> dal, string nome) =>
        {
            // var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            var artista = await dal.GetAll().AsNoTracking()
                                              .Include(a => a.Avaliacoes)
                                              .Include(a => a.Musicas)
                                              .Where(a => a.Nome.ToUpper().Equals(nome.ToUpper()))
                                              .FirstOrDefaultAsync();


            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(EntityToResponse(artista));

        });

        groupBuilder.MapPost("", async ([FromServices] IHostEnvironment env, [FromServices] Repository<Artista> dal, [FromBody] ArtistaInput artistaRequest) =>
        {
            var nome = artistaRequest.Nome.Trim();
            var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpeg";

            var path = Path.Combine(env.ContentRootPath,
    "wwwroot", "FotosPerfil", imagemArtista);

            using MemoryStream ms = new(Convert.FromBase64String(artistaRequest.FotoPerfil!));
            using FileStream fs = new(path, FileMode.Create);
            await ms.CopyToAsync(fs);

            var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio)
            {
                FotoPerfil = $"/FotosPerfil/{imagemArtista}"
            };

            dal.Adicionar(artista);
            return Results.Ok();
        });

        groupBuilder.MapDelete("{id}", ([FromServices] Repository<Artista> dal, int id) =>
        {
            var artista = dal.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound();
            }
            dal.Deletar(artista);
            return Results.NoContent();

        });

        groupBuilder.MapPut("", ([FromServices] Repository<Artista> dal, [FromBody] ArtistaEditInput artistaRequestEdit) =>
        {
            var artistaAAtualizar = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
            if (artistaAAtualizar is null)
                return Results.NotFound();

            artistaAAtualizar.Nome = artistaRequestEdit.Nome;
            artistaAAtualizar.Bio = artistaRequestEdit.Bio;
            dal.Atualizar(artistaAAtualizar);
            return Results.Ok();
        });


        groupBuilder.MapPost("{id}/avaliacao", (int id, HttpContext context,
                                           [FromBody] AvaliacaoArtistaInput request,
                                           [FromServices] Repository<Artista> dalArtista,
                                           [FromServices] Repository<PessoaComAcesso> dalPessoa) =>
        {
            var artista = dalArtista.RecuperarPor(a => a.Id == id);
            if (artista is null)
            {
                return Results.NotFound();
            }
            var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ??
                throw new InvalidOperationException("Pessoa não está conectada.");

            var pessoa = dalPessoa.RecuperarPor(p => p.Email.Equals(email)) ??
                throw new InvalidOperationException("Pessoa não está conectada");

            var avaliacao = artista.Avaliacoes
                    .FirstOrDefault(a => a.ArtistaId == artista.Id && a.PessoaId == pessoa.Id);

            if (avaliacao is null)
                artista.AdicionarNota(pessoa.Id, request.Nota);
            else
                avaliacao.Nota = request.Nota;


            dalArtista.Atualizar(artista);

            return Results.Created();
        });

        groupBuilder.MapGet("{id}/avaliacao", (int id,
                                               HttpContext context,
                                               [FromServices] Repository<Artista> dalArtista,
                                               [FromServices] Repository<PessoaComAcesso> dalPessoa) =>
        {
            var artista = dalArtista.RecuperarPor(a => a.Id == id);
            if (artista is null) return Results.NotFound();
            var email = context.User.Claims
                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            var pessoa = dalPessoa.RecuperarPor(p => p.Email!.Equals(email))
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            var avaliacao = artista
                .Avaliacoes
                .FirstOrDefault(a => a.ArtistaId == id && a.PessoaId == pessoa.Id);

            if (avaliacao is null) return Results.Ok(new AvaliacaoArtistaOutput(id, 0));
            else return Results.Ok(new AvaliacaoArtistaOutput(id, avaliacao.Nota));
        });

        #endregion
    }

    private static ICollection<ArtistaOutput> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
    {
        return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaOutput EntityToResponse(Artista artista)
    {
        return new ArtistaOutput(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil)
        {
            Classificacao = artista.Avaliacoes
                                .Select(a => a.Nota)
                                .DefaultIfEmpty(0)
                                .Average(),
            Musicas = artista.Musicas.Count > 0 ? [.. artista.Musicas.Select(m => new MusicaOutput(m.Id, m.Nome, m.ArtistaId, artista.Nome, m.AnoLancamento))]
                      : null
        };
    }


}
