using AppUgel.Class;
using AppUgel.Service;

namespace AppUgel.Pages;

public partial class MenuPage : ContentPage
{
    public List<menuCLS> listaMenu { get; set; }
    public menuCLS omenuCLS { get; set; }

    public MenuPage()
    {
        InitializeComponent();

        // Definir los elementos del menú
        listaMenu = new List<menuCLS>()
        {
            new menuCLS() { codMenu = 1, nomMenu = "Equipos", imgMenu = "iclist.png" },
            new menuCLS() { codMenu = 2, nomMenu = "Mantenimientos", imgMenu = "icsettings.png" },
            new menuCLS() { codMenu = 3, nomMenu = "Pendientes", imgMenu = "icnoti.png" },
            new menuCLS() { codMenu = 4, nomMenu = "Historial", imgMenu = "ichistory.png" }
        };

        BindingContext = this; // Enlazar la vista con los datos
    }


    private void lstMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        omenuCLS = e.SelectedItem as menuCLS;

        if (omenuCLS == null) return;

        Page nexPage = null;

        switch (omenuCLS.codMenu)
        {
            case 1:
                nexPage = new EquiposPages();
                break;
        }

        if (nexPage != null)
        {
            App.Navigate.PushAsync(nexPage);
        }

        App.menu.IsPresented = false;

    }
}
