using MarketingAppHJ.Cliente.ViewModels.LoginPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Login.LoginPage;

/// <summary>
/// Representa la página de inicio de sesión de la aplicación.
/// </summary>
public partial class LoginPage : ContentPage
{
    /// <summary>
    /// ViewModel asociado a la página de inicio de sesión.
    /// </summary>
    readonly LoginPageViewModel _vm;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="LoginPage"/>.
    /// </summary>
    /// <param name="vm">El ViewModel que se vinculará a esta página.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el ViewModel proporcionado es nulo.</exception>
    public LoginPage(LoginPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }

    /// <summary>
    /// Método que se ejecuta cuando la página aparece en pantalla.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Limpiar campos cuando se muestra
        _vm.Email = string.Empty;
        _vm.Password = string.Empty;
    }
}
