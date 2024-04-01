using System.Numerics;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

class CoreWebFinancialAmort : ICoreWebFinancialAmort
{
    private decimal? Amount { get; set; }
    private decimal Rate { get; set; }
    private int NumberPay { get; set; }
    private int PeriodPay { get; set; }
    private int? TypeAmortization { get; set; }
    private DateTime? FirstDate { get; set; }

    private List<TbCatalogItem>? ObjCatalogItemsDiasNoCobrables { get; set; }
    private List<TbCatalogItem>? ObjCatalogItemsDiasFeridos365 { get; set; }
    private List<TbCatalogItem>? ObjCatalogItemsDiasFeridos366 { get; set; }

    public void Amort(decimal? amount = 0M, int rate = 0, int numberPay = 0, int periodPay = 0,
        DateTime? firstDate = null, int typeAmortization = 0,
        List<TbCatalogItem>? objCatalogItemsDiasNoCobrables = null,
        List<TbCatalogItem>? objCatalogItemsDiasFeridos365 = null,
        List<TbCatalogItem>? objCatalogItemsDiasFeridos366 = null)
    {
        Amount = amount;
        Rate = rate;
        NumberPay = numberPay;
        PeriodPay = periodPay;
        TypeAmortization = typeAmortization;
        FirstDate = firstDate;

        ObjCatalogItemsDiasNoCobrables = objCatalogItemsDiasNoCobrables;
        ObjCatalogItemsDiasFeridos365 = objCatalogItemsDiasFeridos365;
        ObjCatalogItemsDiasFeridos366 = objCatalogItemsDiasFeridos366;
    }

    public decimal GetPmtValueAleman(decimal? pv, int n, decimal i)
    {
        return (decimal)((pv * i) / (decimal)(1 - (Math.Pow((double)(1 - i), n))))!;
    }

    public int GetBaseRatio(int periodPay)
    {
        return periodPay switch
        {
            7 => 52,
            15 => 24,
            30 => 12,
            1 => 365,
            45 => 8,
            _ => 0
        };
    }

    public bool FechaEsFeriada(DateTime fecha)
    {
        //fecha = new DateTime(fecha.Year, fecha.Month, fecha.Day);

        var diaSemana = (int)fecha.DayOfWeek;
        var diaAno = fecha.DayOfYear;
        var diasTotalesDelAno = DateTime.IsLeapYear(fecha.Year) ? 366 : 365;
        if (ObjCatalogItemsDiasNoCobrables is not null)
        {
            if (ObjCatalogItemsDiasNoCobrables.Any(catalogItem => catalogItem.Sequence == diaSemana))
            {
                return true;
            }
        }


        if (diasTotalesDelAno == 365)
        {
            if (ObjCatalogItemsDiasFeridos365 is not null)
            {
                if (ObjCatalogItemsDiasFeridos365.Any(catalogItem => catalogItem.Sequence == diaAno))
                {
                    return true;
                }
            }
        }
        else
        {

            if (ObjCatalogItemsDiasFeridos366 is not null)
            {
                if (ObjCatalogItemsDiasFeridos366.Any(catalogItem => catalogItem.Sequence == diaAno))
                {
                    return true;
                }

            }
        }
        


        return false;

        

       
    }

    public DateTime? GetNextDate(DateTime? date, int periodPay)
    {
        DateTime fechaReturn;

        switch (periodPay)
        {
            case 7:
            {
                fechaReturn = date!.Value.AddDays(7);
                if (FechaEsFeriada(fechaReturn))
                {
                    fechaReturn = fechaReturn.AddDays(1);
                }

                return fechaReturn;
            }
            case 15:
            {
                fechaReturn = date!.Value.AddDays(15);
                if (FechaEsFeriada(fechaReturn))
                {
                    fechaReturn = fechaReturn.AddDays(1);
                }

                return fechaReturn;
            }
            case 30:
            {
                fechaReturn = date!.Value.AddMonths(1);
                if (FechaEsFeriada(fechaReturn))
                {
                    fechaReturn = fechaReturn.AddDays(1);
                }

                return fechaReturn;
            }
            case 1:
            {
                fechaReturn = date!.Value.AddDays(1);
                for (var i = 0; i <= 10; i++)
                {
                    if (FechaEsFeriada(fechaReturn))
                    {
                        fechaReturn = fechaReturn.AddDays(1);
                    }
                    else
                    {
                        break;
                    }
                }

                return fechaReturn;
            }
            case 45:
            {
                fechaReturn = date!.Value.AddMonths(1).AddDays(15);
                if (FechaEsFeriada(fechaReturn))
                {
                    fechaReturn = fechaReturn.AddDays(1);
                }

                return fechaReturn;
            }
            default:
                return date;
        }
    }

