using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerFrecuencyActuationsModel
{
    int InsertAppPosme(TbCustomerFrecuencyActuation data);
    
    void UpdateAppPosme(int entityId, int idFrecuencyActuation, TbCustomerFrecuencyActuation data);
    
    void DeleteAppPosme(int entityId);
    
    void DeleteWhereIdNotIn(int entityId, List<int?> arrayId);

    TbCustomerFrecuencyActuation? GetRowByPk(int entityId, int id);
    
    List<TbCustomerFrecuencyActuation> GetrowByEntityId(int entityId);

    List<TbExpiredRegisterDto> GetRowExpiredRegisters(string userName);
}