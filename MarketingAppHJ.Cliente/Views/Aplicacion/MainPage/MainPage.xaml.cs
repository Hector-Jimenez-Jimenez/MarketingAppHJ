using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.MainPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.MainPage
{
    /// <summary>
    /// Representa la página de catálogo en la aplicación.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        public MainPage()
            : this(
                IPlatformApplication
                  .Current
                  .Services
                  .GetRequiredService<MainPageViewModel>()
              )
        {
        }

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            if (sender is Border border
                && border.BindingContext is ProductoDto prod)
            {
                Console.WriteLine($"[CATALOGO] Frame tapped: {prod.Id}");
                await Shell.Current.GoToAsync($"///detalles?productoId={prod.Id}");
            }
        }
        void OnAvatarClicked(object sender, EventArgs e)
        {
            // Navegar o abrir menú perfil
        }
        void OnSettingsClicked(object sender, EventArgs e)
        {
            // Navegar a ajustes
        }
        private void OnCartClicked(object sender, EventArgs e)
        {
            // Mostrar/ocultar carrito
        }
    }
}