    /// <summary>
    /// Esta función toma el monto del préstamo pv, el número de períodos n, y la tasa de interés por período i, y devuelve el pago mensual.
    /// </summary>
    /// <param name="pv">PV es el valor presente (el monto del préstamo),</param>
    /// <param name="n">n es el número de períodos (meses)</param>
    /// <param name="i">i es la tasa de interés por período</param>
    /// <returns>Decimal</returns>
    public decimal GetPmtValueFrances(decimal pv, int n, decimal i)
    {
        return (pv * i * ((decimal)(Math.Pow((double)(1 + i), n))) / ((decimal)(Math.Pow((double)(1 + i), n) - 1)));
    }

    /// <summary>
    /// Esta función toma el monto del préstamo pv, el número de períodos n, y la tasa de interés por período i,
    /// y devuelve el pago mensual redondeado a 2 decimales utilizando el método de redondeo AwayFromZero
    /// para que el resultado sea el más cercano al valor exacto.
    /// </summary>
    /// <param name="pv">PV es el valor presente (el monto del préstamo),</param>
    /// <param name="n">n es el número de períodos (meses)</param>
    /// <param name="i">i es la tasa de interés por período</param>
    /// <returns>Decimal</returns>
    public decimal GetPmtValueSimple(decimal pv, int n, decimal i)
    {
        return Math.Round((pv + (pv * i * n)) / n, 2, MidpointRounding.AwayFromZero);
    }

    public SumaryDto GetTable()
    {
        return TypeAmortization switch
        {
            194 => GetTableFrances(),
            195 => GetTableAleman(),
            196 => GetTableAmericano(),
            463 => GetTableSimple(),
            544 => GetTableSimpleNotEmplementable(),
            _ => GetTableConstante()
        };
    }

    public SumaryDto GetTableSimpleNotEmplementable()
    {
        var capitalDesembolsado = Amount;
        var numeroDePagos = NumberPay;
        var montoTotalInteres = Math.Round((decimal)((Rate / 100) * Amount)!, 2);
        var montoTotalApagar = montoTotalInteres + capitalDesembolsado;
        var montoPorCuota = Math.Round((decimal)(montoTotalApagar / NumberPay)!, 2);
        var listDetailDto = new List<DetailDto>();
        var result = new SumaryDto(montoPorCuota, montoTotalInteres, montoTotalApagar!.Value, 0, listDetailDto);
        
        var balanceInicial = capitalDesembolsado;
        var balance = capitalDesembolsado;
        var nextDate = FirstDate;

        for (var i = 1; i <= numeroDePagos; i++)
        {
            var amortization = Math.Round((decimal)(capitalDesembolsado / numeroDePagos)!, 2);
            var interest = Math.Round(montoTotalInteres / numeroDePagos, 2);
            var payment = amortization + interest;
            balance -= amortization;

            if (i == numeroDePagos)
            {
                interest += balance!.Value;
                payment += balance.Value;
                balance = decimal.Zero;
            }

            var paymentDetail = new DetailDto(i, nextDate, amortization, interest, payment,
                balance!.Value, balanceInicial!.Value, 0);
            nextDate = GetNextDate(nextDate!.Value, PeriodPay);
            listDetailDto.Add(paymentDetail);

            balanceInicial -= amortization;
        }

        return result;
    }

