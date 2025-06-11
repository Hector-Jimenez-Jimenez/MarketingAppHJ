using MarketingAppHJ.Cliente.ViewModels.DetallesPedidoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.DetallesPedidoPage;

public partial class DetallesPedidoPage : ContentPage
{
    public DetallesPedidoPage(DetallesPedidoPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnVolverClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("pedidos");
    }
}