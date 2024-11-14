using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v4posme_window.Libraries
{
    public static class CustomException
    {
        public static void LogException(Exception ex)
        {
            string logFilePath = "error_log.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"Fecha y hora: {DateTime.Now}");
                    writer.WriteLine($"Clase: {ex.TargetSite.DeclaringType}");
                    writer.WriteLine($"Método: {ex.TargetSite.Name}");
                    writer.WriteLine($"Número de línea: {GetExceptionLineNumber(ex)}");
                    writer.WriteLine($"Mensaje de error: {ex.Message}");
                    writer.WriteLine($"String: {ex.ToString()}");
                    writer.WriteLine(new string('-', 50));
                    writer.WriteLine();
                }
                
            }
            catch (Exception el)
            {
                MessageBox.Show("Error al intentar escribir en el archivo de registro,  Ejecutar como administrador.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para obtener el número de línea donde se produjo la excepción
        private static int GetExceptionLineNumber(Exception ex)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(ex, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

    }


}
