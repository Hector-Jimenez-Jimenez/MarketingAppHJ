using MarketingAppHJ.Cliente.Views.Aplicacion.CambioDatosPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.MainPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.ProfilePage;
using MarketingAppHJ.Cliente.Views.Login.LoginPage;
using MarketingAppHJ.Cliente.Views.Login.RegisterPage;
using MarketingAppHJ.Cliente.Views.Login.ResetPage;

namespace MarketingAppHJ.Cliente
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("detalles", typeof(DetailsPage));
            Routing.RegisterRoute("carrito", typeof(CarritoPage));
            Routing.RegisterRoute("checkout", typeof(CheckOutPage));
            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("reset", typeof(ResetPage));
            Routing.RegisterRoute("profile", typeof(ProfilePage));
            Routing.RegisterRoute("changedata", typeof(CambioDatosPage));
        }
    }
}
