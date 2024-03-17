using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IPriceModel
{
    void DeleteAppPosme(int companyId, int listPriceId);

    int InsertAppPosme(TbPrice data);

    void UpdateAppPosme(int companyId, int listPriceId, int itemId, int typePriceId, TbPrice data);
    
    List<TbPrice> GetRowByAll(int companyId,int listPriceId);
    
    TbPrice GetRowByPk(int companyId,int listPriceId,int itemId,int typePriceId);
    
    List<TbPrice> GetRowByTransactionMasterId(int companyId, int listPriceId, int transactionMasterId);
    
    TbPrice GetRowByItemIdAndAmountAndComission(int companyId,int listPriceId,int itemId,decimal amount);
    
    List<TbPrice> get_rowByItemID(int companyId,int listPriceId,int itemId);
}