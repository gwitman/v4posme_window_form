using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionCausalModel(DataContext context) : ITransactionCausalModel
{
    public List<TbTransactionCausal> GetCausalByBranch(int companyId, int transactionId, int branchId)
    {
        
        return context.TbTransactionCausals
            .Where(causal => causal.CompanyId == companyId
                             && causal.TransactionId == transactionId
                             && causal.BranchId == branchId
                             && causal.IsActive!.Value )
            .ToList();
    }

    public TbTransactionCausal? GetCausalDefaultId(int companyId, int transactionId)
    {
        
        return context.TbTransactionCausals
            .SingleOrDefault(causal => causal!.CompanyId == companyId
                              && causal.TransactionId == transactionId
                              && causal.IsActive!.Value  && !causal.IsDefault!.Value );
    }

    public List<TbTransactionCausalDto> GetByCompanyAndTransaction(int companyId, int transactionId)
    {
        
        var result = from tc in context.TbTransactionCausals
            join b in context.TbBranches on tc.BranchId!.Value equals b.BranchId
            join w in context.TbWarehouses on tc.WarehouseSourceId!.Value equals w.WarehouseId into wJoin
            from w in wJoin.DefaultIfEmpty()
            join w2 in context.TbWarehouses on tc.WarehouseTargetId!.Value equals w2.WarehouseId into w2Join
            from w2 in w2Join.DefaultIfEmpty()
            where tc.CompanyId == companyId
                  && tc.TransactionId == transactionId
                  && tc.IsActive!.Value
            select new TbTransactionCausalDto
            {
                CompanyId = tc.CompanyId,
                TransactionId = tc.TransactionId,
                TransactionCausalId = tc.TransactionCausalId,
                BranchId = tc.BranchId!.Value,
                Name = tc.Name,
                WarehouseSourceId = tc.WarehouseSourceId!.Value,
                WarehouseTargetId = tc.WarehouseTargetId!.Value,
                IsDefault = tc.IsDefault!.Value,
                IsActive = tc.IsActive!.Value,
                Branch = b.Name,
                WarehouseSourceDescription = w.Name,
                WarehouseTargetDescription = w2.Name
            };
        return result.ToList();
    }

    public TbTransactionCausal? GetByCompanyAndTransactionAndCausal(int companyId, int transactionId, int causalId)
    {
        
        return context.TbTransactionCausals
            .Single(causal => causal.CompanyId == companyId
                              && causal.TransactionId == transactionId
                              && causal.TransactionCausalId == causalId
                              && causal.IsActive!.Value);
    }

    public void DeleteAppPosme(int companyId, int transactionId, List<int> listCausal)
    {
        
        context.TbTransactionCausals
            .Where(causal => causal.CompanyId == companyId
                             && causal.TransactionId == transactionId
                             && !listCausal.Contains(causal.TransactionCausalId))
            .ExecuteUpdate(calls => calls.SetProperty(causal => causal.IsActive!.Value, false));
    }

    public int InsertAppPosme(TbTransactionCausal data)
    {
        
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionCausalId;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int causalId, TbTransactionCausal data)
    {
        
        var find = context.TbTransactionCausals
            .Single(causal => causal.CompanyId == companyId
                              && causal.TransactionId == transactionId
                              && causal.TransactionCausalId == causalId);
        data.TransactionCausalId = find.TransactionCausalId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int CountCausalDefault(int companyId, int transactionId)
    {
        
        return context.TbTransactionCausals
            .Count(causal => causal.CompanyId == companyId
                             && causal.TransactionId == transactionId
                             && causal.IsActive!.Value
                             && causal.IsDefault!.Value);
    }
}