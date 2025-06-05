using MarketingAppHJ.Cliente.ViewModels.ProfilePageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.ProfilePage;

/// <summary>
/// Representa la página de perfil del usuario.
/// </summary>
public partial class ProfilePage : ContentPage
{
    private readonly ProfilePageViewModel _viewModel;

    /// <summary>
    /// Inicializa una nueva instancia de la clase <see cref="ProfilePage"/>.
    /// </summary>
    /// <param name="viewModel">El ViewModel asociado a la página de perfil.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el parámetro <paramref name="viewModel"/> es nulo.</exception>
    public ProfilePage(ProfilePageViewModel viewModel)
    {
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
        BindingContext = _viewModel;
    }

    /// <summary>
    /// Método que se ejecuta cuando la página aparece en pantalla.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Cargar datos del usuario (por ejemplo, desde FirebaseAuth o tu repositorio)
        await _viewModel.CargarPerfilAsync();
    }
}
