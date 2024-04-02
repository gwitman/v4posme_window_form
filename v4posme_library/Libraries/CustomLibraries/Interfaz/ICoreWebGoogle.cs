namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebGoogle
{
    string GetRequestPermissionPosme(string state);
    Task<string> GetRequestTokenPosme(string code);

    Task<string> SetEventPosme(string accessToken, string titulo, string descripcion, string date, string hora,
        string horaFin);

    Task<string> RemoveEventPosme(string accessToken, string eventId);
}