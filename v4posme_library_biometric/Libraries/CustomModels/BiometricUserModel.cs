using Models;
using v4posme_library_biometric.Models;

namespace v4posme_library_biometric.Libraries.CustomModels;

public class BiometricUserModel : IBiometricUserModel
{
    public void DeleteAppPosme(int userId)
    {
        using var context = new DataContext();
        var find = context.Users.Single(user => user.Id == userId);
        context.Users.Remove(find);
        context.SaveChanges();
    }

    public int InsertAppPosme(User? data)
    {
        using var context = new DataContext();
        var add = context.Users.Add(data);
        context.SaveChanges();
        return add.Entity.Id;
    }
}