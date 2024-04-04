using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebView : ICoreWebView
{
    private static readonly int PermissionAll =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ALL"]!);

    private static readonly int PermissionNone =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]!);

    private static readonly int PermissionBranch =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_BRANCH"]!);

    private static readonly int
        PermissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]!);

    private readonly IDataViewModel
        _dataViewModel = VariablesGlobales.Instance.UnityContainer.Resolve<IDataViewModel>();

    private readonly ICompanyDataViewModel _companyDataViewModel =
        VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyDataViewModel>();

    private readonly ICompanyModel _companyModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyModel>();

    private readonly IBdModel _bdModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBdModel>();

    public TableCompanyDataViewDto  GetViewByName(TbUser user, int componentId, string name, int callerId, int? permission = null,
        Dictionary<string, string>? parameter = null)
    {
        // Obtener la vista generica
        var dataView = _dataViewModel.GetViewByName(componentId, name, callerId);
        if (dataView is null)
            return null;

        // Obtener la compañia
        var objCompany = _companyModel.GetRowByPk(user.CompanyId);

        // Obtener la vista por el flavor
        var dataViewId = dataView.DataViewId;

        var companyDataView = _companyDataViewModel.GetRowByCompanyIdDataViewIdAndFlavor(user.CompanyId, dataViewId,
            callerId, componentId, objCompany.FlavorId);
        if (companyDataView is null)
        {
            // Obtener la vista unica
            companyDataView =
                _companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyId, dataViewId, callerId, componentId);
            if (companyDataView is null)
                return null;
        }

        // EXECUTE
        var queryFill = ReplacePlaceholders(dataView.SqlScript!, parameter!);

        // Aplicar Filtros y Asignar Variables
        var filterPermission = "";
        if (permission == PermissionAll)
        {
            filterPermission = "";
        }
        else if (permission == PermissionNone)
        {
            filterPermission = " AND 1 != 1 ";
        }
        else if (permission == PermissionBranch)
        {
            filterPermission = " AND x.createdAt = " + user.BranchId;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserId;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);

        // Ejecutar consulta
        TableCompanyDataViewDto objResult  = new TableCompanyDataViewDto();
        objResult.config                = companyDataView;
        var dataRecordSet               = _bdModel.ExecuteRender<dynamic>(queryFill);
        objResult.data = dataRecordSet;
        return objResult;
    }

    public TableCompanyDataViewDto GetViewByDataViewId(TbUser user, int componentId, int dataviewId, int callerId, int? permission = null,
        Dictionary<string, string>? parameter = null)
    {
        var companyDataView =
            _companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyId, dataviewId, callerId, componentId);
        if (companyDataView is null)
        {
            throw new Exception("No existe el company data view");
        }

        // EXECUTE
        var queryFill = ReplacePlaceholders(companyDataView.SqlScript!, parameter!);
        // Aplicar Filtros y Asignar Variables
        var filterPermission = "";
        if (permission == PermissionAll)
        {
            filterPermission = "";
        }
        else if (permission == PermissionNone)
        {
            filterPermission = " AND 1 != 1 ";
        }
        else if (permission == PermissionBranch)
        {
            filterPermission = " AND x.createdAt = " + user.BranchId;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserId;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);


        TableCompanyDataViewDto objResult = new TableCompanyDataViewDto();
        objResult.config = companyDataView;
        var dataRecordSet = _bdModel.ExecuteRender<dynamic>(queryFill);
        objResult.data = dataRecordSet;
        return objResult;

      ;
    }

    public TableCompanyDataViewDto GetView(TbUser user, int? componentId = null, int? callerId = null, int? permission = null,
        Dictionary<string, string>? parameter = null)
    {
        var objListView = _dataViewModel.GetListByCompanyComponentCaller(componentId!.Value, callerId!.Value);
        if (objListView is null)
        {
            throw new Exception("No existe el data view");
        }

        var companyDataView =
            _companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyId, objListView.DataViewId, callerId!.Value,
                componentId!.Value);
        if (companyDataView is null)
        {
            throw new Exception("No existe el company data view");
        }

        // EXECUTE
        var queryFill = ReplacePlaceholders(companyDataView.SqlScript!, parameter!);
        // Aplicar Filtros y Asignar Variables
        var filterPermission = "";
        if (permission == PermissionAll)
        {
            filterPermission = "";
        }
        else if (permission == PermissionNone)
        {
            filterPermission = " AND 1 != 1 ";
        }
        else if (permission == PermissionBranch)
        {
            filterPermission = " AND x.createdAt = " + user.BranchId;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserId;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);


        TableCompanyDataViewDto objResult = new TableCompanyDataViewDto();
        objResult.config = companyDataView;
        var dataRecordSet = _bdModel.ExecuteRender<dynamic>(queryFill);
        objResult.data = dataRecordSet;
        return objResult;


    }

    public TableCompanyDataViewDto GetViewDefault(TbUser user, int? componentId = null, int? callerId = null,
        int? targetComponentId = null,
        int? permission = null, Dictionary<string, string>? parameter = null)
    {
        var companyDefaultDataViewModel =
            VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyDefaultDataViewModel>();
        var objCompanyDefaultDataView =
            companyDefaultDataViewModel.GetRowByCcct(user.CompanyId, componentId!.Value,
                callerId!.Value, targetComponentId!.Value);
        if (objCompanyDefaultDataView is null)
        {
            throw new Exception("No existe el company default data view");
        }

        var companyDataView = _companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyId,
            objCompanyDefaultDataView.DataViewId, callerId.Value, componentId.Value);
        if (companyDataView is null)
        {
            throw new Exception("No existe el company data view");
        }

        // EXECUTE
        var queryFill = ReplacePlaceholders(companyDataView.SqlScript!, parameter!);
        // Aplicar Filtros y Asignar Variables
        var filterPermission = "";
        if (permission == PermissionAll)
        {
            filterPermission = "";
        }
        else if (permission == PermissionNone)
        {
            filterPermission = " AND 1 != 1 ";
        }
        else if (permission == PermissionBranch)
        {
            filterPermission = " AND x.createdAt = " + user.BranchId;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserId;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);

        TableCompanyDataViewDto objResult = new TableCompanyDataViewDto();
        objResult.config = companyDataView;
        var dataRecordSet = _bdModel.ExecuteRender<dynamic>(queryFill);
        objResult.data = dataRecordSet;
        return objResult;


    }

    private static string ReplacePlaceholders(string input, Dictionary<string, string> parameter)
    {
        // Reemplazar los placeholders en la cadena de entrada con los valores del diccionario

        return parameter.Aggregate(input, (current, entry) => current.Replace(entry.Key, entry.Value));
    }
}