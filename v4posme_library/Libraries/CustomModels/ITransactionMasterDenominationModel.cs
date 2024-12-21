using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterDenominationModel
{
    int DeleteAppPosme(int transactionMasterId, DataContext? dataContext =null);

    int InsertAppPosme(TbTransactionMasterDenomination data, DataContext? dataContext =null);

    void UpdateAppPosme(int transactionMasterDenominationId, TbTransactionMasterDenomination data);

    List<TbTransactionMasterDenominationDto> GetRowByTransactionMaster(int companyId, int transactionId,
        int transactionMasterId);
    
    TbTransactionMasterDenominationDto GetRowByPk(int transactionMasterDenominationId);
}