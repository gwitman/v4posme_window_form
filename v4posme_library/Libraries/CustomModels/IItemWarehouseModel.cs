using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemWarehouseModel
{
    void DeleteWhereIdNotIn(int companyId, int itemId, List<int> listWarehouseId);

    void UpdateAppPosme(int companyId, int itemId, int warehouseId, TbItemWarehouse data);

    int InsertAppPosme(TbItemWarehouse data);

    List<TbItemWarehouseDto> GetByWarehouse(int companyId, int warehouseId);

    List<TbItemWarehouseDto> GetRowLowMinimus(int companyId);

    List<TbItemWarehouseDto> GetRowByItemId(int companyId, int itemId);

    TbItemWarehouseDto? GetByPk(int companyId, int itemId, int warehouseId);

    int WarehouseIsEmpty(int companyId, int warehouseId);
}