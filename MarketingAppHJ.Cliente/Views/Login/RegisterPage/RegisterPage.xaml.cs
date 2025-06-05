using MarketingAppHJ.Cliente.ViewModels.RegisterPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Login.RegisterPage;

/// <summary>
/// Representa la página de registro en la aplicación.
/// </summary>
public partial class RegisterPage : ContentPage
{
    readonly RegisterPageViewModel _vm;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="RegisterPage"/>.
    /// </summary>
    /// <param name="vm">El ViewModel asociado a la página de registro.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el ViewModel es nulo.</exception>
    public RegisterPage(RegisterPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }
}
