using MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;

public partial class CheckOutPage : ContentPage
{
    readonly CheckOutPageViewModel _vm;

    public CheckOutPage(CheckOutPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Carga los ítems del carrito para mostrar el resumen
        await _vm.LoadCartAsync();
    }
}