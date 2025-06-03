using System;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.MainPageViewModel;
using Microsoft.Maui.Controls;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.MainPage
{
    /// <summary>
    /// Representa la página de catálogo con paginación infinita
    /// y un botón para desplegar el carrito.
    /// </summary>
    public partial class MainPage : ContentPage
    {
        readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            _viewModel = IPlatformApplication.Current
                            .Services
                            .GetService<MainPageViewModel>()
                        ?? throw new InvalidOperationException("MainPageViewModel no disponible.");

            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Carga inicial: página 1
            _viewModel.LoadNextPage();
        }

        /// <summary>
        /// Se dispara cuando quedan 2 ítems por visualizar,
        /// para cargar la siguiente página.
        /// </summary>
        private void OnRemainingItemsThresholdReached(object sender, EventArgs e)
            => _viewModel.LoadNextPage();

        /// <summary>
        /// Navega a la página de detalle del producto seleccionado.
        /// </summary>
        private async void OnItemTapped(object sender, EventArgs e)
        {
            if ((sender as Border)?.BindingContext is ProductoDto prod)
            {
                await Shell.Current.GoToAsync($"detalles?productoId={prod.Id}");
            }
        }

        /// <summary>
        /// Handler del botón “Despliega tu carrito”
        /// (aún implementa la navegación a la página de carrito).
        /// </summary>
        private async void OnCartClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("carrito");
        }

        private async void OnAvatarClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("profile");
        }

        private async void OnLogoClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("main");
        }

        void OnSettingsClicked(object sender, EventArgs e)
        {
            // Ir a ajustes...
        }
        void OnMenuClicked(object sender, EventArgs e)
        {
            if (Shell.Current.FlyoutBehavior != FlyoutBehavior.Disabled)
                Shell.Current.FlyoutIsPresented = true;
        }
    }
}
