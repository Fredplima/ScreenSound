using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API;
using ScreenSound.API.Endpoints;
using ScreenSound.Core.Artistas;
using ScreenSound.Core.Authorizations;
using ScreenSound.Core.Musicas;
using ScreenSound.Shared.Dados.Banco;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configura��o de Servi�os
builder.Services.AddDbContext<ScreenSoundContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ScreenSoundDB"])
           .UseLazyLoadingProxies());

builder.Services.AddIdentityApiEndpoints<PessoaComAcesso>()
                .AddEntityFrameworkStores<ScreenSoundContext>();

builder.Services.AddAuthorization();

builder.Services.AddScoped<Repository<PessoaComAcesso>>();
builder.Services.AddScoped<Repository<Artista>>();
builder.Services.AddScoped<Repository<Musica>>();
builder.Services.AddScoped<Repository<Genero>>();
builder.Services.AddScoped<Repository<AvaliacaoArtista>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(options =>
    options.AddPolicy("wasm", policy =>
        policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7089",
                           builder.Configuration["FrontendUrl"] ?? "https://localhost:7015"])
              .AllowAnyMethod()
              .SetIsOriginAllowed(pol => true)
              .AllowAnyHeader()
              .AllowCredentials()));

// Registrar Health Checks (ANTES de builder.Build())
builder.Services.AddHealthChecks()
               .AddCheck<DbContextHealthCheck<ScreenSoundContext>>("ScreenSoundDBHealthCheck");

var app = builder.Build(); // A partir daqui, a cole��o de servi�os � read-only

// Configura��o de Middleware
app.UseCors("wasm");
app.UseStaticFiles();
app.UseAuthorization();

// Endpoints
app.AddEndPointsArtistas();
app.AddEndPointsMusicas();
app.AddEndPointGeneros();

// Configura��es IdentityApi
app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>()
   .WithTags("Autoriza��o");

// Logoff endpoint override
app.MapPost("auth/logout", async ([FromServices] SignInManager<PessoaComAcesso> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization().WithTags("Autoriza��o");

// Health Check Endpoint
app.MapHealthChecks("/health");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();