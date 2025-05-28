using MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;

namespace MarketingAppHJ.Cliente
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("detalles", typeof(DetailsPage));
            Routing.RegisterRoute("carrito", typeof(CarritoPage));
            Routing.RegisterRoute("checkout", typeof(CheckOutPage));
        }
    }
}
