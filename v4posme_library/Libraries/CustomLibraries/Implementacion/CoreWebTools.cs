using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Net;
using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebTools : ICoreWebTools
{
    private static readonly string? Path = VariablesGlobales.ConfigurationBuilder["PATH_LOG_DB"];
    /// <summary>
    /// Esta función recibe un objeto message y devuelve una cadena formateada.
    /// Si message es un array, concatena todos los elementos del array en una sola cadena.
    /// Si no es un array, elimina los saltos de línea de la cadena.
    /// </summary>
    /// <param name="message">Array|string?</param>
    /// <returns>string?</returns>
    public string? FormatMessageError(object message)
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
    public Dictionary<string?, object>? FormatParameter(string? filter)
    {
        try
        {
            filter          = filter.Replace("|", ":");
            filter          = filter.Replace("{}", ",");
            var json        = filter.StartsWith("{") && filter.EndsWith("}") ? filter : "{" + filter + "}";
            var jsonObject  = JObject.Parse(json);

            var result = new Dictionary<string?, object>();
            foreach (var item in jsonObject)
            {
                result["{" + item.Key + "}"] = item.Value!.ToString();
            }

            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public TbComponent? GetComponentIdByComponentName(string? componentName)
    {
        var componentModel = VariablesGlobales.Instance.UnityContainer.Resolve<IComponentModel>();
        return componentModel.GetRowByName(componentName);
    }

    public string? HelperSegmentsByIndex(string[] objListSegments, int i, string? variable)
    {
        var result = "";
        var index = i + 1;
        var count = objListSegments.Length;

        // Si la variable es nula o vacía y el índice está dentro del rango
        if ((string.IsNullOrEmpty(variable) || !string.IsNullOrWhiteSpace(variable)) && index < count)
        {
            result = objListSegments[index];
        }
        else
        {
            result = variable;
        }

        return result;
    }


    public void Log(string? logMessage)
    {
        CreateDirectory();
        using (StreamWriter w = File.AppendText(Path + "\\log.txt"))
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

    }
    private void CreateDirectory()
    {
        try
        {
            if (!Directory.Exists(Path)) Directory.CreateDirectory(Path);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void SendEmail(string? subject, string? body)
    {
        var emailFrom = VariablesGlobales.ConfigurationBuilder["EMAIL_APP"];
        var emailCc = VariablesGlobales.ConfigurationBuilder["EMAIL_APP_COPY"];
        var fromAddress = new MailAddress(emailFrom, "posMe");
        var toAddress = new MailAddress(emailCc, "posMe");
        var fromPassword = VariablesGlobales.ConfigurationBuilder["EMAIL_APP_PASSWORD"];

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using var message = new MailMessage(fromAddress, toAddress);
        message.Subject = subject;
        message.Body = body;
        smtp.Send(message);
    }

    public void SendEmail(string toAddress, string? subject, string? body)
    {
        var emailFrom = VariablesGlobales.ConfigurationBuilder["EMAIL_APP"];
        var fromAddress = new MailAddress(emailFrom, "posMe");
        var toMailAddress = new MailAddress(toAddress, "posMe");
        var fromPassword = VariablesGlobales.ConfigurationBuilder["EMAIL_APP_PASSWORD"];

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using var message = new MailMessage(fromAddress, toMailAddress);
        message.Subject = subject;
        message.Body = body;
        smtp.Send(message);
    }
  
}