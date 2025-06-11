using System;
using System.Globalization;
using System.Linq;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage
{
    /// <summary>
    /// Representa la página del carrito de compras en la aplicación.
    /// </summary>
    public partial class CarritoPage : ContentPage
    {
        readonly CarritoPageViewModel _vm;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="CarritoPage"/>.
        /// </summary>
        /// <param name="vm">El ViewModel asociado a la página del carrito.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el ViewModel es nulo.</exception>
        public CarritoPage(CarritoPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm ?? throw new ArgumentNullException(nameof(vm));
            BindingContext = _vm;
        }

        /// <summary>
        /// Método llamado cuando la página aparece.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _vm.LoadCartAsync();
        }

        /// <summary>
        /// Maneja el evento de clic para regresar a la página anterior.
        /// </summary>
        void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("main");
        }

        /// <summary>
        /// Maneja el evento de clic para ir al catálogo principal.
        /// </summary>
        void OnGoToCatalogClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("main");
        }

        /// <summary>
        /// Maneja el evento de clic para eliminar un producto del carrito.
        /// </summary>
        /// <param name="sender">El botón que disparó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        async void OnRemoveClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn
                && btn.CommandParameter is string idProducto)
            {
                await _vm.RemoveItemAsync(idProducto);
            }
        }

        /// <summary>
        /// Maneja el evento de clic para vaciar el carrito.
        /// </summary>
        async void OnClearClicked(object sender, EventArgs e)
        {
            await _vm.ClearCartAsync();
        }

        /// <summary>
        /// Maneja el evento de clic para proceder al pago.
        /// </summary>
        void OnCheckoutClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("checkout");
        }

        private void SettingsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("ajustes");
        }

        private void OnLogoClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("main");
        }

        private void OnProfileClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("profile");
        }
    }
}
