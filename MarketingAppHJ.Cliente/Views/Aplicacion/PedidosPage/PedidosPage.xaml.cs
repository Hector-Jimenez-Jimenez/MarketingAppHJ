using System;
using MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel;
using Microsoft.Maui.Controls;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.PedidosPage
{
    /// <summary>
    /// Representa la página de pedidos en la aplicación.
    /// </summary>
    public partial class PedidosPage : ContentPage
    {
        readonly PedidosPageViewModel _viewModel;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PedidosPage"/>.
        /// </summary>
        /// <param name="vm">El ViewModel asociado a la página.</param>
        /// <exception cref="ArgumentNullException">Se lanza si el ViewModel es nulo.</exception>
        public PedidosPage(PedidosPageViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm ?? throw new ArgumentNullException(nameof(vm));
            BindingContext = _viewModel;
        }

        /// <summary>
        /// Método llamado cuando la página aparece.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadPedidosAsync();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón para ver detalles de un pedido.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        public async void OnVerDetallesClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string orderId)
            {
                 //await Shell.Current.GoToAsync($"detallepedido?pedidoId={orderId}");
            }
        }
    }
}
