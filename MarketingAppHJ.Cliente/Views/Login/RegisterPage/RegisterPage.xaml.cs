using MarketingAppHJ.Cliente.ViewModels.RegisterPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Login.RegisterPage;

public partial class RegisterPage : ContentPage
{
    readonly RegisterPageViewModel _vm;

    public RegisterPage(RegisterPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }
}