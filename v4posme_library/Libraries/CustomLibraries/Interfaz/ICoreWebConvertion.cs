namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebConvertion
{
    decimal Convert(int companyId,decimal quantity,int catalogId,int fromCatalogItemId,int toCatalogItemId);
}