using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v4posme_library.Libraries.CustomHelper
{
    public class WebToolsHelper
    {
        public DateOnly HelperGetDate()
        {
            var now = DateTime.Now;
            //var appHourDiferencePhp = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_PHP"];
            // Convertir la cadena a un TimeSpan
            //var intervaloTiempo = TimeSpan.Parse(appHourDiferencePhp!);
            return DateOnly.Parse(now.ToString("yyyy-MM-dd"));
        }

        public DateOnly HelperGetDateMoreOneMonth()
        {
            //DateOnly.FromDateTime(DateTime.Now).AddMonths(1);
            // Obtener la fecha actual
            var fechaActual = DateTime.Today;

            // Agregar un mes a la fecha actual
            var nuevaFecha = fechaActual.AddMonths(1);
            return DateOnly.FromDateTime(nuevaFecha);
        }

        public DateTime HelperGetDateTime()
        {
            return DateTime.Now;
            /*var appHourDiferencePhp = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_PHP"];
            var intervaloTiempo = TimeSpan.Parse(appHourDiferencePhp!);
            return DateTime.Parse(fechaActual.ToString("yyyy-MM-dd HH:mm:ss"));*/
        }

        public DateOnly HelperPrimerDiaDelMes()
        {
            var today = DateTime.Today;
            var firstDayOfMonth = today.AddDays(1 - today.Day);
            return DateOnly.FromDateTime(firstDayOfMonth);
        }

        public DateOnly HelperPrimerDiaDelYear()
        {
            var today = DateTime.Today;
            return new DateOnly(today.Year, 1, 1);
        }

        public DateTime HelperUltimoDiaDelMes()
        {
            var today = DateTime.Today;
            var firstDayOfNextMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1);
            var lastDayOfMonth = firstDayOfNextMonth.AddDays(-1);
            return new DateTime(lastDayOfMonth.Year, lastDayOfMonth.Month, lastDayOfMonth.Day, 23, 59, 59);
        }

        public DateTime HelperUltimoDiaDelYear()
        {
            var today = HelperPrimerDiaDelYear();
            var add = new DateTime(today.Year, today.Month, 1).AddYears(1);
            var last = add.AddDays(-1);
            return new DateTime(last.Year, last.Month, last.Day, 23, 59, 59);
        }

        public string HelperStrPadString(string input, int length, char paddingChar)
        {
            if (input.Length >= length)
            {
                return input;
            }

            return input.PadLeft(length, paddingChar);
        }

        public string HelperStringToNumber(string input)
        {
            return input.Replace(",", "");
        }

        public string HelperGetSr(string sexo)
        {
            return sexo == "FEMENINO" ? "Sra." : "Sr.";
        }

        public string HelperGetEl(string sexo)
        {
            return sexo == "FEMENINO" ? "la" : "el";
        }

        public int HelperGetFechaNacimiento(string fecha)
        {
            var fechaNacimiento = DateOnly.Parse(fecha, CultureInfo.InvariantCulture);
            var fechaActual = DateTime.Now;
            var diff = fechaActual.Year-fechaNacimiento.Year;
            return diff;
        }
        public static void EmptyDir(string dir)
        {
            if (!Directory.Exists(dir)) return;
            var files = Directory.GetFiles(dir);
            foreach (var file in files)
            {
                File.Delete(file);
            }

            var subDirs = Directory.GetDirectories(dir);
            foreach (var subDir in subDirs)
            {
                EmptyDir(subDir);
                Directory.Delete(subDir);
            }
        }
        public static void DeleteDir(string dir)
        {
            if (!Directory.Exists(dir)) return;
            foreach (var file in Directory.GetFiles(dir))
            {
                File.Delete(file);
            }

            foreach (var subDir in Directory.GetDirectories(dir))
            {
                DeleteDir(subDir);
            }
            
            Directory.Delete(dir);
        }
    }
}