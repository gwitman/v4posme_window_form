using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebWhatsap : ICoreWebWhatsap
{
    public bool ValidSendMessage(int companyId)
    {
        var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
        var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();

        var objPWhatsapMonth = parameterModel.GetRowByName("WHATSAP_MONTH");
        var objPWhatsapMonthId = objPWhatsapMonth!.ParameterId;
        var objCpWhatsapMonth = companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapMonthId);

        var objPWhatsapMessageByMonto = parameterModel.GetRowByName("WHATSAP_MESSAGE_BY_MONTO");
        var objPWhatsapMessageByMontoId = objPWhatsapMessageByMonto!.ParameterId;
        var objCP_WhatsapMessageByMonto =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapMessageByMontoId);

        var objPWhatsapCounterMessage = parameterModel.GetRowByName("WHATSAP_COUNTER_MESSAGE");
        var objPWhatsapCounterMessageId = objPWhatsapCounterMessage!.ParameterId;
        var objCpWhatsapCounterMessage =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapCounterMessageId);

        var fechaNow = DateTime.Now;
        var fechaMonth = DateTime.Parse(objCpWhatsapMonth!.Value);

        if (fechaNow == fechaMonth && int.Parse(objCpWhatsapCounterMessage!.Value) <=
            int.Parse(objCP_WhatsapMessageByMonto!.Value))
        {
            var data = companyParameterModel.GetRowByParameterIdCompanyId(objCpWhatsapCounterMessage.CompanyId,
                objCpWhatsapCounterMessage.ParameterId);
            data!.Value = (int.Parse(objCpWhatsapCounterMessage.Value) + 1).ToString();
            companyParameterModel.UpdateAppPosme(objCpWhatsapCounterMessage.CompanyId,
                objCpWhatsapCounterMessage.ParameterId, data);
            return true;
        }

        if (fechaNow > fechaMonth && int.Parse(objCpWhatsapCounterMessage!.Value) > 0)
        {
            var data = companyParameterModel
                .GetRowByParameterIdCompanyId(objCpWhatsapMonth.CompanyId, objCpWhatsapMonth.ParameterId);
            data!.Value = fechaNow.ToString("yyyy-MM-dd");
            companyParameterModel.UpdateAppPosme(objCpWhatsapMonth.CompanyId, objCpWhatsapMonth.ParameterId, data);

            data = companyParameterModel
                .GetRowByParameterIdCompanyId(objCpWhatsapCounterMessage.CompanyId,
                    objCpWhatsapCounterMessage.ParameterId);
            data!.Value=1.ToString();
            companyParameterModel.UpdateAppPosme(objCpWhatsapCounterMessage.CompanyId,
                objCpWhatsapCounterMessage.ParameterId, data);
            return true;
        }

        return false;
    }
}