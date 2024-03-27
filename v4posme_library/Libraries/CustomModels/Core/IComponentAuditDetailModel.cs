using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IComponentAuditDetailModel
{
    int InsertAppPosme(TbComponentAuditDetail data);
    
    List<TbComponentAuditDetailDto> GetAuditDetail(int companyId, int id, int elementId);
}