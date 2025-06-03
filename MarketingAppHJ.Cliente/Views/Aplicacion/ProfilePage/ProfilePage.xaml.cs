using MarketingAppHJ.Cliente.ViewModels.ProfilePageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.ProfilePage;

public partial class ProfilePage : ContentPage
{
	private readonly ProfilePageViewModel _viewModel;
    public ProfilePage(ProfilePageViewModel viewModel)
	{
        _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        InitializeComponent();
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Cargar datos del usuario (por ejemplo, desde FirebaseAuth o tu repositorio)
        await _viewModel.CargarPerfilAsync();
    }
}