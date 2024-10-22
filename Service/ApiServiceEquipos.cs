using System.Net.Http.Json;
using System.Net.Http;
using AppUgel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUgel.Service
{
    public class ApiServiceEquipos
    {
        private readonly HttpClient _httpClient;

        public ApiServiceEquipos()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://apiugel.somee.com/");
        }

        // Obtener todos los equipos
        public async Task<List<EquiposCLS>> GetEquiposAsync()
        {
            try
            {
                // Corrigiendo la ruta: agregar barra al final
                var response = await _httpClient.GetFromJsonAsync<List<EquiposCLS>>("api/equipos/");
                return response ?? new List<EquiposCLS>(); // En caso de que la respuesta sea null, devolvemos una lista vacía.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener equipos: {ex.Message}");
                return new List<EquiposCLS>();
            }
        }

        // Obtener equipo por ID
        public async Task<EquiposCLS> GetEquipoByIdAsync(int id)
        {
            try
            {
                // Corrigiendo la ruta: agregar barra al final y antes del ID
                var response = await _httpClient.GetFromJsonAsync<EquiposCLS>($"api/equipos/{id}/");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener equipo: {ex.Message}");
                return null;
            }
        }

        // Agregar un nuevo equipo
        public async Task<EquiposCLS> AddEquipoAsync(EquiposCLS nuevoEquipo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/equipos/", nuevoEquipo);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EquiposCLS>();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar equipo: {ex.Message}");
                return null;
            }
        }

        // Actualizar un equipo existente
        public async Task<bool> UpdateEquipoAsync(int id, EquiposCLS equipoActualizado)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/equipos/{id}/", equipoActualizado);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar equipo: {ex.Message}");
                return false;
            }
        }

        // Eliminar un equipo
        public async Task<bool> DeleteEquipoAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/equipos/{id}/");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar equipo: {ex.Message}");
                return false;
            }
        }
    }

}

