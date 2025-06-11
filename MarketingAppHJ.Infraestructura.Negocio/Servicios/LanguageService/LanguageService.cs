using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Interfaces.Idiomas;

namespace MarketingAppHJ.Infraestructura.Negocio.Servicios.LanguageService
{
    /// <summary>  
    /// Servicio para gestionar el idioma de la aplicación.  
    /// </summary>  
    public class LanguageService : IIdiomaService
    {
        /// <summary>  
        /// Evento que se dispara cuando el idioma ha sido cambiado.  
        /// </summary>  
        public event EventHandler IdiomaCambiado;

        /// <summary>  
        /// Obtiene el idioma actual en formato ISO de dos letras.  
        /// </summary>  
        public string idiomaActual => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        /// <summary>  
        /// Cambia el idioma de la aplicación.  
        /// </summary>  
        /// <param name="idioma">Código del idioma en formato ISO.</param>  
        /// <exception cref="ArgumentException">Se lanza si el idioma es nulo o vacío.</exception>  
        public void CambiarIdioma(string idioma)
        {
            if (string.IsNullOrWhiteSpace(idioma))
            {
                throw new ArgumentException("El idioma no puede ser nulo o vacío.", nameof(idioma));
            }
            var cultura = new CultureInfo(idioma);

            CultureInfo.CurrentCulture = cultura;
            CultureInfo.CurrentUICulture = cultura;

            IdiomaCambiado?.Invoke(this, EventArgs.Empty);
        }
    }
}
