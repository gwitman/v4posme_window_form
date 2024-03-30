namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebCounter
{
    string GetFillNumber(int companyId, int branchId, string componentName, int componentItemId, int number);
    string GetCurrenctNumber(int companyId, int branchId, string componentName, int componentItemId);
    string GoNextNumber(int companyId, int branchId, string componentName, int componentItemId);
}