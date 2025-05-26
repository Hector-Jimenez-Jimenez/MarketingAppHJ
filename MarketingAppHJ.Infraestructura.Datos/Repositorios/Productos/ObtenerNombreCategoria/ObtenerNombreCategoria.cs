using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerNombreCategoria;
using TheMarketingApp.Dominio.Entidades;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerNombreCategoria
{
    /// <summary>
    /// Clase que implementa la interfaz para obtener el nombre de una categoría desde Firebase.
    /// </summary>
    public class ObtenerNombreCategoria : IObtenerNombreCategoria
    {
        private readonly FirebaseClient _firebaseClient;

        /// <summary>
        /// Constructor de la clase ObtenerNombreCategoria.
        /// </summary>
        /// <param name="firebaseClient">Instancia de FirebaseClient utilizada para interactuar con la base de datos.</param>
        /// <exception cref="ArgumentNullException">Se lanza si firebaseClient es nulo.</exception>
        public ObtenerNombreCategoria(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient ?? throw new ArgumentNullException(nameof(firebaseClient));
        }

        /// <summary>
        /// Obtiene los datos de una categoría específica desde Firebase.
        /// </summary>
        /// <param name="IdCategoria">El identificador único de la categoría.</param>
        /// <returns>Un objeto <see cref="CategoriaDto"/> que contiene los datos de la categoría.</returns>
        public async Task<CategoriaDto> ObtenerCategoria(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return new CategoriaDto { Id_Categoria = "", Nombre = "Sin categoría" };

            try
            {
                var dto = await _firebaseClient
                    .Child($"categorias/{categoryId}")
                    .OnceSingleAsync<CategoriaDto>();

                if (dto == null)
                    return new CategoriaDto { Id_Categoria = categoryId, Nombre = "Sin categoría" };

                dto.Id_Categoria = categoryId;
                return dto;
            }
            catch
            {
                // Ruta no existe o error de red
                return new CategoriaDto { Id_Categoria = categoryId, Nombre = "Sin categoría" };
            }
        }
    }
}
