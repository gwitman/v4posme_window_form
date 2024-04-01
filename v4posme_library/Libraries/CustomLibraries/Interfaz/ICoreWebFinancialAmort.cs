using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebFinancialAmort
{
    void Amort(decimal? amount = 0M, decimal? rate = 0, int numberPay = 0, int periodPay = 0,
        DateTime? firstDate = null, int typeAmortization = 0,
        List<TbCatalogItem>? objCatalogItemsDiasNoCobrables = null,
        List<TbCatalogItem>? objCatalogItemsDiasFeridos365 = null,
        List<TbCatalogItem>? objCatalogItemsDiasFeridos366 = null);
    
    decimal GetPmtValueAleman(decimal? pv, int n, decimal i);
    
    int GetBaseRatio(int periodPay);
    
    bool FechaEsFeriada(DateTime fecha);
    
    DateTime? GetNextDate(DateTime? date, int periodPay);

    /// <summary>
    /// Esta función toma el monto del préstamo pv, el número de períodos n, y la tasa de interés por período i, y devuelve el pago mensual.
    /// </summary>
    /// <param name="pv">PV es el valor presente (el monto del préstamo),</param>
    /// <param name="n">n es el número de períodos (meses)</param>
    /// <param name="i">i es la tasa de interés por período</param>
    /// <returns>Decimal</returns>
    decimal GetPmtValueFrances(decimal pv, int n, decimal i);

    /// <summary>
    /// Esta función toma el monto del préstamo pv, el número de períodos n, y la tasa de interés por período i,
    /// y devuelve el pago mensual redondeado a 2 decimales utilizando el método de redondeo AwayFromZero
    /// para que el resultado sea el más cercano al valor exacto.
    /// </summary>
    /// <param name="pv">PV es el valor presente (el monto del préstamo),</param>
    /// <param name="n">n es el número de períodos (meses)</param>
    /// <param name="i">i es la tasa de interés por período</param>
    /// <returns>Decimal</returns>
    decimal GetPmtValueSimple(decimal pv, int n, decimal i);
    
    TableAmortizationDto GetTable();
    
    TableAmortizationDto GetTableSimpleNotEmplementable();
    TableAmortizationDto GetTableSimple();
    TableAmortizationDto GetTableFrances();
    TableAmortizationDto GetTableAleman();
    TableAmortizationDto GetTableAmericano();
    TableAmortizationDto GetTableConstante();
}