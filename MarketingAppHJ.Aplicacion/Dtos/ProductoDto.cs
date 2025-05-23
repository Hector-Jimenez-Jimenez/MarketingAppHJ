namespace MarketingAppHJ.Aplicacion.Dtos
{
    /// <summary>
    /// Representa un producto con sus propiedades principales.
    /// </summary>
    public class ProductoDto
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Precio del producto.
        /// </summary>
        public decimal Precio { get; set; }

        /// <summary>
        /// URL de la imagen del producto.
        /// </summary>
        public string ImagenUrl { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad de producto disponible en stock.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Identificador de la categoría a la que pertenece el producto.
        /// </summary>
        public string CategoriaId { get; set; } = string.Empty;
    }
}
