using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerTodosProductos;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.ObtenerProductos;

namespace MarketingAppHJ.Cliente.ViewModels.MainPageViewModel
{
    /// <summary>
    /// ViewModel para gestionar el catálogo de productos.
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IObtenerProductos _usecase;
        public ObservableCollection<ProductoDto> Productos { get; } = new();
        [ObservableProperty] bool isBusy;

        public MainPageViewModel(IObtenerProductos usecase) => _usecase = usecase;
        public MainPageViewModel() { }
        [RelayCommand]
        public async Task CargarProductosAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            Productos.Clear();
            var list = await _usecase.ObtenerProductosAsync();
            foreach (var p in list) Productos.Add(p);
            IsBusy = false;
        }
    }
}