    public SumaryDto GetTableSimple()
    {
        var pv = Amount;
        var n = NumberPay;
        var i = (Rate / GetBaseRatio(PeriodPay)) / 100;
        var pmt = GetPmtValueSimple(pv!.Value, n, i);
        var listaDetailDto = new List<DetailDto>();
        var result = new SumaryDto(pmt, (decimal)((pmt * n) - pv)!, pmt * n, 0, listaDetailDto);
        var amount = Amount;
        var numpay = NumberPay;        
        var monthly = Rate;
        var payment = pmt;
        
        var balance = amount;
        var nextDate = FirstDate;
        for (var jIndex = 1; jIndex <= numpay; jIndex++)
        {
            var newInterest = monthly * amount;
            var amort = payment - newInterest;
            balance = balance - amort;
            var balanceInicial = balance + amort;
            var detailDto = new DetailDto(jIndex, nextDate, amort, newInterest, payment, balance, balanceInicial, 0);
            listaDetailDto.Add(detailDto);
            nextDate = GetNextDate(nextDate, PeriodPay);
        }

        return result;
    }

    public SumaryDto GetTableFrances()
    {
        var pv = Amount;
        var n = NumberPay;
        var i = (Rate / GetBaseRatio(PeriodPay)) / 100;
        var pmt = GetPmtValueFrances(pv!.Value, n, i);
        var listaDetailDto = new List<DetailDto>();
        var result = new SumaryDto(pmt, (decimal)((pmt * n) - pv)!, (pmt * n), 0, listaDetailDto);
        var amount = Amount;
        var numPay = NumberPay;
        var rate = (Rate / GetBaseRatio(PeriodPay)) / 100;        
        var monthly = rate;
        var payment = (((amount * monthly)) / ((decimal?)(1 - Math.Pow((double)(1 + monthly), -numPay))));
        var total = payment * numPay;
        var interest = total - amount;
        var balance = amount;
        var nextDate = FirstDate;
        for (var index = 1; index <= numPay; index++)
        {
            var newInterest = monthly * balance;
            var amort = payment - newInterest;
            balance = balance - amort;
            var balanceInicial = balance + amort;
            var detailDto = new DetailDto(index, nextDate, amort, newInterest, payment, balance, balanceInicial, 0);
            nextDate = GetNextDate(nextDate, PeriodPay);
            listaDetailDto.Add(detailDto);
        }

        return result;
    }

    public SumaryDto GetTableAleman()
    {
        var pv = Amount;
        var n = NumberPay;
        var i = (Rate / GetBaseRatio(PeriodPay)) / 100;
        var pmt = GetPmtValueAleman(pv, n, i);
        var interest = pv * i;
        var listaDetailDto = new List<DetailDto>();
        var result = new SumaryDto(pmt, ((decimal)((pmt * n) + interest)!) - pv, ((decimal)((pmt * n) + interest)!), interest, listaDetailDto);
        var amount = Amount;
        var numPay = NumberPay;
        var rate = (Rate / GetBaseRatio(PeriodPay)) / 100;        
        var monthly = rate;

        var initInterest = monthly * amount;
        var initParcela = initInterest;
        var s = (1 - monthly);
        var payment = (amount * monthly) / (decimal?)(1 - (Math.Pow((double)s, numPay)));
        decimal amort = 0;
        var principal = payment;
        var saldo = amount;
        n = numPay;
        var total = payment * numPay;
        interest = total - amount;
        var nextDate = FirstDate;
        i = 1;
        

        var firstDetailDto = new DetailDto((int)i-1, nextDate, initInterest, initParcela, 0, amount, amount, 0);
        listaDetailDto.Add(firstDetailDto);
        for (var index = 1; index <= numPay; index++)
        {
            nextDate = GetNextDate(nextDate, PeriodPay);
            amort = (decimal)(payment * (decimal?)(Math.Pow((double)s, (double)(n - i))))!;
            saldo = saldo - amort;
            var newInterest = monthly * saldo;
            var newpayment = amount;               
            var saldoInicial = saldo + amort;

            if (index == numPay)
            {
                newInterest = 0;                
            }

            var detailDto = new DetailDto(index, nextDate, amort, newInterest, payment, saldo, saldoInicial, 0);
            listaDetailDto.Add(detailDto);
        }

        return result;
    }

