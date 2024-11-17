using System.Net.Http.Json;
using System.Net.Http;
using AppUgel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using System.Text.Json;
public class ApiServiceEquipos
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiServiceEquipos()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://apiugel.somee.com/"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        // Configurar headers
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        // Configurar opciones de serialización
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = ReferenceHandler.Preserve,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<List<EquiposCLS>> GetEquiposAsync()
    {
        try
        {
            // Asegurarnos de usar la ruta correcta del controlador
            var response = await _httpClient.GetAsync("api/Equipos");

            // Log para debugging
            Console.WriteLine($"URL solicitada: {_httpClient.BaseAddress}api/Equipos");
            Console.WriteLine($"Status Code: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Contenido de la respuesta: {content}");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Error del servidor: {response.StatusCode} - {content}");
            }

            // Usar las opciones de serialización configuradas
            var equipos = JsonSerializer.Deserialize<List<EquiposCLS>>(
                content, _jsonOptions);

            return equipos ?? new List<EquiposCLS>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error de HTTP: {ex.Message}");
            throw;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error de deserialización: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error general: {ex.Message}");
            throw;
        }
    }

    public async Task<EquiposCLS> GetEquipoByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/Equipos/{id}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<EquiposCLS>(content, _jsonOptions);
    }

    public async Task<EquiposCLS> AddEquipoAsync(EquiposCLS nuevoEquipo)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Equipos", nuevoEquipo);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<EquiposCLS>(content, _jsonOptions);
    }

    public async Task<bool> UpdateEquipoAsync(int id, EquiposCLS equipoActualizado)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Equipos/{id}", equipoActualizado);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteEquipoAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Equipos/{id}");
        return response.IsSuccessStatusCode;
    }
}