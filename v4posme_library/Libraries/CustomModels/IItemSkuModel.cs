using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemSkuModel
{
    void DeleteAppPosme(int itemId);

    void UpdateAppPosme(int itemId, int catalogItemId, TbItemSku data);

    int InsertAppPosme(TbItemSku data);

    List<TbItemSku> GetRowByItemId(int itemId);

    TbItemSku GetByPk(int itemId, int catalogItemId);

    List<TbItemSku> GetRowByTransactionMasterId(int companyId, int transactionMasterId);
}