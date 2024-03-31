using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanySubElementAuditModel
{
    List<TbCompanySubelementAuditDto>? ListElementAudit(int companyId, int elementId);
}