    public SumaryDto GetTableAmericano()
    {
        var pv = Amount;
        var n = NumberPay;
        var i = (Rate / GetBaseRatio(PeriodPay)) / 100;
        var interest = n * pv * i;
        var listaDetailDto = new List<DetailDto>();
        var result = new SumaryDto(0, interest, (pv + interest), 0, listaDetailDto);
        var amount = Amount;
        var numpay = NumberPay;
        var rate = (Rate / GetBaseRatio(PeriodPay));
        rate = rate / 100;
        var monthly = rate;
        decimal? payment = 0;
        var amort = 0;
        var saldo = amount;
        n = numpay;
        var total = payment * numpay;
        interest = total - amount;
        saldo = amount;
        var nextDate = FirstDate;
        for (var index = 1; index <= numpay; index++)
        {
            var newInterest = monthly * amount;
            payment = newInterest;

            if(index == numpay)
            {
                saldo = 0;                
                payment = amount + (monthly * amount);
            }

            var saldoInicial = saldo + amort;
            var detailDto = new DetailDto(index, nextDate, amort, newInterest, payment, saldo, saldoInicial, 0);
            listaDetailDto.Add(detailDto);
            nextDate = GetNextDate(nextDate, PeriodPay);
        }

        return result;
    }

    public SumaryDto GetTableConstante()
    {
        var pv = Amount;
        var n = NumberPay;
        var i = (Rate / GetBaseRatio(PeriodPay)) / 100;
        var p = pv / n;
        var saldo = pv + p;
        decimal? npv = 0;
        decimal? newInterest = 0;
        decimal? totpay = 0;

        for (var t = 1; t <= n; t++)
        {
            npv = saldo - p;
            newInterest = newInterest + (i * npv);
            saldo = npv;
            totpay = totpay + npv;
        }


        var totint = newInterest; //total de intereses
        totpay = pv + totint; //total de pago
        var listaDetailDto = new List<DetailDto>();
        var result = new SumaryDto(0, totint, totpay, 0, listaDetailDto);


        var amount = Amount;
        var numpay = NumberPay;
        var rate = (Rate / GetBaseRatio(PeriodPay))/100;
        var monthly = rate;
        var bases = (1 - Math.Pow((double)(1 + rate), -numpay));
        var payment = bases == 0 ? (amount / numpay) : ((amount * rate) / (decimal?)bases);
        var total = payment * numpay;
        var interest = total - amount;
        saldo = amount;
        totint = 0;
        var nextDate = FirstDate;
        decimal cuotaAcumulada = 0;


        for (var index = 1; index <= numpay; index++)
        {
            newInterest = Math.Round(Math.Round(rate, 2) * Math.Round(saldo!.Value, 2), 2);
            var principal = Math.Round(Math.Round(amount!.Value, 2) / numpay, 2);
            var parcela = principal + newInterest;
            saldo = Math.Round(saldo!.Value - principal, 2);
            var saldoInicial = saldo + principal;
            cuotaAcumulada = cuotaAcumulada + principal;

            DetailDto detailDto;
            if (index == numpay)
            {
                var diferencia = Math.Round((decimal)(cuotaAcumulada - amount), 2);
                principal = Math.Round(principal - diferencia, 2); //principal
                parcela = Math.Round(parcela!.Value - diferencia, 2); //cuota
                detailDto = new DetailDto(index, nextDate, principal, newInterest, parcela, 0, saldoInicial, 0);
            }
            else
            {
                detailDto = new DetailDto(index, nextDate, principal, newInterest, parcela, saldo, saldoInicial, 0);
            }

            listaDetailDto.Add(detailDto);
            nextDate = GetNextDate(nextDate, PeriodPay);
        }
        return result;
    }
}