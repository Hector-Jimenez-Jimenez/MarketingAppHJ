using MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.PedidosPage;

public partial class PedidosPage : ContentPage
{
    private readonly PedidosPageViewModel _vm;

    public PedidosPage(PedidosPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_vm.Pedidos.Count == 0)
            await _vm.LoadPedidosAsync();
    }

}