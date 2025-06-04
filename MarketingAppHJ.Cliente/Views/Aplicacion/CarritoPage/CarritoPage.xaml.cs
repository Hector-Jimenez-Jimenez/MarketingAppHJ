using System;
using System.Globalization;
using System.Linq;
using Firebase.Database.Streaming;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;

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
            await _vm.LoadCartAsync();
        }
        void OnBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }
        void OnGoToCatalogClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("main");
        }

        async void OnRemoveClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton btn
                && btn.CommandParameter is string idProducto)
            {
                await _vm.RemoveItemAsync(idProducto);
            }
        }

        async void OnClearClicked(object sender, EventArgs e)
        {
            await _vm.ClearCartAsync();
        }

        void OnCheckoutClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("checkout");
        }
    }
}
