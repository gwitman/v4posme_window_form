using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterDetailCreditModel
{
    int InsertAppPosme(TbTransactionMasterDetailCredit data);

    void UpdateAppPosme(int transactionMasterDetailId, TbTransactionMasterDetailCredit? data);

    int DeleteAppPosme(int transactionMasterDetailId);

    TbTransactionMasterDetailCredit? GetRowByPk(int transactionMasterDetailId);

    int DeleteWhereIdNotIn(int transactionMasterId, List<int> listTmdId);
}