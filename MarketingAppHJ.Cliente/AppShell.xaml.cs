using MarketingAppHJ.Cliente.Views.Aplicacion.CambioDatosPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.MainPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.PedidosPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.ProfilePage;
using MarketingAppHJ.Cliente.Views.Aplicacion.SettingsPage;
using MarketingAppHJ.Cliente.Views.Login.LoginPage;
using MarketingAppHJ.Cliente.Views.Login.RegisterPage;
using MarketingAppHJ.Cliente.Views.Login.ResetPage;

namespace MarketingAppHJ.Cliente
{
    /// <summary>
    /// Representa la clase principal de la aplicación que define las rutas de navegación.
    /// </summary>
    public partial class AppShell : Shell
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AppShell"/>.
        /// </summary>
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
            Routing.RegisterRoute("pedidos", typeof(PedidosPage));
            Routing.RegisterRoute("ajustes", typeof(SettingsPage));
            Routing.RegisterRoute("shell", typeof(AppShell));
        }
    }
}
