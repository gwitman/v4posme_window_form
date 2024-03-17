using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IListPriceModel
{
    void UpdateAppPosme(int companyId, int listPriceId, TbListPrice data);

    void DeleteAppPosme(int companyId, int listPriceId);

    int InsertAppPosme(TbListPrice data);

    TbListPrice GetRowByPk(int companyId, int listPriceId);

    TbListPrice GetListPriceToApply(int companyId);
}