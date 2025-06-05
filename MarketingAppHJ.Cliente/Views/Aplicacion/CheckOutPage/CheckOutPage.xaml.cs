using MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;

/// <summary>
/// Representa la página de pago donde los usuarios pueden revisar y confirmar su carrito de compras.
/// </summary>
public partial class CheckOutPage : ContentPage
{
    readonly CheckOutPageViewModel _vm;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="CheckOutPage"/>.
    /// </summary>
    /// <param name="vm">El ViewModel asociado a esta página.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el ViewModel proporcionado es nulo.</exception>
    public CheckOutPage(CheckOutPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }

    /// <summary>
    /// Método llamado cuando la página aparece en pantalla.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadCartAsync();
    }

    /// <summary>
    /// Maneja el evento de clic en el botón de retroceso.
    /// </summary>
    /// <param name="sender">El objeto que desencadenó el evento.</param>
    /// <param name="e">Los datos del evento.</param>
    void OnBackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void SettingsClicked(object sender, EventArgs e)
    {
        //Shell.Current.GoToAsync("ajustes");
    }

    private void OnLogoClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("main");
    }

    private void OnProfileClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("perfil");
    }
}
