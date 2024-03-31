using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebFinancialAmort
{
    void Amort(decimal? amount = decimal.Zero, int rate = 0, int numberPay = 0, int periodPay = 0,
        DateTime? firstDate = null, string typeAmortization = "", string objCatalogItemsDiasNoCobrables = "",
        string objCatalogItemsDiasFeridos365 = "", string objCatalogItemsDiasFeridos366 = "");
    
    decimal GetPmtValueAleman(decimal pv,int n,int i);
    
    int GetBaseRatio(int periodPay);
    
    bool FechaEsFeriada(DateTime fecha);
    
    bool GetNextDate(DateTime date,int periodPay);
    
    decimal GetPmtValueFrances(decimal pv,int n,int i);

    SumaryDto GetTable();
    
    Dictionary<SumaryDto, List<DetailDto>> GetTableSimpleNotEmplementable();
}