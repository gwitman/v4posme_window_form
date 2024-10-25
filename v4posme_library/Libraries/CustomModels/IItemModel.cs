using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemModel
{
    void UpdateAppPosme(int companyId, int itemId, TbItem data);

    void DeleteAppPosme(int companyId, int itemId);

    int InsertAppPosme(TbItem data);

    TbItem? GetRowByCode(int companyId, string? itemNumber);
    
    TbItem? GetRowByCodeBarra(int companyId, string? itemNumber);
    
    List<TbItem> GetRowByCodeBarraSimilar(int companyId, string? itemNumber);
    
    TbItem? GetRowByPk(int companyId,int itemId);
    
    TbItem? GetRwByPkAndInactive(int companyId,int itemId);
    
    List<TbItem> GetRowsByPk(int companyId,List<int> listItem);
    
    List<TbItem> GetRowByCompany(int companyId);
    
    int GetCount(int companyId);

    List<TbItemDto> GetRowByTransactionMasterId(int transactionMasterId);
}