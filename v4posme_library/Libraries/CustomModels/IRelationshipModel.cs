using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IRelationshipModel
{
    int DeleteAppPosme(int employeeId,int customerId);
    
    int InsertAppPosme(TbRelationship data);
}