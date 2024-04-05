using System.Diagnostics.Eventing.Reader;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CoreWebPermission(
        ICoreWebParameter coreWebParameter,
        IUserModel userModel,
        ICompanyParameterModel parameterModel)
        : ICoreWebPermission
    {
        private static readonly string? UrlSuffixNew = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX_NEW"];
        private static readonly string? UrlSuffixOld = VariablesGlobales.ConfigurationBuilder["URL_SUFFIX_OLD"];

        public int GetElementId(string controller, string method, string suffix, List<TbMenuElement> dataMenuTop,
            List<TbMenuElement> dataMenuLeft,
            List<TbMenuElement> dataMenuBodyReport, List<TbMenuElement> dataMenuBodyTop,
            List<TbMenuElement> dataMenuHiddenPopup)
        {
            var urlSuffixNew = UrlSuffixNew;
            var urlSuffixOld = UrlSuffixNew;

            var url = controller.ToLower().Replace("app\\controllers\\", "") + "/" + method + suffix;
            var urlIndex = controller.ToLower().Replace("app\\controllers\\", "") + "/index" + suffix;
            if (dataMenuHiddenPopup.Count > 0)
            {
                foreach (var menuElement in dataMenuHiddenPopup)
                {
                    string urlCompare = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(urlCompare, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return menuElement.MenuElementId;
                    }
                }
            }

            if (dataMenuBodyTop.Count > 0)
            {
                foreach (var menuElement in dataMenuBodyTop)
                {
                    var urlCompare = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(urlCompare, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return menuElement.MenuElementId;
                    }
                }
            }

            if (dataMenuBodyReport.Count > 0)
            {
                foreach (var menuElement in dataMenuBodyReport)
                {
                    var urlCompare = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(urlCompare, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return menuElement.MenuElementId;
                    }
                }
            }

            if (dataMenuTop.Count > 0)
            {
                foreach (var menuElement in dataMenuTop)
                {
                    var urlCompare = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(urlCompare, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return menuElement.MenuElementId;
                    }
                }
            }

            if (dataMenuLeft.Count > 0)
            {
                foreach (var menuElement in dataMenuLeft)
                {
                    var urlCompare = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(urlCompare, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return menuElement.MenuElementId;
                    }
                }
            }

            return 0;
        }

        public bool UrlPermited(string controller, string method, string suffix, List<TbMenuElement> dataMenuTop,
            List<TbMenuElement> dataMenuLeft,
            List<TbMenuElement> dataMenuBodyReport, List<TbMenuElement> dataMenuBodyTop,
            List<TbMenuElement> dataMenuHiddenPopup)
        {
            var url = controller.ToLower().Replace("app\\controllers\\", "") + "/" + method + suffix;
            var urlIndex = controller.ToLower().Replace("app\\controllers\\", "") + "/index" + suffix;

            if (dataMenuHiddenPopup.Count > 0)
            {
                foreach (var menuElement in dataMenuHiddenPopup)
                {
                    var url2 = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(url2, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                    else if (string.Equals(url2, urlIndex, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (dataMenuBodyTop.Count > 0)
            {
                foreach (var menuElement in dataMenuBodyTop)
                {
                    var url2 = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(url2, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                    else if (string.Equals(url2, urlIndex, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (dataMenuBodyReport.Count > 0)
            {
                foreach (var menuElement in dataMenuBodyReport)
                {
                    var url2 = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(url2, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                    else if (string.Equals(url2, urlIndex, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (dataMenuLeft.Count > 0)
            {
                foreach (var menuElement in dataMenuLeft)
                {
                    var url2 = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(url2, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                    else if (string.Equals(url2, urlIndex, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (dataMenuTop.Count > 0)
            {
                foreach (var menuElement in dataMenuTop)
                {
                    var url2 = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(url2, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                    else if (string.Equals(url2, urlIndex, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool UrlPermissionCmd(string controller, string method, string suffix, TbRole role,
            TbUser user, List<TbMenuElement> dataMenuTop, List<TbMenuElement> dataMenuLeft,
            List<TbMenuElement> dataMenuBodyReport, List<TbMenuElement> dataMenuBodyTop,
            List<TbMenuElement> dataMenuHiddenPopup)
        {
            var userPermissionModel = VariablesGlobales.Instance.UnityContainer.Resolve<IUserPermissionModel>();
            var permissionNone = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_NONE"]);
            var permissionAll = Convert.ToInt32(VariablesGlobales.ConfigurationBuilder["PERMISSION_ALL"]);
            var url = controller.ToLower().Replace("app\\controllers\\", "") + "/" + method + suffix;
            var url2 = controller.ToLower().Replace("app\\controllers\\", "") + "/index" + suffix;

            var elementId = 0;
            if (role.IsAdmin!.Value)
            {
                return true;
            }

            if (dataMenuTop.Count > 0)
            {
                foreach (var menuElement in dataMenuTop)
                {
                    var replace = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (replace.ToUpper() == url.ToUpper())
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                    else if (replace.ToUpper() == url2.ToUpper())

                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                }
            }

            if (dataMenuLeft.Count > 0)
            {
                foreach (var menuElement in dataMenuLeft)
                {
                    var replace = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(replace, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                    else if (string.Equals(replace, url2, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                }
            }

            if (dataMenuBodyReport.Count > 0)
            {
                foreach (var menuElement in dataMenuBodyReport)
                {
                    var replace = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(replace, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                    else if (string.Equals(replace, url2, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                }
            }

            if (dataMenuBodyTop.Count > 0)
            {
                foreach (var menuElement in dataMenuBodyTop)
                {
                    var replace = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(replace, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                    else if (string.Equals(replace, url2, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                }
            }

            if (dataMenuHiddenPopup.Count > 0)
            {
                foreach (var menuElement in dataMenuHiddenPopup)
                {
                    var replace = menuElement.Address!.Replace(UrlSuffixOld!, UrlSuffixNew);
                    if (string.Equals(replace, url, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                    else if (string.Equals(replace, url2, StringComparison.CurrentCultureIgnoreCase))
                    {
                        elementId = menuElement.ElementId;
                        break;
                    }
                }
            }

            if (elementId == 0)
            {
                return false;
            }

            var rowRolePermission = userPermissionModel.GetRowByPk(user.CompanyId, user.BranchId,role.RoleId, elementId);
            if (rowRolePermission == null)
            {
                return false;
            }

            return method switch
            {
                "index" => rowRolePermission.Selected!.Value,
                "edit" => rowRolePermission.Edited!.Value,
                "delete" => rowRolePermission.Deleted!.Value,
                "add" => rowRolePermission.Inserted!.Value,
                _ => false
            };
        }

        public void GetValueLicense(int companyId, string url)
        {
            var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
            var objParameterMaxUser = coreWebParameter.GetParameter("CORE_CUST_PRICE_MAX_USER", companyId);
            var parameterFechaExpiration =
                DateTime.Parse(coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId)!.Value);
            var objParameterISleep =
                int.Parse(coreWebParameter.GetParameter("CORE_CUST_PRICE_SLEEP", companyId)!.Value);
            var objParameterTipoPlan = coreWebParameter.GetParameter("CORE_CUST_PRICE_TIPO_PLAN", companyId)!.Value;
            var objParameterExpiredLicense =
                DateTime.Parse(coreWebParameter.GetParameter("CORE_CUST_PRICE_LICENCES_EXPIRED", companyId)!.Value);
            var objParameterCreditos = coreWebParameter.GetParameter("CORE_CUST_PRICE_BALANCE", companyId);
            var objParameterCreditosId = objParameterCreditos!.ParameterId;
            var parameterCreditos = int.Parse(objParameterCreditos.Value);
            var objParameterPriceByInvoice =
                int.Parse(coreWebParameter.GetParameter("CORE_CUST_PRICE_BY_INVOICE", companyId)!.Value);
            var parameterMaxUser = int.Parse(objParameterMaxUser!.Value);
            if (parameterMaxUser > 0)
            {
                var count = userModel.GetCount(companyId);
                if ((count + 1) > parameterMaxUser)
                {
                    throw new Exception("""
                                        
                                            Ha superado el número máximo de usuarios.
                                            Teléfono de contacto: 8712-5827 para activar licencia
                                            Realizar el pago de la licencia aquí o
                                            Realizar la transferencia a la siguiente cuenta BAC Dólares: 366-577-484 
                                                                            
                                        """);
                }
            }

            var fechaNow = DateTime.Now;
            if (fechaNow > parameterFechaExpiration)
            {
                throw new Exception("""
                                    
                                                    La licencia ha expirado.
                                                    Teléfono de contacto: 8712-5827 para activar licencia
                                                    Realizar el pago de la licencia aquí o
                                                    Realizar la transferencia a la siguiente cuenta BAC Dólares: 366-577-484
                                                    
                                    """);
            }


            if (objParameterTipoPlan == "CONSUMIBLE")
            {
                if ((parameterCreditos - objParameterPriceByInvoice) < 0)
                {
                    throw new Exception("""
                                        
                                                        No tiene suficientes créditos.
                                                        Teléfono de contacto: 8712-5827 para activar licencia</p>
                                                        Realizar el pago de la licencia aquí o
                                                        Realizar la transferencia a la siguiente cuenta BAC Dólares: 366-577-484
                                                        
                                        """);
                }
            
            }

            parameterCreditos -= objParameterPriceByInvoice;
            var dataNewParameter = companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameterCreditosId);
            dataNewParameter!.Value = parameterCreditos.ToString();
            parameterModel.UpdateAppPosme(companyId, objParameterCreditosId, dataNewParameter);


            var parameterCantiadTransacciones = coreWebParameter.GetParameter("CORE_QUANTITY_TRANSACCION", companyId);
            var parameterCantiadTransaccionesId = parameterCantiadTransacciones!.ParameterId;
            var parameterCantiadTransaccionesValue = int.Parse(parameterCantiadTransacciones.Value);
            var parameterCantiadTransaccionesNewValor = parameterCantiadTransaccionesValue + 1;
            var dataNewParameterCantidadTransacciones = companyParameterModel.GetRowByParameterIdCompanyId(companyId, parameterCantiadTransaccionesId);
            dataNewParameterCantidadTransacciones!.Value = parameterCantiadTransaccionesNewValor.ToString();
            parameterModel.UpdateAppPosme(companyId, parameterCantiadTransaccionesId,dataNewParameterCantidadTransacciones);

            if (fechaNow > objParameterExpiredLicense && objParameterTipoPlan != "MEMBRESIA")
            {
                var diff = objParameterExpiredLicense - fechaNow;
                var days = Math.Abs(diff.Days) + objParameterISleep;
                if (days > 60)
                {
                    days = 60;
                }

                if (days > 0)
                {
                    Thread.Sleep(days);
                }
            }
        }


        
    }
}