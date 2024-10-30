using System.Net.Http.Json;
using System.Net.Http;
using AppUgel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUgel.Service
{
    public class ApiServiceMantenimiento
    {
        private readonly HttpClient _httpClient;

        public ApiServiceMantenimiento()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://apiugel.somee.com/");
        }

        public async Task<List<MantenimientoCLS>> GetMantenimientosAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/Mantenimientos");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<List<MantenimientoCLS>>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener mantenimientos: {ex.Message}");
                return new List<MantenimientoCLS>();
            }
        }
        public async Task<MantenimientoCLS> GetMantenimientoByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MantenimientoCLS>($"/api/Mantenimientos/{id}");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error al obtener mantenimiento: {ex.Message}");
                return null;
            }
        }
    }
}
