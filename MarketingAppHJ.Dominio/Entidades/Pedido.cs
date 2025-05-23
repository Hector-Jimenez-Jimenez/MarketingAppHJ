using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingApp.Dominio.Entidades
{
    /// <summary>
    /// Representa un pedido realizado por un usuario.
    /// </summary>
    public class Pedido : IEquatable<Pedido>
    {
        /// <summary>
        /// Identificador único del pedido.
        /// </summary>
        public string Id_Pedido { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del usuario que realizó el pedido.
        /// </summary>
        public string UsuarioId { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en la que se realizó el pedido.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Total del pedido.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Estado actual del pedido.
        /// </summary>
        public string Estado { get; set; } = string.Empty;

        /// <summary>
        /// Determina si el pedido actual es igual a otro pedido.
        /// </summary>
        /// <param name="other">El pedido a comparar.</param>
        /// <returns>True si los pedidos son iguales; de lo contrario, false.</returns>
        public bool Equals(Pedido? other)
        {
            if (other is null)
                return false;

            return Id_Pedido == other.Id_Pedido &&
                   UsuarioId == other.UsuarioId &&
                   Fecha == other.Fecha &&
                   Total == other.Total &&
                   Estado == other.Estado;
        }

        /// <summary>
        /// Sobrescribe el método Equals para comparar objetos.
        /// </summary>
        /// <param name="obj">El objeto a comparar.</param>
        /// <returns>True si los objetos son iguales; de lo contrario, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Pedido);
        }

        /// <summary>
        /// Genera un código hash para el pedido.
        /// </summary>
        /// <returns>El código hash del pedido.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Pedido, UsuarioId, Fecha, Total, Estado);
        }
    }
}
