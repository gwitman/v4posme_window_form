using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebParameter : ICoreWebParameter
    {
        public TbCompanyParameter? GetParameter(string parameterName, int companyId)
        {
            var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
            var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
            var parameter = parameterModel.GetRowByName(parameterName);
            if (parameter is null)
            {
                throw new Exception($"NO EXISTE EL PARAMETRO {parameterName} PARA LA COMPANY {companyId}");
            }

            var companyParameter =
                companyParameterModel.GetRowByParameterIdCompanyId(companyId, parameter.ParameterId);
            if (companyParameter is null)
            {
                throw new Exception($"NO EXISTE EL PARAMETRO {parameterName} PARA LA COMPANY {companyId}");
            }

            return companyParameter!;
        }

        public string? GetParameterValue(string parameterName, int companyId)
        {
            var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
            var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
            var objParameter = parameterModel.GetRowByName(parameterName);
            if (objParameter is null)
            {
                throw new Exception($"NO EXISTE EL PARAMETRO {parameterName}");
            }

            var objCompanyParameter =
                companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterId);
            if (objCompanyParameter is null)
            {
                throw new Exception($"NO EXISTE EL PARAMETRO {parameterName} PARA LA COMPANY {companyId}");
            }

            return objCompanyParameter.Value;
        }

        /// <summary>
        /// El diccionario resultante contiene los nombres de los parámetros como claves y los valores de los parámetros
        /// para la compañía especificada como valores. Si un parámetro no tiene un valor para la compañía especificada,
        /// no se incluirá en el diccionario resultante.
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>Dictionary Clave-Valor</returns>
        public Dictionary<string, string?> GetParameterAll(int companyId)
        {
            var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
            var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
            var objParameterList = parameterModel.GetAll();
            var data = new Dictionary<string, string?>();
            if (objParameterList == null) return data;
            foreach (var objParameter in objParameterList)
            {
                if (objParameter == null) continue;
                var objCompanyParameter =
                    companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterId);
                if (objCompanyParameter != null)
                {
                    data[objParameter.Name!] = objCompanyParameter.Value;
                }
            }

            return data;
        }

        /// <summary>
        /// Este método es útil para obtener el ID de un parámetro específico que está asociado con una compañía en un sistema.
        /// </summary>
        /// <param name="parameterName">Nombre del parametro</param>
        /// <param name="companyId">Id de la compañia</param>
        /// <returns></returns>
        /// <exception cref="Exception">Lanza una excepción si el parámetro con el nombre dado no existe o si el
        /// parámetro no está asociado con la compañía especificada.</exception>
        public int GetParameterId(string parameterName, int companyId)
        {
            var _companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
            var _parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
            // Obtener el Parámetro
            var objParameter = _parameterModel.GetRowByName(parameterName);
            if (objParameter == null)
                throw new Exception($"NO EXISTE EL PARAMETRO {parameterName}");

            // Obtener el CompanyParameter
            var objCompanyParameter =
                _companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterId);
            if (objCompanyParameter == null)
                throw new Exception($"NO EXISTE EL PARAMETRO {parameterName} PARA LA COMPANY {companyId}");

            return objCompanyParameter.ParameterId;
        }
    }
}