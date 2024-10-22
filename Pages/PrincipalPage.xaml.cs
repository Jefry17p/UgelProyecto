namespace AppUgel.Pages;

public partial class PrincipalPage : FlyoutPage
{
	public PrincipalPage()
	{
		InitializeComponent();
        App.Navigate = Navigate;
        App.menu = this;
    }
}