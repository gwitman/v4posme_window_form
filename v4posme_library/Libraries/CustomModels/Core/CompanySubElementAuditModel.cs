using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

class CompanySubElementAuditModel : ICompanySubElementAuditModel
{
    public List<TbCompanySubelementAuditDto>? ListElementAudit(int companyId, int elementId)
    {
        using var context = new DataContext();
        return context.TbCompanySubelementAudits.AsNoTracking()
            .Join(context.TbSubelements.AsNoTracking(),
                audit => audit.SubElementID,
                subelement => subelement.SubElementID
                , (audit, subelement) => new { audit, subelement })
            .Where(arg => arg.audit.CompanyID == companyId
                          && arg.audit.ElementID == elementId)
            .Select(arg => new TbCompanySubelementAuditDto
            {
                CompanyId = arg.audit.CompanyID,
                ElementId = arg.audit.ElementID,
                SubElementId = arg.audit.SubElementID,
                Name = arg.subelement.Name
            }).ToList();
    }
}