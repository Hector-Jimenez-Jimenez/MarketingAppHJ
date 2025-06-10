using MarketingAppHJ.Aplicacion.Interfaces.Cloudinary;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.Authentication;
using MarketingAppHJ.Aplicacion.Interfaces.Firebase.RealTimeDatabase;
using MarketingAppHJ.Aplicacion.Interfaces.Idiomas;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.CloudinaryService;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.Authentication;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.Firebase.RealtimeDatabase;
using MarketingAppHJ.Infraestructura.Negocio.Servicios.LanguageService;
using Microsoft.Extensions.DependencyInjection;

namespace TheMarketingApp.Infraestructura.Negocio.Servicios
{
    /// <summary>
    /// Proporciona métodos de extensión para registrar servicios de infraestructura de negocio en el contenedor de dependencias.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Agrega los servicios de infraestructura de negocio al contenedor de dependencias.
        /// </summary>
        /// <param name="services">El contenedor de dependencias <see cref="IServiceCollection"/>.</param>
        public static void AddInfraestructuraBusiness(this IServiceCollection services)
        {
            services.AddScoped<IFirebaseAuthentication, FirebaseAuthentication>();
            services.AddScoped<IFirebaseRealtimeDatabase, FirebaseRealTimeDatabase>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            services.AddSingleton<IIdiomaService, LanguageService>();
        }
    }
}
