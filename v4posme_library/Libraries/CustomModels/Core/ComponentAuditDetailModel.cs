using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

class ComponentAuditDetailModel : IComponentAuditDetailModel
{
    public int InsertAppPosme(TbComponentAuditDetail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ComponentAuditDetailID;
    }

    public List<TbComponentAuditDetailDto> GetAuditDetail(int companyId, int id, int elementId)
    {
        using var context = new DataContext();
        var result = from cu in context.TbComponentAudits.AsNoTracking()
            join cud in context.TbComponentAuditDetails.AsNoTracking()
                on new { cu.CompanyID, ComponentAuditId = cu.ComponentAuditID } equals new { cud.CompanyID, ComponentAuditId = cud.ComponentAuditID }
            join u in context.TbUsers.AsNoTracking() on cu.ModifiedBy equals u.UserID
            join se in context.TbSubelements.AsNoTracking() on cud.FieldID equals se.SubElementID
            join ws in context.TbWorkflowStages.AsNoTracking()
                on Convert.ToInt32(cud.OldValue) equals ws.WorkflowStageID into wsJoin
            from ws in wsJoin.DefaultIfEmpty()
            join ws2 in context.TbWorkflowStages.AsNoTracking()
                on Convert.ToInt32(cud.NewValue) equals ws2.WorkflowStageID into ws2Join
            from ws2 in ws2Join.DefaultIfEmpty()
            join ci in context.TbCatalogItems.AsNoTracking() on Convert.ToInt32(cud.OldValue) equals ci.CatalogItemID into ciJoin
            from ci in ciJoin.DefaultIfEmpty()
            join ci2 in context.TbCatalogItems.AsNoTracking() on Convert.ToInt32(cud.NewValue) equals ci2.CatalogItemID into ci2Join
            from ci2 in ci2Join.DefaultIfEmpty()
            where cu.CompanyID == companyId
                  && cu.ElementItemID == id 
                  && cu.ElementID == elementId
            orderby cu.ModifiedOn descending
            select new TbComponentAuditDetailDto
            {
                ModifiedOn = cu.ModifiedOn,
                Nickname = u.Nickname,
                Name = se.Name,
                OldValue = cud.OldValue,
                NewValue = cud.NewValue
            };
        return result.ToList();
    }
}