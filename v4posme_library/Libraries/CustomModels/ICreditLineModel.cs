using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICreditLineModel
{
    int InsertAppPosme(TbCreditLine data);

    void UpdateAppPosme(int companyId, int creditLineId, TbCreditLine data);
        
    void DeleteAppPosme(int companyId,int creditLineId);
    
    List<TbCreditLine> GetRowByCompany(int companyId);
    
    TbCreditLine  GetRowByPk(int companyId,int creditLineId);
}