using MarketingAppHJ.Cliente.ViewModels.ResetPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Login.ResetPage;

/// <summary>
/// Represents the ResetPage view for resetting user credentials.
/// </summary>
public partial class ResetPage : ContentPage
{
    readonly ResetPageViewModel _vm;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResetPage"/> class.
    /// </summary>
    /// <param name="vm">The view model for the ResetPage.</param>
    /// <exception cref="ArgumentNullException">Thrown when the view model is null.</exception>
    public ResetPage(ResetPageViewModel vm)
    {
        InitializeComponent();
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
        BindingContext = _vm;
    }
}
