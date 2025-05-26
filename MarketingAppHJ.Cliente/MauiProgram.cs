using Firebase.Database;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.AgregarProductoAlCarrito;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerProductosPorId;
using MarketingAppHJ.Aplicacion.Interfaces.UseCases.ObtenerTodosProductos;
using MarketingAppHJ.Cliente.ViewModels.DetallesProductoPageViewModel;
using MarketingAppHJ.Cliente.ViewModels.MainPageViewModel;
using MarketingAppHJ.Cliente.Views.Aplicacion.DetailsPage;
using MarketingAppHJ.Cliente.Views.Aplicacion.MainPage;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.AgregarProductoAlCarrito;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.ObtenerProductoPorId;
using MarketingAppHJ.Infraestructura.Datos.Repositorios.ObtenerProductos;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.Authentication;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;
using Microsoft.Extensions.Logging;

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
            // services.AddScoped<IObtenerCarritoUsuario, ObtenerCarritoUsuarioUseCase>();
            // services.AddScoped<IRealizarPedido, RealizarPedidoUseCase>();
            // services.AddScoped<IRegistrarUsuario, RegistrarUsuarioUseCase>();
        }

        /// <summary>
        /// Adds view model services to the service collection.
        /// </summary>
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<DetallesProductoPageViewModel>();
            // services.AddTransient<CarritoViewModel>();
            // services.AddTransient<LoginViewModel>();
        }

        /// <summary>
        /// Adds view services to the service collection.
        /// </summary>
        public static void AddViews(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddSingleton<DetailsPage>();
            // services.AddSingleton<CarritoPage>();
            // services.AddSingleton<LoginPage>();
        }
    }
}
