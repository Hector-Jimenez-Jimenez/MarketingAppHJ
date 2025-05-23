using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingApp.Dominio.Entidades
{
    /// <summary>
    /// Representa una categoría en el sistema.
    /// </summary>
    public class Categoria : IEquatable<Categoria>
    {
        /// <summary>
        /// Identificador único de la categoría.
        /// </summary>
        public string Id_Categoria { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de la categoría.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la categoría.
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Determina si la instancia actual es igual a otra instancia de <see cref="Categoria"/>.
        /// </summary>
        /// <param name="other">La otra instancia de <see cref="Categoria"/> a comparar.</param>
        /// <returns><c>true</c> si las instancias son iguales; de lo contrario, <c>false</c>.</returns>
        public bool Equals(Categoria? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id_Categoria == other.Id_Categoria &&
                   Nombre == other.Nombre &&
                   Descripcion == other.Descripcion;
        }

        /// <summary>
        /// Determina si la instancia actual es igual a otro objeto.
        /// </summary>
        /// <param name="obj">El objeto a comparar.</param>
        /// <returns><c>true</c> si el objeto es igual a la instancia actual; de lo contrario, <c>false</c>.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Categoria);
        }

        /// <summary>
        /// Devuelve un código hash para la instancia actual.
        /// </summary>
        /// <returns>Un código hash para la instancia actual.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Categoria, Nombre, Descripcion);
        }
    }
}
