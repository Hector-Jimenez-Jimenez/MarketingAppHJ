using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerProductosPorId;

namespace MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel
{
    /// <summary>
    /// ViewModel para la página de detalles del producto.
    /// </summary>
    public partial class DetallesProductoPageViewModel : ObservableObject
    {
        private readonly IObtenerProductoPorId _usecase;
        [ObservableProperty] private ProductoDto producto;
        [ObservableProperty] private bool isBusy;
        public DetallesProductoPageViewModel(IObtenerProductoPorId usecase) => _usecase = usecase;
        public DetallesProductoPageViewModel() { }
        public async Task LoadProductAsync(string id)
        {
            if (IsBusy) return;
            IsBusy = true;
            Producto = await _usecase.ObtenerProductoPorIdAsync(id);
            IsBusy = false;
        }

        [RelayCommand]
        public Task AddToCartAsync() => Task.CompletedTask;
    }
}
