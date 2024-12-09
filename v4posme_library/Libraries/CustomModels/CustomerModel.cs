using System.ComponentModel.Design;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class CustomerModel : ICustomerModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomer data)
    {
        using var context = new DataContext();
        var find = context.TbCustomers
            .Single(customer => customer.CompanyID == companyId
                                && customer.BranchID == branchId
                                && customer.EntityID == entityId);
        data.CustomerID = find.CustomerID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var find = context.TbCustomers
            .Single(customer => customer.CompanyID == companyId
                                && customer.BranchID == branchId
                                && customer.EntityID == entityId);
        find.IsActive = false;
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCustomer data)
    {
        using var context = new DataContext();
        var entityEntry = context.Add(data);
        context.SaveChanges();
        return entityEntry.Entity.CustomerID;
    }

    public List<TbCustomerDto> GetHappyBirthDay(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from c in dbContext.TbCustomers
            join n in dbContext.TbNaturales on c.EntityID equals n.EntityID
            where c.CompanyID == companyId
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
                EntityContactId = c.EntityContactID,
                Reference3 = c.Reference3,
                Reference4 = c.Reference4,
                Reference5 = c.Reference5,
                Reference6 = c.Reference6,
                Budget = c.Budget
            };


        return result.ToList();
    }

    public TbCustomer? GetRowByCode(int companyId, string? customerCode)
    {
        using var dbContext = new DataContext();
        return dbContext.TbCustomers
            .SingleOrDefault(c => c.CompanyID == companyId
                         && c.IsActive!.Value
                         && c.CustomerNumber.Equals(customerCode));
    }

    public TbCustomerDto? GetRowByCodeDto(int companyId, string? customerCode)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomers
            join nat in dbContext.TbNaturales on i.EntityID equals nat.EntityID
            where i.CompanyID == companyId
                  && i.CustomerNumber.Equals(customerCode)
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                SubCategoryId = i.SubCategoryID,
                CustomerTypeId = i.CustomerTypeID,
                BirthDate = i.BirthDate,
                StatusId = i.StatusID,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionID,
                SexoId = i.SexoID,
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
                DateContract = (i.DateContract),
                EntityContactId = i.EntityContactID,
                Reference3 = i.Reference3,
                Reference4 = i.Reference4,
                Reference5 = i.Reference5,
                Reference6 = i.Reference6,
                Budget = i.Budget
            };
        return result.SingleOrDefault();
    }

    public TbCustomer? GetRowByIdentification(int companyId, string identification)
    {
        using var dbContext = new DataContext();
        return dbContext.TbCustomers
            .Single(c => c.CompanyID == companyId
                         && c.IsActive!.Value
                         && c.Identification.Equals(identification));
    }

    public List<TbCustomerDto> GetRowByCompanyPhoneAndEmail(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomers
            join nat in dbContext.TbNaturales on i.EntityID equals nat.EntityID
            where i.CompanyID == companyId
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                SubCategoryId = i.SubCategoryID,
                CustomerTypeId = i.CustomerTypeID,
                BirthDate = (i.BirthDate),
                StatusId = i.StatusID,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionID,
                SexoId = i.SexoID,
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
                DateContract = (i.DateContract),
                EntityContactId = i.EntityContactID,
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
            join nat in dbContext.TbNaturales on i.EntityID equals nat.EntityID
            where i.CompanyID == companyId
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                SubCategoryId = i.SubCategoryID,
                CustomerTypeId = i.CustomerTypeID,
                BirthDate = (i.BirthDate),
                StatusId = i.StatusID,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionID,
                SexoId = i.SexoID,
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
                DateContract = (i.DateContract),
                EntityContactId = i.EntityContactID,
                Reference3 = i.Reference3,
                Reference4 = i.Reference4,
                Reference5 = i.Reference5,
                Reference6 = i.Reference6,
                Budget = i.Budget
            };
        return result.ToList();
    }

    public TbCustomerDto? GetRowByEntity(int companyId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbCustomers
            join nat in dbContext.TbNaturales on i.EntityID equals nat.EntityID
            where i.CompanyID == companyId
                  && i.EntityID == entityId
                  && i.IsActive!.Value
            select new TbCustomerDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                CustomerNumber = i.CustomerNumber,
                IdentificationType = i.IdentificationType,
                Identification = i.Identification,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                Location = i.Location,
                Address = i.Address,
                CurrencyId = i.CurrencyID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                SubCategoryId = i.SubCategoryID,
                CustomerTypeId = i.CustomerTypeID,
                BirthDate = i.BirthDate,
                StatusId = i.StatusID,
                TypePay = i.TypePay,
                PayConditionId = i.PayConditionID,
                SexoId = i.SexoID,
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
                DateContract = (i.DateContract),
                EntityContactId = i.EntityContactID,
                Reference3 = i.Reference3,
                Reference4 = i.Reference4,
                Reference5 = i.Reference5,
                Reference6 = i.Reference6,
                Budget = i.Budget
            };
        return result.SingleOrDefault();
    }

    public TbCustomer? GetRowByPKK(int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .FirstOrDefault(customer => customer.EntityID == entityId);
    }

    public TbCustomer? GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomers
            .FirstOrDefault(customer => customer.CompanyID == companyId
                                        && customer.BranchID == branchId
                                        && customer.EntityID == entityId);
    }
}