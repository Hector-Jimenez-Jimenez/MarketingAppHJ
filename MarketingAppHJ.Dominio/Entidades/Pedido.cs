using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketingAppHJ.Dominio.Entidades
{
    /// <summary>
    /// Representa un pedido realizado por un usuario.
    /// </summary>
    public class Pedido : IEquatable<Pedido>
    {
        /// <summary>
        /// Identificador único del pedido.
        /// </summary>
        [Key]
        public string Id_Pedido { get; set; } = string.Empty;

        /// <summary>
        /// Identificador del usuario que realizó el pedido.
        /// </summary>
        [ForeignKey("Id_Usuario")]
        public string Id_Usuario { get; set; } = string.Empty;

        /// <summary>
        /// Fecha en la que se realizó el pedido.
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Total del pedido.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
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
                   Id_Usuario == other.Id_Usuario &&
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
            return HashCode.Combine(Id_Pedido, Id_Usuario, Fecha, Total, Estado);
        }
        /// <summary>
        /// Colección de detalles del pedido asociados a este pedido.
        /// </summary>
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
