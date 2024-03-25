using v4posme_library.Models;
using v4posme_library.ModelsViews;

namespace v4posme_library.Libraries.CustomModels;

public interface ICompanyCurrencyModel
{
    int DeleteAppPosme(int companyId,int currencyId);
    
    void UpdateAppPosme(int companyId,int currencyId,TbCompanyCurrency data);
    
    int InsertAppPosme(TbCompanyCurrency data);

    TbCompanyCurrency GetRowByPk(int companyId, int currencyId);
    
    List<TbCompanyCurrencyDto> GetByCompany(int companyId);
}