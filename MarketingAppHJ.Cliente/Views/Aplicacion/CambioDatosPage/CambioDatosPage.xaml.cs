using MarketingAppHJ.Cliente.ViewModels.CambioDatosPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CambioDatosPage;

/// <summary>
/// Representa la página para cambiar los datos del usuario.
/// </summary>
public partial class CambioDatosPage : ContentPage
{
    private readonly CambioDatosPageViewModel _viewModel;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CambioDatosPage"/>.
    /// </summary>
    /// <param name="viewModel">El ViewModel asociado a esta página.</param>
    public CambioDatosPage(CambioDatosPageViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Método que se ejecuta cuando la página aparece.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CambioDatosPageViewModel vm)
        {
            await vm.CargarDatosUsuarioAsync();
        }
    }
}
