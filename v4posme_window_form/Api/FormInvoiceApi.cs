using DevExpress.XtraEditors;
using Google.Protobuf.WellKnownTypes;
using System.Data;
using System.Printing;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Implementacion;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Dto;
using v4posme_window.Libraries;

namespace v4posme_window.Api
{
    public class FormInvoiceApi
    {
        private readonly ICoreWebPermission _objInterfazCoreWebPermission = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebPermission>();
        private readonly ICoreWebCurrency _objInterfazCoreWebCurrency = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCurrency>();
        private readonly ICoreWebParameter _objInterfazCoreWebParameter = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();
        private readonly ICustomerModel _objInterfazCustomerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
        private readonly ICustomerCreditLineModel _objInterfazCustomerCreditLineModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditLineModel>();
        private readonly ICustomerCreditAmortizationModel _objInterfazCustomerCreditAmortizationModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerCreditAmortizationModel>();






        public FormInvoiceApiGetValidExistenciaDTO GetValidExistencia(int warehouseID , int itemID,decimal quantity)
        {
            
            var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
            var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
            var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
            var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
            var user = VariablesGlobales.Instance.User;
            FormInvoiceApiGetValidExistenciaDTO result = new FormInvoiceApiGetValidExistenciaDTO();

            if (user is null)
            {
                throw new Exception(userNotAutenticated);
            }

            var ParametroFacturacionZero = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_QUANTITY_ZERO", user.CompanyID);
            IItemModel _objItemModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemModel>();
            IItemWarehouseModel _objItemWarehouseModel = VariablesGlobales.Instance.UnityContainer.Resolve<IItemWarehouseModel>();
            var objItemModel = _objItemModel.GetRowByPk(user.CompanyID, itemID);
            var objItemWarehouseModel = _objItemWarehouseModel.GetByPk(user.CompanyID, itemID, warehouseID);

            if (
                objItemWarehouseModel.Quantity < quantity &&
                objItemModel.IsInvoiceQuantityZero == 0 &&
                ParametroFacturacionZero!.Value!.ToUpper() == "false".ToUpper()
            )
            {
                result.Codigo = objItemModel.ItemNumber;
                result.Nombre = objItemModel.Name;
                result.Mensaje = "Existencia agotada";
                result.QuantityInWarehouse = objItemWarehouseModel.Quantity;
            }
            
            return result;

        }
        public FormInvoiceApiGetLineByCustomerDTO? GetLineByCustomer(int entityID)
        {
            try
            {
                
                var userNotAutenticated = VariablesGlobales.ConfigurationBuilder["USER_NOT_AUTENTICATED"];
                var notAccessControl = VariablesGlobales.ConfigurationBuilder["NOT_ACCESS_CONTROL"];
                var notAllEdit = VariablesGlobales.ConfigurationBuilder["NOT_ALL_EDIT"];
                var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
                var appNeedAuthentication = VariablesGlobales.ConfigurationBuilder["APP_NEED_AUTHENTICATION"];
                var urlSuffix = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX"];
                var user = VariablesGlobales.Instance.User;
                if (user is null)
                {
                    throw new Exception(userNotAutenticated);
                }

                var role = VariablesGlobales.Instance.Role;
                var objCurrencyDolares  = _objInterfazCoreWebCurrency.GetCurrencyExternal(user.CompanyID);
                var objCurrencyCordoba  = _objInterfazCoreWebCurrency.GetCurrencyDefault(user.CompanyID);
                var dateOn              = DateTime.Today;
                var ExchangeRate        = _objInterfazCoreWebCurrency.GetRatio(user.CompanyID, dateOn,decimal.One, objCurrencyDolares!.CurrencyID, objCurrencyCordoba!.CurrencyID);
                var ParameterCausalTypeCredit   = _objInterfazCoreWebParameter.GetParameter("INVOICE_BILLING_CREDIT", user.CompanyID);
                var ObjCustomer                 = _objInterfazCustomerModel.GetRowByEntity(user.CompanyID,entityID);
                var BranchID                    = ObjCustomer is not null ? ObjCustomer.BranchId : 0;
                var ObjListCustomerCreditLine2  = _objInterfazCustomerCreditLineModel.GetRowByEntity(user.CompanyID, BranchID, entityID);
                var ObjListCustomerCreditLine   = new List<TbCustomerCreditLineDto>();       
                
                //Obtener linesas de credito
                if(ObjListCustomerCreditLine2.Count > 0 )
                {
                    for( var i = 0; i < ObjListCustomerCreditLine2.Count; i++ )
                    {
                        if (ObjListCustomerCreditLine2[i].Balance > 0 )
                        {
                            ObjListCustomerCreditLine.Add(ObjListCustomerCreditLine2[i]);
                        }
                    }
                }

                //Obtener tablas de amortizaciones
                var ObjCustomerCreditAmoritizationAll = _objInterfazCustomerCreditAmortizationModel.GetRowByCustomerId(entityID);
                var Result = new FormInvoiceApiGetLineByCustomerDTO();
                Result.objCustomer = ObjCustomer;
                Result.ObjListCustomerCreditLine = ObjListCustomerCreditLine;
                Result.ExchangeRate = ExchangeRate;
                Result.ObjCustomerCreditAmoritizationAll = ObjCustomerCreditAmoritizationAll;
                Result.objCurrencyDolares = objCurrencyDolares;
                Result.objCurrencyCordoba = objCurrencyCordoba;
                Result.ParameterCausalTypeCredit = ParameterCausalTypeCredit;
                return Result;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Se produjo el siguiente error: {ex.Message}");
                return null;
            }
        }
        public DataTable? GetViewApi(int componentId,string viewName = "",string filter = "")
        {
            var coreWebTools = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
            var coreWebView = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebView>();
            var usuario = VariablesGlobales.Instance.User;
            var calleridSearch = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["CALLERID_SEARCH"]);

            //Parametros
            var parameter = new Dictionary<string, string>
            {
                ["{companyID}"] = usuario!.CompanyID.ToString()
            };

            // Agregar al diccionarios los parametros dinamicos
            var result = coreWebTools.FormatParameter(filter);
            if (result is not null)
            {
                foreach (var kvp in result)
                {
                    parameter[kvp.Key] = kvp.Value.ToString()!;
                }
            }

            var resultdo = coreWebView.GetViewByName(usuario, componentId, viewName, calleridSearch, null, parameter);
            var viewData = (List<Dictionary<string, object>>)resultdo!.Data!;
            var table = CoreWebRenderInView.FillGridControl(viewData);
            return table;


        }
    }
}
