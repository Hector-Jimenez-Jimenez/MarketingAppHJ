
using MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;

/// <summary>
/// Represents the details page in the application.
/// </summary>
/// 
[QueryProperty(nameof(ProductoId), "productoId")]
public partial class DetailsPage : ContentPage
{
    /// <summary>
    /// Gets or sets the product ID for the details page.
    /// When set, it triggers the loading of the product details in the ViewModel.
    /// </summary>
    public string ProductoId
    {
        set
        {
            if (BindingContext is DetallesProductoPageViewModel vm)
                vm.LoadProductById(value);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DetailsPage"/> class.
    /// </summary>
    /// <param name="viewModel">The ViewModel associated with the details page.</param>
    public DetailsPage(DetallesProductoPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

