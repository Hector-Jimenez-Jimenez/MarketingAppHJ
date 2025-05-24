using CommunityToolkit.Mvvm.ComponentModel;

namespace MarketingAppHJ.Aplicacion.Dtos
{
    /// <summary>
    /// Representa un producto con sus propiedades principales.
    /// </summary>
    public partial class ProductoDto : ObservableObject
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        [ObservableProperty]
        private string id = string.Empty;

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        [ObservableProperty]
        private string nombre = string.Empty;

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        [ObservableProperty]
        private string descripcion = string.Empty;

        /// <summary>
        /// Precio del producto.
        /// </summary>
        [ObservableProperty]
        private decimal precio;

        /// <summary>
        /// URL de la imagen del producto.
        /// </summary>
        [ObservableProperty]
        private string imagenUrl = string.Empty;

        /// <summary>
        /// Cantidad de producto disponible en stock.
        /// </summary>
        [ObservableProperty]
        private int stock;

        /// <summary>
        /// Identificador de la categoría a la que pertenece el producto.
        /// </summary>
        [ObservableProperty]
        private string categoriaId = string.Empty;
    }
}
