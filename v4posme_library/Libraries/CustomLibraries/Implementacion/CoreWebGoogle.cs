using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using v4posme_library.Libraries.CustomLibraries.Interfaz;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebGoogle : ICoreWebGoogle
{
    private static readonly string? PosmeClientId = VariablesGlobales.ConfigurationBuilder["POSME_CLIENTID"];
    private static readonly string? PosmeClientSecret = VariablesGlobales.ConfigurationBuilder["POSME_CLIENTSECRET"];

    private static readonly string? PosmeUrlRedirect =
        VariablesGlobales.ConfigurationBuilder["URL_REDIRECT_CALENDAR_POSME"];

    private static readonly string? PosmeScope = VariablesGlobales.ConfigurationBuilder["POSME_SCOPE"];
    private static readonly string? PosmeAuth = VariablesGlobales.ConfigurationBuilder["POSME_AUTH"];
    private static readonly string? PosmeTokenUrl = VariablesGlobales.ConfigurationBuilder["POSME_TOKENURL"];
    private static readonly string? PosmeZoneHoariUrl = VariablesGlobales.ConfigurationBuilder["POSME_ZONEHOARIURL"];
    private static readonly string? PosmeCalendarUrl = VariablesGlobales.ConfigurationBuilder["POSME_CALENDARURL"];

    public string GetRequestPermissionPosme(string state)
    {
        var parameters = new Dictionary<string, string>
        {
            { "response_type", "code" },
            { "client_id", PosmeClientId! },
            { "redirect_uri", PosmeUrlRedirect! },
            { "scope", PosmeScope! },
            { "state", state }
        };

        var queryString = string.Join("&", parameters.Select(x => $"{x.Key}={Uri.EscapeDataString(x.Value)}"));
        var url = $"{PosmeAuth}{queryString}";

        return url;
    }

    public async Task<string> GetRequestTokenPosme(string code)
    {
        var client = new HttpClient();
        var requestParams = new Dictionary<string, string>
        {
            { "client_id", PosmeClientId! },
            { "redirect_uri", PosmeUrlRedirect! },
            { "client_secret", PosmeClientSecret! },
            { "code", code },
            { "grant_type", "authorization_code" }
        };

        var requestContent = new FormUrlEncodedContent(requestParams);
        var response = await client.PostAsync(PosmeTokenUrl, requestContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = "Failed to receive access token";
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                errorMessage = "Invalid request parameters";
            }

            throw new Exception($"Error {(int)response.StatusCode}: {errorMessage}");
        }

        var responseData = JObject.Parse(responseContent);
        return responseData["access_token"]!.ToString();
    }

    public async Task<string> SetEventPosme(string accessToken, string titulo, string descripcion, string date,
        string hora, string horaFin)
    {
        using var client = new HttpClient();
        // Obtener zona horaria
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var timezoneResponse = await client.GetAsync(PosmeZoneHoariUrl);
        timezoneResponse.EnsureSuccessStatusCode();
        var getAsync = await client.GetAsync(client.BaseAddress);
        if (!getAsync.IsSuccessStatusCode)
        {
            var httpCode = getAsync.StatusCode;
            const string errorMsg = "Failed to fetch timezone"; 
            throw new Exception($"Error {httpCode}: {errorMsg}"); 
        }
        var timezoneData = JObject.Parse(await getAsync.Content.ReadAsStringAsync());
        var userTimezone = timezoneData["value"]!.ToString();

        // Crear evento
        var eventData = new
        {
            summary = titulo,
            location = "Managua",
            description = descripcion
        };

        const bool allDay = false;
        const string calendarId = "primary";
        var eventTimezone = userTimezone;
        var apiUrl = $"{PosmeCalendarUrl}{calendarId}/events";
        
        var curlPost = new Dictionary<string, object>();
        if (!string.IsNullOrEmpty(eventData.summary))
        {
            curlPost["summary"] = eventData.summary;
        }

        if (!string.IsNullOrEmpty(eventData.location))
        {
            curlPost["location"] = eventData.location;
        }

        if (!string.IsNullOrEmpty(eventData.description))
        {
            curlPost["description"] = eventData.description;
        }
        var eventDate = string.IsNullOrEmpty(date) ? DateTime.UtcNow.ToString("yyyy-MM-dd") : date;
        var startTime = string.IsNullOrEmpty(hora) ? DateTime.UtcNow.ToString("HH:mm:ss") : hora;
        var endTime = string.IsNullOrEmpty(horaFin) ? DateTime.UtcNow.ToString("HH:mm:ss") : horaFin;
        if (allDay)
        {
            curlPost["start"] = new { date = eventDate };
            curlPost["end"] = new { date = eventDate };
        }
        else
        {
            var current = TimeZoneInfo.FindSystemTimeZoneById(eventTimezone);
            var utcTime = DateTime.UtcNow;
            var offsetInSecs = current.GetUtcOffset(utcTime).TotalSeconds;
            var hoursAndSec = TimeSpan.FromSeconds(Math.Abs(offsetInSecs)).ToString("hh:mm");
            var timezoneOffset = offsetInSecs >= 0 ? $"+{hoursAndSec}" : $"-{hoursAndSec}";

            var dateTimeStart = $"{eventDate}T{startTime}{timezoneOffset}";
            var dateTimeEnd = $"{eventDate}T{endTime}{timezoneOffset}";

            curlPost["start"] = new { dateTime = dateTimeStart, timeZone = eventTimezone };
            curlPost["end"] = new { dateTime = dateTimeEnd, timeZone = eventTimezone };
        }

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.PostAsync(apiUrl,
            new StringContent(JsonConvert.SerializeObject(curlPost), Encoding.UTF8, "application/json"));
        var responseStatusCode = response.StatusCode;
        if (!response.IsSuccessStatusCode)
        {
            const string errorMsg = "Failed to create event"; 
            throw new Exception($"Error {responseStatusCode}: {errorMsg}"); 
        }
        var responseData = JObject.Parse(await response.Content.ReadAsStringAsync());
        return $"evento agregado:{responseData["id"]}";
    }

    public async Task<string> RemoveEventPosme(string accessToken, string eventId)
    {
        var calendarId = "primary";
        var apiUrl = $"{PosmeCalendarUrl}{calendarId}/events/{eventId}";
        if (apiUrl == null) throw new ArgumentNullException(nameof(apiUrl));

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await client.DeleteAsync(apiUrl);
        if (!response.IsSuccessStatusCode)
        {
            var responseStatusCode = response.StatusCode;
            const string errorMsg = "Failed to create event"; 
            throw new Exception($"Error {responseStatusCode}: {errorMsg}"); 
        }
        return eventId;
    }
}