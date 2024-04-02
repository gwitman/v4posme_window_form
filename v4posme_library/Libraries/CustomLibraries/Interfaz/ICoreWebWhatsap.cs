namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebWhatsap
{
    bool ValidSendMessage(int companyId);
    public void SendMessage(int companyId, string message);
    Task<string> SendMessageAsync(int companyId, string message, string phoneDestino = "");
    Task<string> SendMessageTypeImageUltramsg(int companyId, string message, string title,
        string phoneDestino = "");
}