using MarketingAppHJ.Cliente.ViewModels.ResetPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Login.ResetPage;

public partial class ResetPage : ContentPage
{
    readonly ResetPageViewModel _vm;

    public ResetPage(ResetPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }
}