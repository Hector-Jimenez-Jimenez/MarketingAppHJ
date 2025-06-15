using MarketingAppHJ.Cliente.ViewModels.AdminPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.AdminPage;

public partial class AdminPage : ContentPage
{
    public AdminPage(AdminPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}