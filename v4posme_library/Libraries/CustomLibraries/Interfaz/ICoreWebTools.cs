using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebTools
{
    /// <summary>
    /// Esta función recibe un objeto message y devuelve una cadena formateada.
    /// Si message es un array, concatena todos los elementos del array en una sola cadena.
    /// Si no es un array, elimina los saltos de línea de la cadena.
    /// </summary>
    /// <param name="message">Array|string</param>
    /// <returns>string</returns>
    string FormatMessageError(object message);
    
    /// <summary>
    /// Esta función toma una cadena filter, la formatea y la convierte en un diccionario de claves y valores.
    /// Primero, reemplaza algunos caracteres en la cadena. Luego, convierte la cadena en un objeto JSON
    /// y lo recorre para construir el diccionario final.
    /// </summary>
    /// <param name="filter">Parametro a filtrar</param>
    /// <returns>Clave-Valor</returns>
    Dictionary<string, object> FormatParameter(string filter);

    void Log(string logMessage);

    void SendEmail(string subject, string body);

    TbComponent? GetComponentIdByComponentName(string componentName);
}