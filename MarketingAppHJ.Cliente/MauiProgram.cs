using System.Globalization;
using Firebase.Database;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
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
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.Registro;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Login.ResetConstraseña;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerNombreCategoria;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerProductosPorId;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.Productos.ObtenerTodosProductos;
using MarketingAppHJ.Cliente.ViewModels.CarritoPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.CheckOutPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.LoginPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.MainPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.RegisterPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.ResetPageViewModel;
using MarketingAppHJ.Cliente.Views.Aplicacion;
using MarketingAppHJ.Cliente.Views.Aplicacion.CarritoPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.CheckOutPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.MainPage;
using MarketingAppHJ.Cliente.Views.Login.LoginPage;
using MarketingAppHJ.Cliente.Views.Login.RegisterPage;
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
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.Registro;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Login.ResetearContrasena;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerNombreCategoria;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerProductoPorId;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.Productos.ObtenerProductos;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.Authentication;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;
using Microsoft.Extensions.Logging;
using TheMarketingApp.Infraestructura.Negocio.Servicios;

namespace MarketingAppHJ.Cliente
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddSingleton(sp =>
                new FirebaseClient(
                    "https://themarketingapp-15895-default-rtdb.firebaseio.com/")
                );
            builder.Services.AddInfraestructuraBusiness();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddUseCases();
            builder.Services.AddViewModels();  
            builder.Services.AddViews();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("es-ES");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("es-ES");
            return builder.Build();
        }
        public static void AddServices(this IServiceCollection services)
        {
            // Firebase auth and realtime database clients
            services.AddSingleton<IFirebaseAuthentication, FirebaseAuthentication>();
            services.AddSingleton<IFirebaseRealtimeDatabase, FirebaseRealTimeDatabase>();
        }

        /// <summary>
        /// Adds use case services to the service collection.
        /// </summary>
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IObtenerProductos, ObtenerProductosCatalogo>();
            services.AddScoped<IObtenerProductoPorId, ObtenerProductoPorId>();
            services.AddScoped<IAgregarProductoAlCarrito, AgregarProdcutoAlCarrito>();
            services.AddScoped<IObtenerCarrito, ObtenerProductosCarrito>();
            services.AddScoped<IObtenerNombreCategoria, ObtenerNombreCategoria>();
            services.AddScoped<IBorrarProductoCarrito,  BorrarProductoCarrito>();
            services.AddScoped<IBorrarProductosCarrito, BorrarProductosCarrito>();
            services.AddScoped<IObservarCambiosCarrito, ObservarCambiosCarrito>();
            services.AddScoped<IModificarCantidadCarrito, ModificarCantidadCarrito>();
            services.AddScoped<ICrearPedido, CrearPedido>();
            services.AddScoped<IGuardarPedido, GuardarPedido>();
            services.AddScoped<IResetContrasena, ResetearContrasena>();
            services.AddScoped<ILogOut, LogOut>();
            services.AddScoped<IIniciarSesion, IniciarSesion>();
            services.AddScoped<IRegistro, Registro>();
        }

        /// <summary>
        /// Adds view model services to the service collection.
        /// </summary>
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<DetallesProductoPageViewModel>();
            services.AddTransient<CarritoPageViewModel>();
            services.AddTransient<CheckOutPageViewModel>();
            services.AddTransient<RegisterPageViewModel>();
            services.AddTransient<LoginPageViewModel>();
            services.AddTransient<ResetPageViewModel>();
        }

        /// <summary>
        /// Adds view services to the service collection.
        /// </summary>
        public static void AddViews(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<DetailsPage>();
            services.AddSingleton<CarritoPage>();
            services.AddSingleton<CheckOutPage>();
            services.AddSingleton<RegisterPage>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<RegisterPage>();
        }
    }
}
