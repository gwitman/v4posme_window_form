using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterDenominationModel : ITransactionMasterDenominationModel
{
    public int DeleteAppPosme(int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDenominations
            .Where(denomination => denomination.TransactionMasterId == transactionMasterId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbTransactionMasterDenomination data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterDenominationId;
    }

    public void UpdateAppPosme(int transactionMasterDenominationId, TbTransactionMasterDenomination data)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterDenominations
            .Find(transactionMasterDenominationId);
        if (find is null) return;
        data.TransactionMasterDenominationId = transactionMasterDenominationId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public List<TbTransactionMasterDenomination> GetRowByTransactionMaster(int companyId, int transactionId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from i in context.TbTransactionMasterDenominations
            join ci in context.TbCatalogItems on i.CatalogItemId equals ci.CatalogItemId
            where i.IsActive == 1 && i.CompanyId == companyId
                                  && i.TransactionId == transactionId
                                  && i.TransactionMasterId == transactionMasterId
            orderby ci.Sequence
            select new TbTransactionMasterDenomination
            {
                CompanyId = i.CompanyId,
                CatalogItemId = i.CatalogItemId,
                CurrencyId = i.CurrencyId,
                Quantity = i.Quantity,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                IsActive = i.IsActive,
                TransactionId = i.TransactionId,
                TransactionMasterDenominationId = i.TransactionMasterDenominationId,
                TransactionMasterId = i.TransactionMasterId,
                ComponentId = i.ComponentId,
                ExchangeRate = i.ExchangeRate,
                Ratio = i.Ratio,
                DenominationName = ci.Name
            };
        return result.ToList();
    }

    public TbTransactionMasterDenomination GetRowByPk(int transactionMasterDenominationId)
    {
        using var context = new DataContext();
        var result = from i in context.TbTransactionMasterDenominations
            join ci in context.TbCatalogItems on i.CatalogItemId equals ci.CatalogItemId
            where i.IsActive == 1
                  && i.TransactionMasterDenominationId == transactionMasterDenominationId
            orderby ci.Sequence
            select new TbTransactionMasterDenomination
            {
                CompanyId = i.CompanyId,
                CatalogItemId = i.CatalogItemId,
                CurrencyId = i.CurrencyId,
                Quantity = i.Quantity,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                IsActive = i.IsActive,
                TransactionId = i.TransactionId,
                TransactionMasterDenominationId = i.TransactionMasterDenominationId,
                TransactionMasterId = i.TransactionMasterId,
                ComponentId = i.ComponentId,
                ExchangeRate = i.ExchangeRate,
                Ratio = i.Ratio,
                DenominationName = ci.Name
            };
        return result.Single();
    }
}