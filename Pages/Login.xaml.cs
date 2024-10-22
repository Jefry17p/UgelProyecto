namespace AppUgel.Pages;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private void btnAcceso(object sender, EventArgs e)
    {
        App.Current.MainPage = new PrincipalPage();
    }
}