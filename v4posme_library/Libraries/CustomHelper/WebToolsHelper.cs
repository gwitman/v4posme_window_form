using System.Diagnostics;
using System.Globalization;


namespace v4posme_library.Libraries.CustomHelper
{
    public class WebToolsHelper
    {
        public static long ToUnixTimestamp(DateTime dateTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixTime = dateTime - epoch;
            return (long)unixTime.TotalSeconds;
        }

        public static long ToUnixTimestamp(DateOnly dateTime)
        {
            var epoch = new DateOnly(1970, 1, 1);
            var unixTime = dateTime.DayNumber - epoch.DayNumber;
            return unixTime;
        }

        public string helper_RequestGetValueObjet(dynamic data, string fieldData, string defaultValue)
        {
            //Validar data
            if (data is null)
                return defaultValue;

            //Validar has
            var dictionary = (IDictionary<string, object>)data;
            if (!dictionary.ContainsKey(fieldData))
                return defaultValue;


            string value = dictionary[fieldData].ToString();
            return value;
        }

        public DateTime HelperGetDate()
        {
            var now = DateTime.Now;
            var appHourDiferencePhp = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_PHP"];
            return now.AddMinutes(ValidarCampo(appHourDiferencePhp!));
            //return DateOnly.Parse(now.ToString("yyyy-MM-dd 00:00:00"));
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
            var appHourDiferencePhp = VariablesGlobales.ConfigurationBuilder["APP_HOUR_DIFERENCE_PHP"];
            return DateTime.Now.AddMinutes(ValidarCampo(appHourDiferencePhp!));
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

        public static decimal HelperStringToNumber(string input)
        {
            return Convert.ToDecimal(input.Replace(",", ""));
        }

        public static T? ConvertToNumber<T>(string input)
        {
            var result = default(T);
            if (string.IsNullOrEmpty(input))
            {
                return result;
            }
            try
            {
                result = (T)Convert.ChangeType(input!, typeof(T));
            }
            catch (FormatException)
            {
                Debug.WriteLine($@"Error: La cadena {input} no se puede convertir a un número del tipo {typeof(T).Name}");
            }
            catch (InvalidCastException)
            {
                Debug.WriteLine($@"Error: La cadena {input} no se puede convertir a un número del tipo {typeof(T).Name}");
            }

            return result;
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
            var diff = fechaActual.Year - fechaNacimiento.Year;
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

        private static int ValidarCampo(string input)
        {
            var isNegative = input.StartsWith("-");
            var isPositive = input.StartsWith("+");

            if (isNegative)
            {
                input = input.Substring(1);
            }

            if (isPositive)
            {
                input = input.Substring(1);
            }

            if (int.TryParse(input, out var result))
            {
                if (isNegative)
                {
                    result *= -1;
                }
            }
            else
            {
                return 0;
            }

            return result;
        }
    }
}