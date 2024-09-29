using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionCausalModel(DataContext context) : ITransactionCausalModel
{
    public List<TbTransactionCausal> GetCausalByBranch(int companyId, int transactionId, int branchId)
    {
        
        return context.TbTransactionCausals
            .Where(causal => causal.CompanyID == companyId
                             && causal.TransactionID == transactionId
                             && causal.BranchID == branchId
                             && causal.IsActive )
            .ToList();
    }

    public TbTransactionCausal? GetCausalDefaultId(int companyId, int transactionId)
    {
        
        return context.TbTransactionCausals
            .SingleOrDefault(causal => causal!.CompanyID == companyId
                              && causal.TransactionID == transactionId
                              && causal.IsActive  && !causal.IsDefault );
    }

    public List<TbTransactionCausalDto> GetByCompanyAndTransaction(int companyId, int transactionId)
    {
        
        var result = from tc in context.TbTransactionCausals
            join b in context.TbBranches on tc.BranchID!.Value equals b.BranchID
            join w in context.TbWarehouses on tc.WarehouseSourceID!.Value equals w.WarehouseID into wJoin
            from w in wJoin.DefaultIfEmpty()
            join w2 in context.TbWarehouses on tc.WarehouseTargetID!.Value equals w2.WarehouseID into w2Join
            from w2 in w2Join.DefaultIfEmpty()
            where tc.CompanyID == companyId
                  && tc.TransactionID == transactionId
                  && tc.IsActive
            select new TbTransactionCausalDto
            {
                CompanyId = tc.CompanyID,
                TransactionId = tc.TransactionID,
                TransactionCausalId = tc.TransactionCausalID,
                BranchId = tc.BranchID!.Value,
                Name = tc.Name,
                WarehouseSourceId = tc.WarehouseSourceID!.Value,
                WarehouseTargetId = tc.WarehouseTargetID!.Value,
                IsDefault = tc.IsDefault,
                IsActive = tc.IsActive,
                Branch = b.Name,
                WarehouseSourceDescription = w.Name,
                WarehouseTargetDescription = w2.Name
            };
        return result.ToList();
    }

    public TbTransactionCausal? GetByCompanyAndTransactionAndCausal(int companyId, int transactionId, int causalId)
    {
        
        return context.TbTransactionCausals
            .SingleOrDefault(causal => causal.CompanyID == companyId
                              && causal.TransactionID == transactionId
                              && causal.TransactionCausalID == causalId
                              && causal.IsActive);
    }

    public void DeleteAppPosme(int companyId, int transactionId, List<int> listCausal)
    {
        
        context.TbTransactionCausals
            .Where(causal => causal.CompanyID == companyId
                             && causal.TransactionID == transactionId
                             && !listCausal.Contains(causal.TransactionCausalID))
            .ExecuteUpdate(calls => calls.SetProperty(causal => causal.IsActive, false));
    }

    public int InsertAppPosme(TbTransactionCausal data)
    {
        
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionCausalID;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int causalId, TbTransactionCausal data)
    {
        
        var find = context.TbTransactionCausals
            .Single(causal => causal.CompanyID == companyId
                              && causal.TransactionID == transactionId
                              && causal.TransactionCausalID == causalId);
        data.TransactionCausalID = find.TransactionCausalID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int CountCausalDefault(int companyId, int transactionId)
    {
        
        return context.TbTransactionCausals
            .Count(causal => causal.CompanyID == companyId
                             && causal.TransactionID == transactionId
                             && causal.IsActive
                             && causal.IsDefault);
    }
}