using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheMarketingApp.Dominio.Entidades
{
    /// <summary>
    /// Representa un usuario en el sistema.
    /// </summary>
    public class Usuario : IEquatable<Usuario>
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public string Id_Usuario { get; set; } = string.Empty;

        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// URL del avatar del usuario.
        /// </summary>
        public string AvatarUrl { get; set; } = string.Empty;

        /// <summary>
        /// Determina si el objeto actual es igual a otro objeto del mismo tipo.
        /// </summary>
        /// <param name="other">El objeto a comparar con este objeto.</param>
        /// <returns>true si los objetos son iguales; de lo contrario, false.</returns>
        public bool Equals(Usuario? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Id_Usuario == other.Id_Usuario &&
                   Nombre == other.Nombre &&
                   Email == other.Email &&
                   AvatarUrl == other.AvatarUrl;
        }

        /// <summary>
        /// Determina si el objeto actual es igual a otro objeto.
        /// </summary>
        /// <param name="obj">El objeto a comparar con este objeto.</param>
        /// <returns>true si los objetos son iguales; de lo contrario, false.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Usuario);
        }

        /// <summary>
        /// Devuelve un código hash para el objeto actual.
        /// </summary>
        /// <returns>Código hash del objeto.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Id_Usuario, Nombre, Email, AvatarUrl);
        }
    }
}
