using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketingAppHJ.Aplicacion.Interfaces.Idiomas;

namespace MarketingAppHJ.Infraestructura.Negocio.Servicios.LanguageService
{
    public class LanguageService : IIdiomaService
    {
        public event EventHandler IdiomaCambiado;
        public string idiomaActual => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
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
