using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.MainPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.MainPage
{
    /// <summary>
    /// Representa la página de catálogo en la aplicación.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = IPlatformApplication.Current?.Services?.GetService<MainPageViewModel>();
            if (_viewModel == null)
            {
                throw new InvalidOperationException("No se pudo obtener el servicio MainPageViewModel.");
            }
            BindingContext = _viewModel;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (_viewModel as IDisposable)?.Dispose();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Solución para CS8602: Verificar que _viewModel no sea nulo antes de usarlo
            if (_viewModel != null)
            {
                await _viewModel.CargarProductosAsync();
            }
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
