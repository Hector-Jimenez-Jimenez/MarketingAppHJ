using System;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;
using Microsoft.Maui.Controls;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage
{
    public partial class CarritoPage : ContentPage
    {
        readonly CarritoPageViewModel _vm;

        public CarritoPage(CarritoPageViewModel vm)
        {
            InitializeComponent();
            _vm = vm ?? throw new ArgumentNullException(nameof(vm));
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Carga inicial del carrito
            await _vm.CargarCarritoAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void EliminarClicked(object sender, EventArgs e)
        {
            if ((sender as Button)?.CommandParameter is string prodId)
                await _vm.EliminarItemAsync(prodId);
        }

        private async void VaciarClicked(object sender, EventArgs e)
        {
            await _vm.VaciarCarritoAsync();
        }
    }
}
