using AppUgel.Pages;

namespace AppUgel
{
    public partial class App : Application
    {
        public static NavigationPage Navigate { get; internal set; }
        public static PrincipalPage menu { get; internal set; }

        public App()
        {
            InitializeComponent();

            Navigate = new NavigationPage(new Home());
            App.Current.MainPage = Navigate;
            menu = new PrincipalPage();
        }

        public static async Task NavigateToAsync(Page page)
        {
            if (Navigate != null)
            {
                await Navigate.PushAsync(page);
            }
        }
    }
}
