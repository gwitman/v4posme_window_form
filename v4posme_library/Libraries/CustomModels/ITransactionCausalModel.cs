using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionCausalModel
{
    List<TbTransactionCausal> GetCausalByBranch(int companyId, int transactionId, int branchId);

    TbTransactionCausal? GetCausalDefaultId(int companyId, int transactionId);

    List<TbTransactionCausalDto> GetByCompanyAndTransaction(int companyId, int transactionId);

    TbTransactionCausal? GetByCompanyAndTransactionAndCausal(int companyId, int transactionId, int causalId);

    void DeleteAppPosme(int companyId, int transactionId, List<int> listCausal);

    int InsertAppPosme(TbTransactionCausal data);

    void UpdateAppPosme(int companyId, int transactionId, int causalId, TbTransactionCausal data);

    int CountCausalDefault(int companyId, int transactionId);
}