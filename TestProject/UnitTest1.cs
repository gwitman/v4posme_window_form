using System.Globalization;
using v4posme_library.Libraries.CustomHelper;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void General()
        {
            var numero = "0,2777";
            var result = WebToolsHelper.ConvertToNumber<decimal>(numero);
            Console.WriteLine(result);
            // Obtener la configuración regional actual
            var culturaActual = CultureInfo.CurrentCulture;

            Console.WriteLine("Configuración regional actual:");
            Console.WriteLine($"Nombre: {culturaActual.Name}");
            Console.WriteLine($"Idioma: {culturaActual.DisplayName}");
            Console.WriteLine($"Código de país: {culturaActual.TwoLetterISOLanguageName}");
            Console.WriteLine();

            // Obtener el formato de número para la configuración regional actual
            var formatoNumero = culturaActual.NumberFormat;

            Console.WriteLine("Formato de número para la configuración regional actual:");
            Console.WriteLine($"Separador de miles: {formatoNumero.NumberGroupSeparator}");
            Console.WriteLine($"Separador decimal: {formatoNumero.NumberDecimalSeparator}");
        }
    }
}