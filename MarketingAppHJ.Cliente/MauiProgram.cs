using System.Globalization;
using Firebase.Auth;
using Firebase.Auth.Providers;
using MarketingAppHJ.Aplicacion.Interfaces.Cloudinary;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.AgregarProductoAlCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductoCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.BorrarProductosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ModificarCantidadCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObservarCambiosCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Carrito.ObtenerCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.CrearPedido;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Checkout.GuardarPedido;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.IniciarSesion;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.LogOut;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ObtenerUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.Registro;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObservarCambiosPedidos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidoPorId;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Pedidos.ObtenerPedidos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ActualizarProducto;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.AgregarProducto;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.EliminarProducto;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerNombreCategoria;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerProductosPorId;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.GuardarFotoPerfil;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.IObtenerPerfilUsuario;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Usuarios.ObtenerUsuarios;
using MarketingAppHJ.Cliente.ViewModels.AdminPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.CambioDatosPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.DetallesPedidoPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.LoginPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.MainPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.PedidosPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.ProfilePageViewModel;
using MarketingAppHJ.Cliente.ViewModels.RegisterPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.ResetPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.SettingsPageViewModel;
using MarketingAppHJ.Cliente.Views.Aplicacion.AdminPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CambioDatosPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetallesPedidoPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.MainPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.PedidosPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.ProfilePage;
using MarketingAppHJ.Cliente.Views.Aplicacion.SettingsPage;
using MarketingAppHJ.Cliente.Views.Login.LoginPage;
using MarketingAppHJ.Cliente.Views.Login.RegisterPage;
using MarketingAppHJ.Cliente.Views.Login.ResetPage;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.AgregarProductoAlCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.BorrarProductoCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.BorrarProductosCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.ModificarCantidadCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.ObservarCambiosCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Carrito.ObtenerProductosCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Checkout.CrearPedido;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Checkout.GuardarPedido;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.IniciarSesion;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.LogOut;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.ObtenerUsuario;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.Registro;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.ResetearContrasena;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Pedidos.ObservarCambiosPedidos;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Pedidos.ObtenerPedidoPorId;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Pedidos.ObtenerPedidos;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ActualizarProducto;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.AgregarProducto;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.EliminarProducto;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerNombreCategoria;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerProductoPorId;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerProductos;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.ActualizarUsuario;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.GuardarImagenPerfil;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.ObtenerPerfilUsuario;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Usuarios.ObtenerUsuarios;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.CloudinaryService;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.Authentication;
using Microsoft.Extensions.Logging;
using TheMarketingApp.Infraestructura.Negocio.Servicios;

namespace MarketingAppHJ.Cliente
{
    /// <summary>
    /// Clase estática que configura y crea la aplicación Maui.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Crea y configura la aplicación Maui.
        /// </summary>
        /// <returns>Una instancia de <see cref="MauiApp"/> configurada.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddInfraestructuraBusiness();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddUseCases();
            builder.Services.AddViewModels();
            builder.Services.AddViews();
            
            builder.Services.Configure<CloudinarySettings>(options =>
            {
                options.CloudName = "df4l4kyo7";
                options.ApiKey = "727739372591495";
                options.ApiSecret = "aWv8WSjgg9UZurB7itMyQgq34KU";
            });

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    Microsoft.Maui.Handlers.ImageHandler.Mapper.AppendToMapping("Splash", (handler, view) =>
                    {
                        handler.PlatformView.SetScaleType(Android.Widget.ImageView.ScaleType.CenterCrop);
                    });
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var idiomaGuardado = Preferences.Get("Idioma", "es");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(idiomaGuardado);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(idiomaGuardado);
            return builder.Build();
        }

        /// <summary>
        /// Agrega servicios a la colección de servicios.
        /// </summary>
        /// <param name="services">La colección de servicios.</param>
        public static void AddServices(this IServiceCollection services)
        {
            // Firebase auth and realtime database clients
            services.AddSingleton<IFirebaseAuthentication, FirebaseAuthentication>();
        }

        /// <summary>
        /// Agrega servicios de casos de uso a la colección de servicios.
        /// </summary>
        /// <param name="services">La colección de servicios.</param>
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IObtenerProductos, ObtenerProductosCatalogo>();
            services.AddScoped<IObtenerProductoPorId, ObtenerProductoPorId>();
            services.AddScoped<IAgregarProductoAlCarrito, AgregarProdcutoAlCarrito>();
            services.AddScoped<IObtenerCarrito, ObtenerProductosCarrito>();
            services.AddScoped<IObtenerNombreCategoria, ObtenerNombreCategoria>();
            services.AddScoped<IBorrarProductoCarrito, BorrarProductoCarrito>();
            services.AddScoped<IBorrarProductosCarrito, BorrarProductosCarrito>();
            services.AddScoped<IObservarCambiosCarrito, ObservarCambiosCarrito>();
            services.AddScoped<IModificarCantidadCarrito, ModificarCantidadCarrito>();
            services.AddScoped<ICrearPedido, CrearPedido>();
            services.AddScoped<IGuardarPedido, GuardarPedido>();
            services.AddScoped<IResetContrasena, ResetearContrasena>();
            services.AddScoped<ILogOut, LogOut>();
            services.AddScoped<IIniciarSesion, IniciarSesion>();
            services.AddScoped<IRegistro, Registro>();
            services.AddScoped<IObtenerUsuario, ObtenerUsuario>();
            services.AddScoped<IObtenerPerfilUsuario, ObtenerPerfilUsuario>();
            services.AddScoped<IActualizarUsuario, ActualizarUsuario>();
            services.AddScoped<IObtenerPedidos, ObtenerPedidos>();
            services.AddScoped<IObservarCambiosPedido, ObservarCambiosPedidos>();
            services.AddScoped<IGuardarImagenPefil, GuardadoImagenPerfil>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IObtenerPedidoPorId, ObtenerPedidoPorId>();
            services.AddScoped<IActualizarProducto, ActualizarProducto>();
            services.AddScoped<IEliminarProducto, EliminarProducto>();
            services.AddScoped<IAgregarProducto, AgregarProducto>();
            services.AddScoped<IObtenerUsuarios, ObtenerUsuarios>();
        }

        /// <summary>
        /// Agrega servicios de modelos de vista a la colección de servicios.
        /// </summary>
        /// <param name="services">La colección de servicios.</param>
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<DetallesProductoPageViewModel>();
            services.AddTransient<CarritoPageViewModel>();
            services.AddTransient<CheckOutPageViewModel>();
            services.AddTransient<RegisterPageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<ResetPageViewModel>();
            services.AddTransient<ProfilePageViewModel>();
            services.AddTransient<CambioDatosPageViewModel>();
            services.AddTransient<PedidosPageViewModel>();
            services.AddTransient<SettingsPageViewModel>();
            services.AddTransient<DetallesPedidoPageViewModel>();
            services.AddTransient<AdminPageViewModel>();
        }

        /// <summary>
        /// Agrega servicios de vistas a la colección de servicios.
        /// </summary>
        /// <param name="services">La colección de servicios.</param>
        public static void AddViews(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<DetailsPage>();
            services.AddSingleton<CarritoPage>();
            services.AddSingleton<CheckOutPage>();
            services.AddSingleton<RegisterPage>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<RegisterPage>();
            services.AddSingleton<ResetPage>();
            services.AddSingleton<ProfilePage>();
            services.AddSingleton<CambioDatosPage>();
            services.AddSingleton<PedidosPage>();
            services.AddTransient<SettingsPage>();
            services.AddTransient<DetallesPedidoPage>();
            services.AddTransient<AdminPage>();
        }
    }
}
