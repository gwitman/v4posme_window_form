﻿using Microsoft.EntityFrameworkCore;
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
                audit => audit.SubElementId,
                subelement => subelement.SubElementId
                , (audit, subelement) => new { audit, subelement })
            .Where(arg => arg.audit.CompanyId == companyId
                          && arg.audit.ElementId == elementId)
            .Select(arg => new TbCompanySubelementAuditDto
            {
                CompanyId = arg.audit.CompanyId,
                ElementId = arg.audit.ElementId,
                SubElementId = arg.audit.SubElementId,
                Name = arg.subelement.Name
            }).ToList();
    }
}