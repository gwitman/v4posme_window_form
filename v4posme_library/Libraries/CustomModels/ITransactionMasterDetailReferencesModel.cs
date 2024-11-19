using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterDetailReferencesModel
{
    void DeleteAppPosme(int transactionMasterDetailRefereceId);
    void DeleteWhereIdNotIn(List<int> listTmdId);
    int InsertAppPosme(TbTransactionMasterDetailReference data);
    void UpdateAppPosme(int transactionMasterDetailRefereceId, TbTransactionMasterDetailReference data);
    TbTransactionMasterDetailReference? GetRowByPk(int transactionMasterDetailRefereceId);
    List<TbTransactionMasterDetailReferenceDTO> GetRowByTransactionMasterIdAndComponentId(int transactionMasterId, int componentId);
    List<TbTransactionMasterDetailReference> GetRowByTransactionMasterId(int transactionMasterId);
}