using ScreenSound.Web.Services.Artistas.Requests;
using ScreenSound.Web.Services.Artistas.Responses;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services.Artistas;

public class ArtistasAPI
{
    private readonly HttpClient _httpClient;
    public ArtistasAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
    }

    public async Task AddArtistaAsync(ArtistaRequest artista)
    {
        await _httpClient.PostAsJsonAsync("artistas", artista);
    }

    public async Task DeleteArtistaAsync(int id)
    {
        await _httpClient.DeleteAsync($"artistas/{id}");
    }

    public async Task<ArtistaResponse?> GetArtistaPorNomeAsync(string nome)
    {        
        return await _httpClient.GetFromJsonAsync<ArtistaResponse>($"artistas/{nome}");
    }
    public async Task UpdateArtistaAsync(ArtistaRequestEdit artista)
    {
        await _httpClient.PutAsJsonAsync($"artistas", artista);
    }

    public async Task AvaliaArtistaAsync(int artistaId, double nota)
    {
        await _httpClient.PostAsJsonAsync("artistas/avaliacao", new { artistaId, nota });
    }

    public async Task<AvaliacaoArtistaResponse?> GetAvaliacaoDaPessoaLogadaAsync(int artistaId)
    {
        return await _httpClient
            .GetFromJsonAsync<AvaliacaoArtistaResponse?>($"artistas/{artistaId}/avaliacao");
    }

}
