using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface ICoreWebParameter
    {
        TbCompanyParameter? GetParameter(string parameterName, int companyId);

        string? GetParameterValue(string parameterName, int companyId);

        /// <summary>
        /// El diccionario resultante contiene los nombres de los parámetros como claves y los valores de los parámetros
        /// para la compañía especificada como valores. Si un parámetro no tiene un valor para la compañía especificada,
        /// no se incluirá en el diccionario resultante.
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>Dictionary Clave-Valor</returns>
        Dictionary<string, string> GetParameterAll(int companyId);

        /// <summary>
        /// Este método es útil para obtener el ID de un parámetro específico que está asociado con una compañía en un sistema.
        /// </summary>
        /// <param name="parameterName">Nombre del parametro</param>
        /// <param name="companyId">Id de la compañia</param>
        /// <returns></returns>
        /// <exception cref="Exception">Lanza una excepción si el parámetro con el nombre dado no existe o si el
        /// parámetro no está asociado con la compañía especificada.</exception>
        int GetParameterId(string parameterName, int companyId);
    }
}