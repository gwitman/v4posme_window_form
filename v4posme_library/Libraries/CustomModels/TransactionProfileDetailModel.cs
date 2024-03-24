using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TransactionProfileDetailModel : ITransactionProfileDetailModel
{
    public List<TbTransactionProfileDetail> GetByCompanyAndTransactionAndCausal(int companyId, int transactionId,
        int causalId)
    {
        using var context = new DataContext();
        var result = from tc in context.TbTransactionProfileDetails
            join tp in context.TbTransactionConcepts on new { tc.ConceptId, tc.TransactionId }
                equals new { tp.ConceptId, tp.TransactionId }
            join a in context.TbAccounts on new { AccountId = Convert.ToInt32(tc.AccountId), tc.CompanyId }
                equals new { a.AccountId, a.CompanyId }
            join cc in context.TbCenterCosts on new { ClassId = Convert.ToInt32(tc.ClassId), tc.CompanyId }
                equals new { cc.ClassId, cc.CompanyId } into ccJoin
            from cc in ccJoin.DefaultIfEmpty()
            where tc.CompanyId == companyId
                  && tc.TransactionId == transactionId
                  && tc.TransactionCausalId == causalId
            select new TbTransactionProfileDetail
            {
                CompanyId = tc.CompanyId,
                TransactionId = tc.TransactionId,
                TransactionCausalId = tc.TransactionCausalId,
                ProfileDetailId = tc.ProfileDetailId,
                ConceptId = tc.ConceptId,
                AccountId = tc.AccountId,
                ClassId = tc.ClassId,
                Sign = tc.Sign,
                ConceptDescription = tp.Name,
                AccountDescription = a.AccountNumber,
                CenterCostDescription = cc.Number
            };
        return result.ToList();
    }

    public TbTransactionProfileDetail GetByCompanyAndTransactionAndCausalAndProfileDetailId(int companyId,
        int transactionId,
        int causalId, int profileDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionProfileDetails
            .First(detail => detail.CompanyId == companyId
                             && detail.TransactionId == transactionId
                             && detail.TransactionCausalId == causalId
                             && detail.ProfileDetailId == profileDetailId);
    }

    public int DeleteAppPosme(int companyId, int transactionId, int causalId, int profileDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionProfileDetails
            .Where(detail => detail.CompanyId == companyId
                             && detail.TransactionId == transactionId
                             && detail.TransactionCausalId == causalId
                             && detail.ProfileDetailId == profileDetailId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbTransactionProfileDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ProfileDetailId;
    }
}