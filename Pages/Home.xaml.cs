namespace AppUgel.Pages;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}
    private async void btnSesion(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new Pages.Login());
    }
}