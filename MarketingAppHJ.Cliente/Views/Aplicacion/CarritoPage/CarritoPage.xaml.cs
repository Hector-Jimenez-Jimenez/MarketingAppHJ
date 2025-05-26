using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage;

public partial class CarritoPage : ContentPage
{
    readonly CarritoPageViewModel _vm;
    public CarritoPage(CarritoPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.ObtenerCarrito();
    }
}