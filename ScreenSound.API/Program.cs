using Microsoft.AspNetCore.Http.Json;
using ScreenSound.Core.Modelos;
using ScreenSound.EntityFrameworkCore.Banco;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Desabilita a serialização de referências cíclicas
builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();


app.MapGet("/", () =>
{
    var artistaRepository = new Repository<Artista>(new ScreenSoundContext());

    return artistaRepository.Listar();
});

app.Run();
