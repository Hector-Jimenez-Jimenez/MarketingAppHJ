namespace MarketingAppHJ.Infraestructura.Datos;

using MarketingAppHJ.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Clase para crear la BD y mapear las entidades a tablas utilizando Entity Framework Core.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Inicializa una nueva instancia de AppDbContext con las opciones especificadas.
    /// </summary>
    /// <param name="options"> Opciones de Inicialización </param>
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// Cracion de la Tabla Usuarios
    /// </summary>
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    /// <summary>
    /// Creacion de la Tabla Productos
    /// </summary>
    public DbSet<Producto> Productos => Set<Producto>();
    /// <summary>
    /// Creacion de la Tabla Categorias
    /// </summary>
    public DbSet<Categoria> Categorias => Set<Categoria>();

    /// <summary>
    /// Creacion de la Tabla Pedidos
    /// </summary>
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    /// <summary>
    /// Creacion de la Tabla DetallesPedidos
    /// </summary>
    public DbSet<DetallePedido> DetallesPedidos => Set<DetallePedido>();
    /// <summary>
    /// Creacion de la Tabla CarritoItems
    /// </summary>
    public DbSet<ItemCarrito> CarritoItems => Set<ItemCarrito>();

    /// <summary>
    /// Configura el modelo de datos para el contexto derivado utilizando el generador de modelos proporcionado.
    /// </summary>
    /// <remarks>Llame siempre a la implementación base para asegurar que la configuración predeterminada de
    /// Entity Framework Core se aplique correctamente.</remarks>
    /// <param name="modelBuilder">El generador de modelos utilizado para configurar las entidades, relaciones y restricciones del modelo de datos.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuraciones adicionales si fueran necesarias
        base.OnModelCreating(modelBuilder);
    }
}