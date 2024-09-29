using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ITransactionModel
{
    TbTransaction? GetRowByPk(int companyId, string? name);

    int GetCounterTransactionMaster(int companyId, int transactionId, int statusId);

    int GetCountInput(int companyId);

    int GetCountOutput(int companyId);

    TbTransaction? GetByCompanyAndTransaction(int companyId, int transactionId);

    List<TbTransaction?> GetTransactionContabilizable(int companyId);

    void UpdateAppPosme(int companyId, int transactionId, TbTransaction data);
}