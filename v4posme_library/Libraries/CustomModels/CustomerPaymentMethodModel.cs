using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CustomerPaymentMethodModel : ICustomerPaymentMethodModel
{
    public int InsertAppPosme(TbCustomerPaymentMethod data)
    {
        using var context = new DataContext();
        var result = context.TbCustomerPaymentMethods.Add(data);
        context.SaveChanges();
        return result.Entity.CustomerPaymentMethod;
    }
    
    public void UpdateAppPosme(int entityId, TbCustomerPaymentMethod data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerPaymentMethods.SingleOrDefault(method => method.EntityID == entityId);
        if (find is null)
        {
            return;
        }
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var find = context.TbCustomerPaymentMethods.SingleOrDefault(method => method.EntityID == entityId);
        if (find is null)
        {
            return;
        }

        find.IsActive = false;
        context.SaveChanges();
    }

    public TbCustomerPaymentMethod? GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomerPaymentMethods
            .SingleOrDefault(method => method.EntityID == entityId && method.IsActive!.Value);
    }
}