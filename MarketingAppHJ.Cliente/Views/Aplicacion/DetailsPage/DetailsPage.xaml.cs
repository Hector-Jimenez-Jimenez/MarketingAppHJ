
using MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel;

namespace MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;

/// <summary>
/// Represents the details page in the application.
/// </summary>
/// 
public partial class DetailsPage : ContentPage,IQueryAttributable
{
    private readonly DetallesProductoPageViewModel _vm;

    public DetailsPage()
    {
        InitializeComponent();
        _vm = IPlatformApplication
              .Current
              .Services
              .GetRequiredService<DetallesProductoPageViewModel>();
        BindingContext = _vm;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("productoId", out var idObj)
            && idObj is string id)
        {
            Console.WriteLine($"[DETAIL PAGE] productoId = {id}");
            _ = _vm.LoadProductAsync(id);
        }
    }
}
