namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebInventory
{
    void CalculateKardexNewInput(int companyId, int transactionId, int transactionMasterId);
    
    void CalculateKardexNewOutput(int companyId, int transactionId, int transactionMasterId);
}