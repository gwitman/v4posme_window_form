using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IItemWarehouseExpiredModel
{
    void DeleteByPk(int companyId, int itemWarehouseExpiredId);

    void UpdateAppPosme(int companyId, int itemWarehouseExpiredId, TbItemWarehouseExpired data);

    int InsertAppPosme(TbItemWarehouseExpired data);

    List<TbItemWarehouseExpired> GetByItemIdAndWarehouse(int companyId, int warehouseId, int itemId);

    List<TbItemWarehouseExpired> GetByItemIdAndWarehouseAndLote(int companyId, int warehouseId, int itemId,
        string lote);

    TbItemWarehouseExpired getBy_ItemIDAndWarehouseAndLoteAndExpired(int companyId, int warehouseId,
        int itemId, string lote, DateTime expired);
    
    TbItemWarehouseExpired GetByPk(int companyId,int itemWarehouseExpiredId);
    
    List<TbItemWarehouseExpiredDto> GetByItemIdAproxVencimiento(int companyId);
}