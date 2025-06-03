using MarketingAppHJ.Cliente.ViewModels.CambioDatosPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CambioDatosPage;

public partial class CambioDatosPage : ContentPage
{
	private readonly CambioDatosPageViewModel _viewModel;
    public CambioDatosPage(CambioDatosPageViewModel viewModel)
	{
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CambioDatosPageViewModel vm)
        {
            await vm.CargarDatosUsuarioAsync();
        }
    }
}