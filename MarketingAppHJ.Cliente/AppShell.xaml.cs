using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;

namespace MarketingAppHJ.Cliente
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("detalles", typeof(DetailsPage));
        }
    }
}
