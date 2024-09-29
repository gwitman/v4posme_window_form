using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels.Core;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebConvertion(ICatalogItemConvertionModel catalogItemConvertionModel) : ICoreWebConvertion
{
    public decimal Convert(int companyId, decimal quantity, int catalogId, int fromCatalogItemId, int toCatalogItemId)
    {
        var objConvertionDefault = catalogItemConvertionModel.GetDefault(companyId, catalogId);
        if (objConvertionDefault is null)
            throw new Exception("NO EXISTE EL CATALOGITEM DEFAULT EN EL CATALOGO");

        var objConvertionSource = catalogItemConvertionModel.GetRowByPk(companyId, catalogId, fromCatalogItemId,
            objConvertionDefault.CatalogItemID);
        if (objConvertionSource is null)
            throw new Exception("NO EXISTE EL CATALOGITEM-SOURCE --> DEFAULT");

        var objConvertionTarget = catalogItemConvertionModel.GetRowByPk(companyId, catalogId, toCatalogItemId,
            objConvertionDefault.CatalogItemID);
        if (objConvertionTarget is null)
            throw new Exception("NO EXISTE EL CATALOGITEM-TARGET --> DEFAULT");

        if (objConvertionSource.CatalogItemID == objConvertionTarget.CatalogItemID)
            return quantity;

        decimal result;

        // De Menor al Default
        if (objConvertionTarget.CatalogItemID == objConvertionDefault.CatalogItemID && objConvertionSource.Ratio > 0)
        {
            result = quantity / objConvertionSource.Ratio!.Value;
        }
        // De Mayor al Default
        else if (objConvertionTarget.CatalogItemID == objConvertionDefault.CatalogItemID &&
                 objConvertionSource.Ratio < 0)
        {
            result = quantity * objConvertionSource.Ratio!.Value;
        }
        // De Menor a mayor
        else if (objConvertionSource.Ratio > objConvertionTarget.Ratio)
        {
            result = quantity * objConvertionTarget.Ratio!.Value / objConvertionSource.Ratio!.Value;
        }
        // De Mayor a Menor
        else
        {
            result = quantity * objConvertionTarget.Ratio!.Value / objConvertionSource.Ratio!.Value;
        }

        return result;
    }
}