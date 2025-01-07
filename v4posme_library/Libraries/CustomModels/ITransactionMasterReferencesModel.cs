using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterReferencesModel
{
    void DeleteAppPosme(int transactionMasterReferencesId);

    int InsertAppPosme(TbTransactionMasterReference tbTransactionMasterReference);

    void UpdateAppPosme(int transactionMasterReferencesId, TbTransactionMasterReference tbTransactionMasterReference);

    void UpdateAppPosmeByTransactionMasterId(int transactionMasterId, TbTransactionMasterReference tbTransactionMasterReference);

    TbTransactionMasterReference? GetRowByPk(int transactionMasterReferencesId);

    TbTransactionMasterReference? GetRowByTransactionMasterId(int transactionMasterId);
}