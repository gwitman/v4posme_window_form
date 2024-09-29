using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class RelationshipModel : IRelationshipModel
{
    public int DeleteAppPosme(int employeeId, int customerId)
    {
        using var context = new DataContext();
        return context.TbRelationships
            .Where(relationship => relationship.EmployeeID == employeeId
                                   && relationship.CustomerID == customerId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbRelationship data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.RelationshipID;
    }
}