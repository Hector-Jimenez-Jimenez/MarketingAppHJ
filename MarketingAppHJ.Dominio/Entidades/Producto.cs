using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingApp.Dominio.Entidades
{
    /// <summary>
    /// Representa un producto en el sistema.
    /// </summary>
    public class Producto : IEquatable<Producto>
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public string Id_Producto { get; set; } = string.Empty;

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

        /// <summary>
        /// Determina si el objeto actual es igual a otro objeto del mismo tipo.
        /// </summary>
        /// <param name="other">El objeto Producto a comparar.</param>
        /// <returns>true si los objetos son iguales; de lo contrario, false.</returns>
        public bool Equals(Producto? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Id_Producto == other.Id_Producto &&
                   Nombre == other.Nombre &&
                   Descripcion == other.Descripcion &&
                   Precio == other.Precio &&
                   ImagenUrl == other.ImagenUrl &&
                   Stock == other.Stock &&
                   CategoriaId == other.CategoriaId;
        }

        /// <summary>
        /// Determina si el objeto actual es igual a otro objeto.
        /// </summary>
        /// <param name="obj">El objeto a comparar.</param>
        /// <returns>true si los objetos son iguales; de lo contrario, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Producto);
        }

        /// <summary>
        /// Devuelve un código hash para el objeto actual.
        /// </summary>
        /// <returns>Código hash del objeto.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Producto, Nombre, Descripcion, Precio, ImagenUrl, Stock, CategoriaId);
        }
    }
}
