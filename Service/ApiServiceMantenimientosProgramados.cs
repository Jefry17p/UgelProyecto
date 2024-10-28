using System.Net.Http.Json;
using System.Net.Http;
using AppUgel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppUgel.Service
{
    public class ApiServiceMantenimientosProgramados
    {
        private readonly HttpClient _httpClient;

        public ApiServiceMantenimientosProgramados()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://apiugel.somee.com/");
        }
    }
}
