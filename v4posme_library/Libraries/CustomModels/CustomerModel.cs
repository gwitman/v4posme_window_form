using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class CustomerModel : ICustomerModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomer data)
    {
        using var context = new DataContext();
        var find = context.TbCustomers
            .Single(customer => customer.CompanyId == companyId
                                && customer.BranchId == branchId
                                && customer.EntityId == entityId);
        data.CustomerId = find.CustomerId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomerDto data)
    {
        using var context = new DataContext();
        var find = context.TbCustomers
            .Single(customer => customer.CompanyId == companyId
                                && customer.BranchId == branchId
                                && customer.EntityId == entityId);
        data.CustomerId = find.CustomerId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }
    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var find = context.TbCustomers
            .Single(customer => customer.CompanyId == companyId
                                && customer.BranchId == branchId
                                && customer.EntityId == entityId);
        find.IsActive = false;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomer data)
    {
        using var context = new DataContext();
        var entityEntry = context.Add(data);
        context.BulkSaveChanges();
        return entityEntry.Entity.CustomerId;
    }

    public List<TbCustomerDto> GetHappyBirthDay(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from c in dbContext.TbCustomers
            join n in dbContext.TbNaturales on c.EntityId equals n.EntityId
            where c.CompanyId == companyId
                  && c.IsActive!.Value
                  && c.BirthDate <= DateTime.Today
            select new TbCustomerDto
            {
                CustomerNumber = c.CustomerNumber,
                FirstName = n.FirstName,
                LastName = n.LastName,
                BirthDate = c.BirthDate,
                BalancePoint = c.BalancePoint,
                DateContract = c.DateContract,
                EntityContactId = c.EntityContactId,
                Reference3 = c.Reference3,
                Reference4 = c.Reference4,
                Reference5 = c.Reference5,
                Reference6 = c.Reference6,
                Budget = c.Budget
            };


        return result.ToList();
    }

    public TbCustomer GetRowByCode(int companyId, string? customerCode)
    {
        using var dbContext = new DataContext();
        return dbContext.TbCustomers
            .Single(c => c.CompanyId == companyId
                         && c.IsActive!.Value
                         && c.CustomerNumber.Equals(customerCode));
    }

    public TbCustomer GetRowByIdentification(int companyId, int identification)
    {
        using var dbContext = new DataContext();
        return dbContext.TbCustomers
            .Single(c => c.CompanyId == companyId
                         && c.IsActive!.Value
                         && c.Identification.Equals(identification));
    }

    public List<TbCustomerDto> GetRowByCompanyPhoneAndEmail(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomers
            join nat in dbContext.TbNaturales on i.EntityId equals nat.EntityId
            where i.CompanyId == companyId
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                SubCategoryId = i.SubCategoryId,
                CustomerTypeId = i.CustomerTypeId,
                BirthDate = i.BirthDate,
                StatusId = i.StatusId,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionId,
                SexoId = i.SexoId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                CreatedIn = i.CreatedIn,
                CreatedBy = i.CreatedBy,
                CreatedOn = i.CreatedOn,
                CreatedAt = i.CreatedAt,
                IsActive = i.IsActive,
                FirstName = nat.FirstName,
                LastName = nat.LastName,
                TypeFirm = i.TypeFirm,
                BalancePoint = i.BalancePoint,
                PhoneNumber = i.PhoneNumber,
                DateContract = i.DateContract,
                EntityContactId = i.EntityContactId,
                Reference3 = i.Reference3,
                Reference4 = i.Reference4,
                Reference5 = i.Reference5,
                Reference6 = i.Reference6,
                Budget = i.Budget
            };
        return result.ToList();
    }

    public List<TbCustomerDto> GetRowByCompany(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomers
            join nat in dbContext.TbNaturales on i.EntityId equals nat.EntityId
            where i.CompanyId == companyId
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                SubCategoryId = i.SubCategoryId,
                CustomerTypeId = i.CustomerTypeId,
                BirthDate = i.BirthDate,
                StatusId = i.StatusId,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionId,
                SexoId = i.SexoId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                CreatedIn = i.CreatedIn,
                CreatedBy = i.CreatedBy,
                CreatedOn = i.CreatedOn,
                CreatedAt = i.CreatedAt,
                IsActive = i.IsActive,
                FirstName = nat.FirstName,
                LastName = nat.LastName,
                TypeFirm = i.TypeFirm,
                BalancePoint = i.BalancePoint,
                PhoneNumber = i.PhoneNumber,
                DateContract = i.DateContract,
                EntityContactId = i.EntityContactId,
                Reference3 = i.Reference3,
                Reference4 = i.Reference4,
                Reference5 = i.Reference5,
                Reference6 = i.Reference6,
                Budget = i.Budget
            };
        return result.ToList();
    }

    public TbCustomerDto GetRowByEntity(int companyId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomers
            join nat in dbContext.TbNaturales on i.EntityId equals nat.EntityId
            where i.CompanyId == companyId
                  && i.EntityId == entityId
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                SubCategoryId = i.SubCategoryId,
                CustomerTypeId = i.CustomerTypeId,
                BirthDate = i.BirthDate,
                StatusId = i.StatusId,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionId,
                SexoId = i.SexoId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                CreatedIn = i.CreatedIn,
                CreatedBy = i.CreatedBy,
                CreatedOn = i.CreatedOn,
                CreatedAt = i.CreatedAt,
                IsActive = i.IsActive,
                FirstName = nat.FirstName,
                LastName = nat.LastName,
                TypeFirm = i.TypeFirm,
                BalancePoint = i.BalancePoint,
                PhoneNumber = i.PhoneNumber,
                DateContract = i.DateContract,
                EntityContactId = i.EntityContactId,
                Reference3 = i.Reference3,
                Reference4 = i.Reference4,
                Reference5 = i.Reference5,
                Reference6 = i.Reference6,
                Budget = i.Budget
            };
        return result.Single();
    }

    public TbCustomer GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .Single(customer => customer.CompanyId == companyId
                                && customer.BranchId == branchId
                                && customer.EntityId == entityId);
    }
}