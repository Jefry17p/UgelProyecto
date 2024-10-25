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
        private List<EquiposCLS> _equiposOriginal;

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
                    _equiposOriginal = equipos;
                    // Asignar la lista de equipos al ListView
                    EquiposListView.ItemsSource = _equiposOriginal;
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

        // Agregar "async" a este método
        private async void EquiposListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null) return;

            var equipoSeleccionado = e.Item as EquiposCLS;
            if (equipoSeleccionado != null)
            {
                // Navegar a la página de detalles con el equipo seleccionado
                await Navigation.PushAsync(new DetailEquiposPage(equipoSeleccionado));
            }

            // Deseleccionar el ítem
            ((ListView)sender).SelectedItem = null;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText=e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                EquiposListView.ItemsSource = _equiposOriginal;
            }
            else
            {
                var filteredEquipos = _equiposOriginal.Where(equipo => equipo.SerieEqui != null && equipo.SerieEqui.Contains
                (searchText, StringComparison.OrdinalIgnoreCase) || equipo.AreaEqui != null && equipo.AreaEqui.Contains
                (searchText, StringComparison.OrdinalIgnoreCase)).ToList();

                EquiposListView.ItemsSource= filteredEquipos;
            }
        }

        private async void RegistrarEquipo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEquipoPage());
        }
    }
}
