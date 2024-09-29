using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemSkuModel
{
    int DeleteAppPosme(int itemId);

    void UpdateAppPosme(int itemId, int catalogItemId, TbItemSku data);

    int InsertAppPosme(TbItemSku data);

    List<TbItemSkuDto> GetRowByItemId(int itemId);

    TbItemSkuDto? GetByPk(int itemId, int catalogItemId);

    List<TbItemSkuDto> GetRowByTransactionMasterId(int companyId, int transactionMasterId);
}