using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebView(
    IDataViewModel dataViewModel,
    ICompanyDataViewModel companyDataViewModel,
    ICompanyModel companyModel,
    IBdModel bdModel)
    : ICoreWebView
{
    private static readonly int PermissionAll =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ALL"]!);

    private static readonly int PermissionNone =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]!);

    private static readonly int PermissionBranch =
        Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_BRANCH"]!);

    private static readonly int
        PermissionMe = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ME"]!);

    public TableCompanyDataViewDto? GetViewByName(TbUser user, int componentId, string? name, int callerId, int? permission = null, Dictionary<string?, string?>? parameter = null)
    {
        // Obtener la vista generica
        var dataView = dataViewModel.GetViewByName(componentId, name, callerId);
        if (dataView is null)
            return null;

        // Obtener la compañia
        var objCompany = companyModel.GetRowByPk(user.CompanyID);

        // Obtener la vista por el flavor
        var dataViewId = dataView.DataViewID;

        var companyDataView = companyDataViewModel.GetRowByCompanyIdDataViewIdAndFlavor(user.CompanyID, dataViewId,callerId, componentId, objCompany.FlavorID);
        if (companyDataView is null)
        {
            // Obtener la vista unica
            companyDataView = companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyID, dataViewId, callerId, componentId);
            if (companyDataView is null)
            return null;
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
            filterPermission = " AND x.createdAt = " + user.BranchID;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserID;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);

        // Ejecutar consulta
        var objResult = new TableCompanyDataViewDto
        {
            Config = companyDataView
        };
        var dataRecordSet = bdModel.ExecuteRenderQueryable(queryFill);
        if (dataRecordSet is null)
        {
            return null;
        }
        objResult.Data = dataRecordSet;
        return objResult;
    }

    public TableCompanyDataViewDto GetViewByDataViewId(TbUser user, int componentId, int dataviewId, int callerId,
        int? permission = null,
        Dictionary<string?, string?>? parameter = null)
    {
        var companyDataView =
            companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyID, dataviewId, callerId, componentId);
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
            filterPermission = " AND x.createdAt = " + user.BranchID;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserID;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);


        TableCompanyDataViewDto objResult = new TableCompanyDataViewDto();
        objResult.Config = companyDataView;
        var dataRecordSet = bdModel.ExecuteRender<dynamic>(queryFill);
        objResult.Data = dataRecordSet;
        return objResult;

        ;
    }

    public TableCompanyDataViewDto GetView(TbUser user, int? componentId = null, int? callerId = null,
        int? permission = null,
        Dictionary<string?, string?>? parameter = null)
    {
        var objListView = dataViewModel.GetListByCompanyComponentCaller(componentId!.Value, callerId!.Value);
        if (objListView is null)
        {
            throw new Exception("No existe el data view");
        }

        var companyDataView =
            companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyID, objListView.DataViewID, callerId!.Value,
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
            filterPermission = " AND x.createdAt = " + user.BranchID;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserID;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);


        var objResult = new TableCompanyDataViewDto
        {
            Config = companyDataView
        };
        var dataRecordSet = bdModel.ExecuteRender<dynamic>(queryFill);
        objResult.Data = dataRecordSet;
        return objResult;
    }

    public TableCompanyDataViewDto? GetViewDefault(TbUser user, int? componentId = null, int? callerId = null,
        int? targetComponentId = null, int? permission = null, Dictionary<string?, string?>? parameter = null)
    {
        var companyDefaultDataViewModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyDefaultDataViewModel>();
        var objCompanyDefaultDataView = companyDefaultDataViewModel.GetRowByCcct(user.CompanyID, componentId!.Value, callerId!.Value, targetComponentId!.Value);
        if (objCompanyDefaultDataView is null)
        {
            return null;
        }

        var companyDataView = companyDataViewModel.GetRowByCompanyIdDataViewId(user.CompanyID, objCompanyDefaultDataView.DataViewID, callerId.Value, componentId.Value);
        if (companyDataView is null)
        {
            return null;
        }

        parameter ??= new Dictionary<string?, string?>();
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
            filterPermission = " AND x.createdAt = " + user.BranchID;
        }
        else if (permission == PermissionMe)
        {
            filterPermission = " AND x.createdBy = " + user.UserID;
        }
        else
        {
            filterPermission = "";
        }

        queryFill = queryFill.Replace("{filterPermission}", filterPermission);
        var objResult = new TableCompanyDataViewDto { Config = companyDataView };

        //var dataRecordSet = bdModel.ExecuteRender<dynamic>(queryFill);
        var dataRecordSet = bdModel.ExecuteRenderQueryable(queryFill);
        if (dataRecordSet is null)
        {
            return null;
        }

        objResult.Data = dataRecordSet;
        return objResult;
    }

    private static string? ReplacePlaceholders(string? input, Dictionary<string?, string?> parameter)
    {
        return parameter.Aggregate(input, (current, entry) => current.Replace(entry.Key, entry.Value));
    }
}