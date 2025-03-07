using Microsoft.AspNetCore.Mvc;
using ScreenSound.Core.Musicas;
using ScreenSound.Shared.API.Generos.Dtos;
using ScreenSound.Shared.Dados.Banco;

namespace ScreenSound.API.Endpoints;

public static class GeneroExtensions
{

    public static void AddEndPointGeneros(this WebApplication app)
    {

        var groupBuilder = app.MapGroup("generos")
                      .RequireAuthorization()
                      .WithTags("Generos");

        groupBuilder.MapPost("", ([FromServices] Repository<Genero> dal, [FromBody] GeneroInput generoReq) =>
        {
            dal.Adicionar(RequestToEntity(generoReq));
        });


        groupBuilder.MapGet("", ([FromServices] Repository<Genero> dal) =>
        {
            return EntityListToResponseList(dal.Listar());
        });

        groupBuilder.MapGet("{nome}", ([FromServices] Repository<Genero> dal, string nome) =>
        {
            var genero = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (genero is not null)
            {
                var response = EntityToResponse(genero!);
                return Results.Ok(response);
            }
            return Results.NotFound("Gênero não encontrado.");
        });

        groupBuilder.MapDelete("{id}", ([FromServices] Repository<Genero> dal, int id) =>
        {
            var genero = dal.RecuperarPor(a => a.Id == id);
            if (genero is null)
            {
                return Results.NotFound("Gênero para exclusão não encontrado.");
            }
            dal.Deletar(genero);
            return Results.NoContent();
        });
    }

    private static Genero RequestToEntity(GeneroInput generoRequest)
    {
        return new Genero() { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao };
    }

    private static ICollection<GeneroOutput> EntityListToResponseList(IEnumerable<Genero> generos)
    {
        return generos.Select(a => EntityToResponse(a)).ToList();
    }

    private static GeneroOutput EntityToResponse(Genero genero)
    {
        return new GeneroOutput(genero.Id, genero.Nome!, genero.Descricao!);
    }
}
