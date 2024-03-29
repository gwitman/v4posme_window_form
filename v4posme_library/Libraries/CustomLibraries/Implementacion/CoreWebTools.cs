using Newtonsoft.Json.Linq;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebTools : ICoreWebTools
{
    /// <summary>
    /// Esta función recibe un objeto message y devuelve una cadena formateada.
    /// Si message es un array, concatena todos los elementos del array en una sola cadena.
    /// Si no es un array, elimina los saltos de línea de la cadena.
    /// </summary>
    /// <param name="message">Array|string</param>
    /// <returns>string</returns>
    public string FormatMessageError(object message)
    {
        var resultMessage = "";

        resultMessage = message is Array
            ? ((Array)message).Cast<object?>().Aggregate(resultMessage, (current, value) => current + value)
            : ((string)message).Replace("\n", "");

        return resultMessage;
    }

    /// <summary>
    /// Esta función toma una cadena filter, la formatea y la convierte en un diccionario de claves y valores.
    /// Primero, reemplaza algunos caracteres en la cadena. Luego, convierte la cadena en un objeto JSON
    /// y lo recorre para construir el diccionario final.
    /// </summary>
    /// <param name="filter">Parametro a filtrar</param>
    /// <returns>Clave-Valor</returns>
    public Dictionary<string, object> FormatParameter(string filter)
    {
        filter = filter.Replace("|", ":");
        filter = filter.Replace("{}", ",");
        var json = filter.StartsWith("{") && filter.EndsWith("}") ? filter : "{" + filter + "}";
        var jsonObject = JObject.Parse(json);

        var result = new Dictionary<string, object>();
        foreach (var item in jsonObject)
        {
            result["{" + item.Key + "}"] = item.Value!.ToString();
        }

        return result;
    }

    public TbComponent? GetComponentIdByComponentName(string componentName)
    {
        var componentModel = VariablesGlobales.Instance.UnityContainer.Resolve<IComponentModel>();
        return componentModel.GetRowByName(componentName);
    }
}