using System;
using MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel;
using Microsoft.Maui.Controls;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.PedidosPage
{
    public partial class PedidosPage : ContentPage
    {
        readonly PedidosPageViewModel _viewModel;

        public PedidosPage(PedidosPageViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm ?? throw new ArgumentNullException(nameof(vm));
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadPedidosAsync();
        }

        void OnVerDetallesClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string pedidoId)
            {
                // Navegar a página de detalles de pedido, pasando el pedidoId
                //Shell.Current.GoToAsync($"pedidoDetalle?pedidoId={pedidoId}");
            }
        }
    }
}
