using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class BiometricUserModel : IBiometricUserModel
{
    public void DeleteAppPosme(int userId)
    {
        using var context = new DataContext();
        var find = context.TbUsers.Single(user => user.UserId == userId);
        context.TbUsers.Remove(find);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbUser data)
    {
        using var context = new DataContext();
        var add = context.TbUsers.Add(data);
        context.BulkSaveChanges();
        return add.Entity.UserId;
    }
}