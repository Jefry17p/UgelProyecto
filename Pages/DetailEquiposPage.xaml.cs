using AppUgel.Class;
using AppUgel.Models;
using AppUgel.Service;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace AppUgel.Pages;

public partial class DetailEquiposPage : ContentPage
{
    private readonly ApiServiceEquipos _apiServiceEquipos;
    public EquiposCLS Equipo { get; set; }
    public ICommand UpdateCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    // Constructor que recibe el equipo seleccionado
    public DetailEquiposPage(EquiposCLS equipoSeleccionado)
    {
        InitializeComponent();
        _apiServiceEquipos = new ApiServiceEquipos();

        // Asignar el equipo seleccionado a la propiedad Equipo
        Equipo = equipoSeleccionado;

        UpdateCommand = new Command(async () => await UpdateEquipo());
        DeleteCommand = new Command(async () => await DeleteEquipo());

        BindingContext = this;
    }

    private async Task UpdateEquipo()
    {
        bool success = await _apiServiceEquipos.UpdateEquipoAsync(Equipo.IdEquipo, Equipo);
        if (success)
        {
            await DisplayAlert("Actualizado", "El equipo fue actualizado correctamente", "OK");
            await Navigation.PopAsync(); // Regresar a la página anterior
        }
        else
        {
            await DisplayAlert("Error", "No se pudo actualizar el equipo", "OK");
        }
    }

    private async Task DeleteEquipo()
    {
        bool confirm = await DisplayAlert("Eliminar", "¿Estás seguro de eliminar este equipo?", "Sí", "No");
        if (confirm)
        {
            bool success = await _apiServiceEquipos.DeleteEquipoAsync(Equipo.IdEquipo);
            if (success)
            {
                await DisplayAlert("Eliminado", "El equipo fue eliminado correctamente", "OK");
                await Navigation.PushAsync(new EquiposPages()); // Regresar a la página anterior
            }
            else
            {
                await DisplayAlert("Error", "No se pudo eliminar el equipo", "OK");
            }
        }
    }
}
