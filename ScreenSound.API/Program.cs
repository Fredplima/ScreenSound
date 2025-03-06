using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Banco;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.ConfigureAppConfiguration(config =>
//{
//    var settings = config.Build();
// //   config.AddAzureAppConfiguration("Endpoint=https://screensound-configuration.azconfig.io;Id=3HVQ;Secret=wF97RuK+avtFwg6iHERM8yeIDBJ2joABSih75njg7N4=");
//});

builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options
            .UseSqlServer(builder.Configuration["ConnectionStrings:ScreenSoundDB"])
            .UseLazyLoadingProxies();
});

builder.Services
    .AddIdentityApiEndpoints<PessoaComAcesso>()
    .AddEntityFrameworkStores<ScreenSoundContext>();

builder.Services.AddAuthorization();

builder.Services.AddScoped<Repository<PessoaComAcesso>>();
builder.Services.AddScoped<Repository<Artista>>();
builder.Services.AddScoped<Repository<Musica>>();
builder.Services.AddScoped<Repository<Genero>>();
builder.Services.AddScoped<Repository<AvaliacaoArtista>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ignora prop. ciclicas no DTO
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
       options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy =>
             policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7089",
             builder.Configuration["FrontendUrl"] ?? "https://localhost:7015"])
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));

var app = builder.Build();

app.UseCors("wasm");

//app.UseCors(options =>
//{
//    options.AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader();
//});

app.UseStaticFiles();

app.UseAuthorization();


// Build endpoints
app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
app.AddEndPointGeneros();

// Configurações IdentityApi
app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>()
    .WithTags("Autorização");

// Logoff endpoint override
app.MapPost("auth/logout", async ([FromServices] SignInManager<PessoaComAcesso> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization().WithTags("Autorização");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();