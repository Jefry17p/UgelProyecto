using AppUgel.Service;
using AppUgel.Models;
using System.Windows.Input;

namespace AppUgel.Pages;

public partial class AddEquipoPage : ContentPage
{
	private readonly ApiServiceEquipos _apiServiceEquipos;
	public ICommand RegisterCommand { get; set; }

    public AddEquipoPage()
	{
		InitializeComponent();
		_apiServiceEquipos = new ApiServiceEquipos();

		RegisterCommand = new Command(async () => await RegisterEquipo());

		BindingContext=this;
    }
	private async Task RegisterEquipo()
	{
		if(string.IsNullOrWhiteSpace(NombreEntry.Text)|| string.IsNullOrWhiteSpace(TipoEntry.Text)
			|| string.IsNullOrWhiteSpace(SerieEntry.Text) || string.IsNullOrWhiteSpace(MarcaEntry.Text) || string.IsNullOrWhiteSpace(ModeloEntry.Text)
			|| string.IsNullOrWhiteSpace(AreaEntry.Text))
		{
			await DisplayAlert("Error","Todos los campos son obligatorios","OK");
			return;
		}
		if (EstadoPicker.SelectedItem == null)
		{
			await DisplayAlert("Error","Debes seleccionar un estado", "OK");
			return;
		}
        if (FechaAdquiPicker.Date > DateTime.Now)
        {
            await DisplayAlert("Error", "La fecha de adquisición no puede ser futura", "OK");
            return;
        }
        if (FechaRegisPicker.Date > DateTime.Now)
		{
            await DisplayAlert("Error", "La fecha de registro no puede ser futura", "OK");
            return;
        }

		var nuevoEquipo = new EquiposCLS
		{
			NombreEqui = NombreEntry.Text,
			TipoEqui = TipoEntry.Text,
            SerieEqui = SerieEntry.Text,
			MarcaEqui = MarcaEntry.Text,
			ModeloEqui = ModeloEntry.Text,
			AreaEqui= AreaEntry.Text,
			EstadoEqui = EstadoPicker.SelectedItem.ToString(),
			FechaAdquisicion = FechaAdquiPicker.Date,
			FechaRegistro = FechaRegisPicker.Date
        };

		var equipoRegistrado = await _apiServiceEquipos.AddEquipoAsync(nuevoEquipo);
		if (equipoRegistrado != null)
		{
			await DisplayAlert("Éxito", "Equipo registrado correctamente", "OK");
			await Navigation.PushAsync(new EquiposPages());
            Navigation.RemovePage(this);
        }
		else 
		{
			await DisplayAlert("Error","No se pudo registrar el equipo","OK");
		}
	}
}