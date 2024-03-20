using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterDenominationModel
{
    int DeleteAppPosme(int transactionMasterId);

    int InsertAppPosme(TbTransactionMasterDenomination data);

    void UpdateAppPosme(int transactionMasterDenominationId, TbTransactionMasterDenomination data);

    List<TbTransactionMasterDenomination> GetRowByTransactionMaster(int companyId, int transactionId,
        int transactionMasterId);
    
    TbTransactionMasterDenomination GetRowByPk(int transactionMasterDenominationId);
}