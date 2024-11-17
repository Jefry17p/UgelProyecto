using AppUgel.Service;
using AppUgel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Text.Json;

namespace AppUgel.Pages
{
    public partial class EquiposPages : ContentPage
    {
        private ApiServiceEquipos _apiServiceEquipos;
        private List<EquiposCLS> _equiposOriginal;
        private ActivityIndicator loadingIndicator;

        public EquiposPages()
        {
            InitializeComponent();
            _apiServiceEquipos = new ApiServiceEquipos();

            // Llamar al método de carga de forma asíncrona
            Task.Run(() => LoadEquiposAsync());
        }

        private async Task LoadEquiposAsync()
        {
            try
            {
                // Mostrar indicador de actividad desde la UI
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    IsBusy = true;
                });

                var equipos = await _apiServiceEquipos.GetEquiposAsync();

                if (equipos != null && equipos.Any())
                {
                    _equiposOriginal = equipos;

                    // Actualizar UI en el hilo principal
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        EquiposListView.ItemsSource = _equiposOriginal;
                        DisplayAlert("Éxito", $"Se cargaron {equipos.Count} equipos", "OK");
                    });
                }
                else
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        _equiposOriginal = new List<EquiposCLS>();
                        EquiposListView.ItemsSource = _equiposOriginal;
                        DisplayAlert("Información", "No se encontraron equipos", "OK");
                    });
                }
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    DisplayAlert("Error", $"Error al cargar equipos: {ex.Message}", "OK");
                });
            }
            finally
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    IsBusy = false;
                });
            }
        }

        // Agregar un botón de recarga
        private void AddRefreshButton()
        {
            var refreshButton = new Button
            {
                Text = "Recargar",
                Command = new Command(async () => await LoadEquiposAsync())
            };
            ((StackLayout)Content).Children.Insert(1, refreshButton);
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
            string searchText = e.NewTextValue;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                EquiposListView.ItemsSource = _equiposOriginal;
            }
            else
            {
                var filteredEquipos = _equiposOriginal?.Where(equipo =>
                    equipo.SerieEqui?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true ||
                    equipo.AreaEqui?.Contains(searchText, StringComparison.OrdinalIgnoreCase) == true).ToList();
                EquiposListView.ItemsSource = filteredEquipos;
            }
        }

        private async void RegistrarEquipo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEquipoPage());
        }
    }
}
