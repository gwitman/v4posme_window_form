using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ComponentAuditModel : IComponentAuditModel
{
    public int InsertAppPosme(TbComponentAudit data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ComponentAuditId;
    }
}