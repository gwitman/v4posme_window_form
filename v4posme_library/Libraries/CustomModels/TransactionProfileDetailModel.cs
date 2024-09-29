using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionProfileDetailModel : ITransactionProfileDetailModel
{
    public List<TbTransactionProfileDetailDto> GetByCompanyAndTransactionAndCausal(int companyId, int transactionId,
        int causalId)
    {
        using var context = new DataContext();
        var result = from tc in context.TbTransactionProfileDetails
            join tp in context.TbTransactionConcepts on new { tc.ConceptID, tc.TransactionID }
                equals new { tp.ConceptID, tp.TransactionID }
            join a in context.TbAccounts on new { AccountID = Convert.ToInt32(tc.AccountID), tc.CompanyID }
                equals new { a.AccountID, a.CompanyID }
            join cc in context.TbCenterCosts on new { ClassID = Convert.ToInt32(tc.ClassID), tc.CompanyID }
                equals new { cc.ClassID, cc.CompanyID } into ccJoin
            from cc in ccJoin.DefaultIfEmpty()
            where tc.CompanyID == companyId
                  && tc.TransactionID == transactionId
                  && tc.TransactionCausalID == causalId
            select new TbTransactionProfileDetailDto
            {
                CompanyId = tc.CompanyID,
                TransactionId = tc.TransactionID,
                TransactionCausalId = tc.TransactionCausalID,
                ProfileDetailId = tc.ProfileDetailID,
                ConceptId = tc.ConceptID,
                AccountId = tc.AccountID,
                ClassId = tc.ClassID,
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
            .First(detail => detail.CompanyID == companyId
                             && detail.TransactionID == transactionId
                             && detail.TransactionCausalID == causalId
                             && detail.ProfileDetailID == profileDetailId);
    }

    public int DeleteAppPosme(int companyId, int transactionId, int causalId, int profileDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionProfileDetails
            .Where(detail => detail.CompanyID == companyId
                             && detail.TransactionID == transactionId
                             && detail.TransactionCausalID == causalId
                             && detail.ProfileDetailID == profileDetailId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbTransactionProfileDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ProfileDetailID;
    }
}