using AppUgel.Service;
using AppUgel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AppUgel.Pages
{
    public partial class EquiposPages : ContentPage
    {
        private ApiServiceEquipos _apiServiceEquipos;

        public EquiposPages()
        {
            InitializeComponent();
            _apiServiceEquipos = new ApiServiceEquipos();
            loadEquiposAsync();
        }

        private async void loadEquiposAsync()
        {
            try
            {
                var equipos = await _apiServiceEquipos.GetEquiposAsync();

                if (equipos != null && equipos.Count > 0)
                {
                    // Asignar la lista de equipos al ListView
                    EquiposListView.ItemsSource = equipos;
                }
                else
                {
                    Console.WriteLine("No se cargaron equipos.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar equipos: {ex.Message}");
            }
        }
    }
}
