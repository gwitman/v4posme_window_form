using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class TransactionModel(DataContext context) : ITransactionModel
{
    public TbTransaction? GetRowByPk(int companyId, string name)
    {
        
        return context.TbTransactions.AsNoTracking()
            .FirstOrDefault(transaction => transaction!.CompanyId == companyId
                                  && transaction.Name == name
                                  && transaction.IsActive!.Value);
    }

    public int GetCounterTransactionMaster(int companyId, int transactionId, int statusId)
    {
        
        var query = from tb in context.TbTransactions.AsNoTracking()
            join tm in context.TbTransactionMasters.AsNoTracking() on tb.TransactionId equals tm.TransactionId
            where tm.IsActive!.Value && tm.CompanyId == companyId && tm.TransactionId == transactionId &&
                  tm.StatusId == statusId
            select new { tm, tb };
        return query.Count();
    }

    public int GetCountInput(int companyId)
    {
        
        var query = from tb in context.TbTransactions.AsNoTracking()
            join tm in context.TbTransactionMasters.AsNoTracking() on tb.TransactionId equals tm.TransactionId
            where tm.IsActive!.Value && tm.CompanyId == companyId && tb.SignInventory == 1
            select new { tm, tb };
        return query.Count();
    }

    public int GetCountOutput(int companyId)
    {
        
        var query = from tb in context.TbTransactions.AsNoTracking()
            join tm in context.TbTransactionMasters.AsNoTracking() on tb.TransactionId equals tm.TransactionId
            where tm.IsActive!.Value && tm.CompanyId == companyId && tb.SignInventory == -1
            select new { tm, tb };
        return query.Count();
    }

    public TbTransaction? GetByCompanyAndTransaction(int companyId, int transactionId)
    {
        
        return context.TbTransactions
            .First(transaction => transaction.CompanyId == companyId
                                  && transaction.TransactionId == transactionId
                                  && transaction.IsActive!.Value);
    }

    public List<TbTransaction?> GetTransactionContabilizable(int companyId)
    {
        
        return context.TbTransactions
            .Where(transaction => transaction.CompanyId == companyId
                                  && transaction.IsCountable!.Value
                                  && transaction.IsActive!.Value)
            .ToList();
    }

    public void UpdateAppPosme(int companyId, int transactionId, TbTransaction data)
    {
        
        var find = context.TbTransactions
            .FirstOrDefault(transaction => transaction.CompanyId == companyId
                                           && transaction.TransactionId == transactionId);
        if(find is null) return;
        data.TransactionId = transactionId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }
}