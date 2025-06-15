using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Cloudinary;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ActualizarProducto;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.AgregarProducto;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.EliminarProducto;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ObtenerUsuarios;

namespace MarketingAppHJ.Cliente.ViewModels.AdminPageViewModel
{
    public partial class AdminPageViewModel : ObservableObject
    {
        private readonly IActualizarProducto _actualizarProducto;
        private readonly IEliminarProducto _eliminarProducto;
        private readonly IObtenerProductos _obtenerProductos;
        private readonly IAgregarProducto _agregarProducto;
        private readonly ICloudinaryService _cloudinary;
        private readonly IObtenerUsuarios _obtenerUsuarios;

        [ObservableProperty]
        private ObservableCollection<ProductoDto> productos;
        [ObservableProperty]
        private ObservableCollection<UsuarioDto> usuarios;

        [ObservableProperty]
        private ProductoDto nuevoProducto = new();
        [ObservableProperty]
        private ImageSource? imagenSeleccionada;

        public AdminPageViewModel(
            IActualizarProducto actualizarProducto,
            IEliminarProducto eliminarProducto,
            IObtenerProductos obtenerProductos,
            ICloudinaryService cloudinary,
            IAgregarProducto agregarProducto)
        {
            _actualizarProducto = actualizarProducto;
            _eliminarProducto = eliminarProducto;
            _obtenerProductos = obtenerProductos;
            _agregarProducto = agregarProducto;
            _cloudinary = cloudinary;

            CargarDatosAsync();
        }
        private async Task CargarDatosAsync()
        {
            var lista = await _obtenerProductos.ObtenerProductosAsync();
            Productos = new ObservableCollection<ProductoDto>(lista);

            var usuarios = await _obtenerUsuarios.ObtenerUsuariosAsync();
            Usuarios = new ObservableCollection<UsuarioDto>(usuarios);
        }
        [RelayCommand]
        private async Task GuardarCambios(ProductoDto producto)
        {
            await _actualizarProducto.ActualizarProductoAsync(producto);
        }

        [RelayCommand]
        private async Task Eliminar(ProductoDto producto)
        {
            await _eliminarProducto.EliminarProductoAsync(producto.Id);
            Productos.Remove(producto);
        }

        [RelayCommand]
        private async Task Agregar()
        {
            await _agregarProducto.AgregarProductoAsync(NuevoProducto);
            Productos.Add(NuevoProducto);
            NuevoProducto = new ProductoDto();
        }
        [RelayCommand]
        private async Task SeleccionarImagen()
        {
            try
            {
                var resultado = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Selecciona una imagen"
                });

                if (resultado != null)
                {
                    var nombre = resultado.FileName;

                    using var originalStream = await resultado.OpenReadAsync();

                    var memoryStream = new MemoryStream();
                    await originalStream.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    ImagenSeleccionada = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));

                    memoryStream.Position = 0;
                    var url = await _cloudinary.SubirImagenAsync(memoryStream, nombre);

                    NuevoProducto.ImagenUrl = url;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo subir la imagen: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async void Volver()
        {
            await Shell.Current.GoToAsync("ajustes");
        }
    }
}
