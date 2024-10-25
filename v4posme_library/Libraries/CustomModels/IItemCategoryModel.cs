using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemCategoryModel
{
    void DeleteAppPosme(int companyId, int itemCategoryId);

    int InsertAppPosme(TbItemCategory data);

    void UpdateAppPosme(int companyId, int itemCategoryId, TbItemCategory data);

    List<TbItemCategory>? GetByCompany(int companyId);

    TbItemCategory GetByPk(int companyId, int itemCategoryId);
}