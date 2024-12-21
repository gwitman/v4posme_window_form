using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterDenominationModel : ITransactionMasterDenominationModel
{
    public int DeleteAppPosme(int transactionMasterId, DataContext? dataContext=null)
    {
        if (dataContext==null)
        {
            using var context = new DataContext();
            return DeleteAppPosme(transactionMasterId, context);
        }
        
        var datos = dataContext.TbTransactionMasterDenominations
            .Where(denomination => denomination.TransactionMasterID == transactionMasterId)
            .ToList();
        dataContext.RemoveRange(datos);
        return dataContext.SaveChanges();
    }

    public int InsertAppPosme(TbTransactionMasterDenomination data, DataContext? dataContext = null)
    {
        if (dataContext==null)
        {
            using var context = new DataContext();
            return InsertAppPosme(data, context);
        }
        var add = dataContext.Add(data);
        dataContext.SaveChanges();
        return add.Entity.TransactionMasterDenominationID;
    }

    public void UpdateAppPosme(int transactionMasterDenominationId, TbTransactionMasterDenomination data)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterDenominations
            .Find(transactionMasterDenominationId);
        if (find is null) return;
        data.TransactionMasterDenominationID = transactionMasterDenominationId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public List<TbTransactionMasterDenominationDto> GetRowByTransactionMaster(int companyId, int transactionId,
        int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from i in context.TbTransactionMasterDenominations
            join ci in context.TbCatalogItems on i.CatalogItemID equals ci.CatalogItemID
            where i.IsActive == 1 && i.CompanyID == companyId
                                  && i.TransactionID == transactionId
                                  && i.TransactionMasterID == transactionMasterId
            orderby ci.Sequence
            select new TbTransactionMasterDenominationDto
            {
                CompanyId = i.CompanyID,
                CatalogItemId = i.CatalogItemID,
                CurrencyId = i.CurrencyID,
                Quantity = i.Quantity,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                IsActive = i.IsActive,
                TransactionId = i.TransactionID,
                TransactionMasterDenominationId = i.TransactionMasterDenominationID,
                TransactionMasterId = i.TransactionMasterID,
                ComponentId = i.ComponentID,
                ExchangeRate = i.ExchangeRate,
                Ratio = i.Ratio,
                DenominationName = ci.Name
            };
        return result.ToList();
    }

    public TbTransactionMasterDenominationDto GetRowByPk(int transactionMasterDenominationId)
    {
        using var context = new DataContext();
        var result = from i in context.TbTransactionMasterDenominations
            join ci in context.TbCatalogItems on i.CatalogItemID equals ci.CatalogItemID
            where i.IsActive == 1
                  && i.TransactionMasterDenominationID == transactionMasterDenominationId
            orderby ci.Sequence
            select new TbTransactionMasterDenominationDto
            {
                CompanyId = i.CompanyID,
                CatalogItemId = i.CatalogItemID,
                CurrencyId = i.CurrencyID,
                Quantity = i.Quantity,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                IsActive = i.IsActive,
                TransactionId = i.TransactionID,
                TransactionMasterDenominationId = i.TransactionMasterDenominationID,
                TransactionMasterId = i.TransactionMasterID,
                ComponentId = i.ComponentID,
                ExchangeRate = i.ExchangeRate,
                Ratio = i.Ratio,
                DenominationName = ci.Name
            };
        return result.Single();
    }
}