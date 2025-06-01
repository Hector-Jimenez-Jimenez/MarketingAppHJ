using MarketingAppHJ.Cliente.ViewModels.LoginPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Login.LoginPage;

public partial class LoginPage : ContentPage
{
    readonly LoginPageViewModel _vm;
    public LoginPage(LoginPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Limpiar campos cuando se muestra
        _vm.Email = string.Empty;
        _vm.Password = string.Empty;
    }
}