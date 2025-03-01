using ScreenSound.Core.Artistas;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Banco;
using ScreenSound.EntityFrameworkCore.Repositories;
using ScreenSound.Web.Host.Endpoints;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Desabilita a serialização de referências cíclicas
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();


// Endpoint
app.AddArtistasEndpoints();
app.AddMusicasEndpoints();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScreenSound API");
});

app.Run();
