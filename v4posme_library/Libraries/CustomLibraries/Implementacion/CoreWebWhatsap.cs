using System.Net.Http.Headers;
using Newtonsoft.Json;
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
        var objCpWhatsapMessageByMonto =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapMessageByMontoId);

        var objPWhatsapCounterMessage = parameterModel.GetRowByName("WHATSAP_COUNTER_MESSAGE");
        var objPWhatsapCounterMessageId = objPWhatsapCounterMessage!.ParameterId;
        var objCpWhatsapCounterMessage =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapCounterMessageId);

        var fechaNow = DateTime.Now;
        var fechaMonth = DateTime.Parse(objCpWhatsapMonth!.Value);

        if (fechaNow.Month == fechaMonth.Month && int.Parse(objCpWhatsapCounterMessage!.Value) <=
            int.Parse(objCpWhatsapMessageByMonto!.Value))
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
            data!.Value = 1.ToString();
            companyParameterModel.UpdateAppPosme(objCpWhatsapCounterMessage.CompanyId,
                objCpWhatsapCounterMessage.ParameterId, data);
            return true;
        }

        return false;
    }

    public void SendMessage(int companyId, string message)
    {
        var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
        var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();

        var objPWhatsapPropertyNumber = parameterModel.GetRowByName("WHATSAP_CURRENT_PROPIETARY_COMMERSE");
        var objPWhatsapPropertyNumberId = objPWhatsapPropertyNumber!.ParameterId;
        var objCpWhatsapPropertyNumber =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapPropertyNumberId);

        var objPWhatsapToken = parameterModel.GetRowByName("WHATSAP_TOCKEN");
        var objPWhatsapTokenId = objPWhatsapToken!.ParameterId;
        var objCpWhatsapToken = companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapTokenId);

        var objPWhatsapUrlSession = parameterModel.GetRowByName("WHATSAP_URL_REQUEST_SESSION");
        var objPWhatsapUrlSessionId = objPWhatsapUrlSession!.ParameterId;
        var objCpWhatsapUrlSession =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapUrlSessionId);

        var objPWhatsapUrlSendMessage = parameterModel.GetRowByName("WAHTSAP_URL_ENVIO_MENSAJE");
        var objPWhatsapUrlSendMessageId = objPWhatsapUrlSendMessage!.ParameterId;
        var objCpWhatsapUrlSendMessage =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapUrlSendMessageId);

        var clientCurl = new HttpClient();
        var response = clientCurl.GetAsync(objCpWhatsapUrlSession!.Value).Result;
        var responseBody = response.Content.ReadAsStringAsync().Result;
        var jsonResponse = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(responseBody);

        if (jsonResponse!.Count > 0 && jsonResponse[0].ContainsKey("id"))
        {
            var sessionId = jsonResponse[0]["id"].ToString();
            var sendWhatsapp = new Dictionary<string, string>
            {
                { "whatsappId", sessionId! },
                { "number", objCpWhatsapPropertyNumber!.Value },
                { "name", "posMe" },
                { "body", message }
            };

            var clientCurl2 = new HttpClient();
            var content = new FormUrlEncodedContent(sendWhatsapp);
            using var requestMessage =
                new HttpRequestMessage(HttpMethod.Post, objCpWhatsapUrlSendMessage!.Value);
            requestMessage.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", objCpWhatsapToken!.Value);
            requestMessage.Content = content;
            clientCurl2.SendAsync(requestMessage);
            //var response2 = clientCurl2.PostAsync(objCpWhatsapUrlSendMessage!.Value, content).Result;
        }
    }

    public async Task<string> SendMessageAsync(int companyId, string message, string phoneDestino = "")
    {
        var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
        var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
        // Obtener los parámetros necesarios
        var objPWhatsapPropertyNumber = parameterModel.GetRowByName("WHATSAP_CURRENT_PROPIETARY_COMMERSE");
        var objPWhatsapPropertyNumberId = objPWhatsapPropertyNumber!.ParameterId;
        var objCpWhatsapPropertyNumber =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapPropertyNumberId);

        var objPWhatsapToken = parameterModel.GetRowByName("WHATSAP_TOCKEN");
        var objPWhatsapTokenId = objPWhatsapToken!.ParameterId;
        var objCpWhatsapToken =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapTokenId);

        var objPWhatsapUrlSession = parameterModel.GetRowByName("WHATSAP_URL_REQUEST_SESSION");
        var objPWhatsapUrlSessionId = objPWhatsapUrlSession!.ParameterId;
        var objCpWhatsapUrlSession =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapUrlSessionId);

        var objPWhatsapUrlSendMessage = parameterModel.GetRowByName("WAHTSAP_URL_ENVIO_MENSAJE");
        var objPWhatsapUrlSendMessageId = objPWhatsapUrlSendMessage!.ParameterId;
        var objCpWhatsapUrlSendMessage =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapUrlSendMessageId);

        // Establecer el número de teléfono de destino
        phoneDestino = string.IsNullOrEmpty(phoneDestino) ? "" : phoneDestino;

        // Construir los parámetros para la solicitud HTTP
        var parameters = new Dictionary<string, string>
        {
            { "token", objCpWhatsapUrlSendMessage!.Value },
            { "to", phoneDestino },
            { "body", message }
        };

        // Realizar la solicitud HTTP
        var httpClient = new HttpClient();
        var response =
            await httpClient.PostAsync(objCpWhatsapUrlSendMessage.Value, new FormUrlEncodedContent(parameters));

        // Leer la respuesta
        var responseContent = await response.Content.ReadAsStringAsync();

        return responseContent;
    }

    public async Task<string> SendMessageTypeImageUltramsg(int companyId, string message, string title,
        string phoneDestino = "")
    {
        var parameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<IParameterModel>();
        var companyParameterModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICompanyParameterModel>();
        // Obtener los parámetros necesarios
        var objPWhatsapPropertyNumber = parameterModel.GetRowByName("WHATSAP_CURRENT_PROPIETARY_COMMERSE");
        var objPWhatsapPropertyNumberId = objPWhatsapPropertyNumber!.ParameterId;
        var objCpWhatsapPropertyNumber =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapPropertyNumberId);

        var objPWhatsapToken = parameterModel.GetRowByName("WHATSAP_TOCKEN");
        var objPWhatsapTokenId = objPWhatsapToken!.ParameterId;
        var objCpWhatsapToken =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapTokenId);

        var objPWhatsapUrlSession = parameterModel.GetRowByName("WHATSAP_URL_REQUEST_SESSION");
        var objPWhatsapUrlSessionId = objPWhatsapUrlSession!.ParameterId;
        var objCpWhatsapUrlSession =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapUrlSessionId);

        var objPWhatsapUrlSendMessage = parameterModel.GetRowByName("WAHTSAP_URL_ENVIO_MENSAJE");
        var objPWhatsapUrlSendMessageId = objPWhatsapUrlSendMessage!.ParameterId;
        var objCpWhatsapUrlSendMessage =
            companyParameterModel.GetRowByParameterIdCompanyId(companyId, objPWhatsapUrlSendMessageId);

        // Establecer el número de teléfono de destino
        phoneDestino = !string.IsNullOrEmpty(phoneDestino) ? objCpWhatsapPropertyNumber!.Value : phoneDestino;

        // Construir los parámetros para la solicitud HTTP
        var parameters = new Dictionary<string, string>
        {
            { "token", objCpWhatsapToken!.Value },
            { "to", phoneDestino },
            { "image", message },
            { "caption", title }
        };

        // Realizar la solicitud HTTP
        var httpClient = new HttpClient();
        var response =
            await httpClient.PostAsync(objCpWhatsapUrlSendMessage!.Value, new FormUrlEncodedContent(parameters));
        // Leer la respuesta
        return await response.Content.ReadAsStringAsync();
    }
}