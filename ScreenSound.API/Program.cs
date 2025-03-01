
using Microsoft.AspNetCore.Mvc;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Banco;
using ScreenSound.Web.Host.services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Adiciona Context
builder.Services.AddDbContext<ScreenSoundContext>();
// Adiciona os repositórios ao container de injeção de dependência
builder.Services.AddTransient<Repository<Artista>>();
builder.Services.AddTransient<Repository<Musica>>();

builder.Services.AddTransient<AppServiceBase<Artista>>();
builder.Services.AddTransient<AppServiceBase<Musica>>();

builder.Services.AddTransient<ArtistaAppService>();

// Desabilita a serialização de referências cíclicas
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();


// Artistas Services
app.MapGet("/Artistas", ([FromServices] ArtistaAppService artistaAppService) =>
{
    return artistaAppService.Listar();
});

app.MapGet("/Artistas/{nome}", ([FromServices] ArtistaAppService artistaAppService, string nome) =>
{
    return artistaAppService.RecuperarArtistaComMusicas(nome);
});

app.MapPost("/Artistas", ([FromServices] ArtistaAppService artistaAppService, [FromBody] Artista artista) =>
{
    return artistaAppService.Adicionar(artista);
});

app.MapDelete("/Artistas/{id}", ([FromServices] ArtistaAppService artistaAppService, int id) =>
{
    return artistaAppService.Deletar(id);
});

app.MapPut("/Artistas", ([FromServices] ArtistaAppService artistaAppService, [FromBody] Artista artista) =>
{
    return artistaAppService.Atualizar(artista, a =>
    {
        a.Nome = artista.Nome;
        a.Bio = artista.Bio;
        a.FotoPerfil = artista.FotoPerfil;
    });
});

// Musicas Services
app.MapGet("/Musicas", ([FromServices] AppServiceBase<Musica> musicaAppService) =>
{
    return musicaAppService.Listar();
});

app.MapGet("/Musicas/{nome}", ([FromServices] AppServiceBase<Musica> musicaAppService, string nome) =>
{
    return musicaAppService.RecuperarPor(x=> x.Nome.ToLower().Equals(nome.ToLower().Trim()));
});

app.MapPost("/Musicas", ([FromServices] AppServiceBase<Musica> musicaAppService, [FromBody] Musica musica) =>
{
    return musicaAppService.Adicionar(musica);
});

app.MapDelete("/Musicas/{id}", ([FromServices] AppServiceBase<Musica> musicaAppService, int id) =>
{
    return musicaAppService.Deletar(id);
});

app.MapPut("/Musicas", ([FromServices] AppServiceBase<Musica> musicaAppService, [FromBody] Musica musica) =>
{
    return musicaAppService.Atualizar(musica, a =>
    {
        a.Nome = musica.Nome;
        a.AnoLancamento = musica.AnoLancamento;
        a.ArtistaId = musica.ArtistaId;        
    });
});

app.Run();
