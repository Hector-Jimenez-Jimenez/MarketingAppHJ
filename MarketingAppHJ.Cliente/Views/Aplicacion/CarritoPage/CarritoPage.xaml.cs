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
    }
}
