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
}