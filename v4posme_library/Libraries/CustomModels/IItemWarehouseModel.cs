using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemWarehouseModel
{
    void DeleteWhereIdNotIn(int companyId, int itemId, List<int> listWarehouseId);

    void UpdateAppPosme(int companyId, int itemId, int warehouseId, TbItemWarehouse data);

    int InsertAppPosme(TbItemWarehouse data);

    List<TbItemWarehouse> GetByWarehouse(int companyId, int warehouseId);

    List<TbItemWarehouse> GetRowLowMinimus(int companyId);

    List<TbItemWarehouse> GetRowByItemId(int companyId, int itemId);

    TbItemWarehouse GetByPk(int companyId, int itemId, int warehouseId);

    int WarehouseIsEmpty(int companyId, int warehouseId);
}