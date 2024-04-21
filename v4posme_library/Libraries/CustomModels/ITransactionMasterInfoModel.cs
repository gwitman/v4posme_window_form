using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterInfoModel
{
    void DeleteAppPosme(int companyId,int transactionId,int transactionMasterId);
    
    int InsertAppPosme(TbTransactionMasterInfo data);
    
    void UpdateAppPosme(int companyId,int transactionId,int transactionMasterId,TbTransactionMasterInfo data);
    
    TbTransactionMasterInfoDto? GetRowByPk(int companyId, int transactionId, int transactionMasterId);
}