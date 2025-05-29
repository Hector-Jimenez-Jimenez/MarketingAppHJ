
using MarketingAppHJ.Aplicacion.Dtos;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerNombreCategoria;

namespace MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerNombreCategoria
{
    /// <summary>
    /// Clase que implementa la interfaz para obtener el nombre de una categoría desde Firebase.
    /// </summary>
    public class ObtenerNombreCategoria : IObtenerNombreCategoria
    {
        private readonly IFirebaseRealtimeDatabase _firebaseClient;

        /// <summary>
        /// Constructor de la clase ObtenerNombreCategoria.
        /// </summary>
        /// <param name="firebaseClient">Instancia de FirebaseClient utilizada para interactuar con la base de datos.</param>
        /// <exception cref="ArgumentNullException">Se lanza si firebaseClient es nulo.</exception>
        public ObtenerNombreCategoria(IFirebaseRealtimeDatabase firebaseClient)
        {
            _firebaseClient = firebaseClient ?? throw new ArgumentNullException(nameof(firebaseClient));
        }

        /// <summary>
        /// Obtiene los datos de una categoría específica desde Firebase.
        /// </summary>
        /// <param name="categoryId">El identificador único de la categoría.</param>
        /// <returns>Un objeto <see cref="CategoriaDto"/> que contiene los datos de la categoría.</returns>
        public async Task<CategoriaDto> ObtenerCategoria(string categoryId)
        {
            if (string.IsNullOrWhiteSpace(categoryId))
                return new CategoriaDto { Id_Categoria = "", Nombre = "Sin categoría" };

            try
            {
                var dto = await _firebaseClient.Instance
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
