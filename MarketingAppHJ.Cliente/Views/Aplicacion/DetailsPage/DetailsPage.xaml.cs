
using MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;

/// <summary>
/// Represents the details page in the application.
/// </summary>
/// 
[QueryProperty(nameof(ProductoId), "productoId")]
public partial class DetailsPage : ContentPage
{
    public string ProductoId
    {
        set
        {
            if (BindingContext is DetallesProductoPageViewModel vm)
                vm.LoadProductById(value);
        }
    }

    public DetailsPage(DetallesProductoPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

