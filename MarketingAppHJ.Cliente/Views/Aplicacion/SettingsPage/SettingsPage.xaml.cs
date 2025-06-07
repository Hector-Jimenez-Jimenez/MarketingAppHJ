using MarketingAppHJ.Cliente.ViewModels.SettingsPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.SettingsPage;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsPageViewModel _viewModel;
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker)
        {
            _viewModel.AplicarTema(picker.SelectedIndex);
        }
    }
}
