using MarketingAppHJ.Cliente.Helper;
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
            _viewModel.AplicarTema(picker.SelectedIndex);
    }

    private void OnIdiomaChanged(object sender, EventArgs e)
    {
        if (sender is Picker picker)
            _viewModel.AplicarIdiomaDesdeVistaAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Suponiendo que el BindingContext es tu ViewModel
        if (BindingContext is SettingsPageViewModel vm)
        {
            var idiomaActual = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            vm.IdiomaSeleccionadoIndex = idiomaActual == "en" ? 1 : 0;
            TranslateExtension.RefreshTranslations();
        }
    }

}
