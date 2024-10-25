using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class EmployeeModel : IEmployeeModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbEmployee data)
    {
        using var context = new DataContext();
        var find = context.TbEmployees
            .Single(employee => employee.CompanyID == companyId
                                && employee.BranchID == branchId
                                && employee.EntityID == entityId);
        data.EmployeeID = find.EmployeeID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var find = context.TbEmployees
            .Single(employee => employee.CompanyID == companyId
                                && employee.BranchID == branchId
                                && employee.EntityID == entityId);
        find.IsActive = false;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbEmployee data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.EmployeeID;
    }

    public List<TbEmployeeDto>? GetRowByBranchIdAndType(int companyId, int branchId, int typeEmployer)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
            join n in dbContext.TbNaturales on i.EntityID equals n.EntityID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.DepartamentID == typeEmployer
                  && i.IsActive.Value
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeID,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                DepartamentId = i.DepartamentID,
                AreaId = i.AreaID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeID,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeID,
                StartOn = i.StartOn,
                EndOn = i.EndOn.Value,
                StatusId = i.StatusID,
                CreatedOn = i.CreatedOn,
                CreatedIn = i.CreatedIn,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName
            };
        return result.ToList();
    }

    public List<TbEmployeeDto> GetRowByBranchId(int companyId, int branchId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
            join n in dbContext.TbNaturales on i.EntityID equals n.EntityID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.IsActive.Value
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeID,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                DepartamentId = i.DepartamentID,
                AreaId = i.AreaID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeID,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeID,
                StartOn = (i.StartOn.Value),
                EndOn = (i.EndOn.Value),
                StatusId = i.StatusID,
                CreatedOn = i.CreatedOn,
                CreatedIn = i.CreatedIn,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName
            };
        return result.ToList();
    }

    public TbEmployeeDto GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
            join n in dbContext.TbNaturales on i.EntityID equals n.EntityID
            where i.CompanyID == companyId
                  && i.BranchID == branchId
                  && i.EntityID == entityId
                  && i.IsActive.Value
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeID,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                DepartamentId = i.DepartamentID,
                AreaId = i.AreaID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeID,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeID,
                StartOn =(i.StartOn.Value),
                EndOn = (i.EndOn.Value),
                StatusId = i.StatusID,
                CreatedOn = i.CreatedOn,
                CreatedIn = i.CreatedIn,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName
            };
        return result.Single();
    }
    public List<TbEmployeeDto> GetRowByCompanyId(int companyId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
                     join n in dbContext.TbNaturales on i.EntityID equals n.EntityID
                     where i.CompanyID == companyId 
                           && i.IsActive.Value
                     select new TbEmployeeDto
                     {
                         CompanyId = i.CompanyID,
                         BranchId = i.BranchID,
                         EntityId = i.EntityID,
                         EmployeNumber = i.EmployeNumber,
                         NumberIdentification = i.NumberIdentification,
                         IdentificationTypeId = i.IdentificationTypeID,
                         SocialSecurityNumber = i.SocialSecurityNumber,
                         Address = i.Address,
                         CountryId = i.CountryID,
                         StateId = i.StateID,
                         CityId = i.CityID,
                         DepartamentId = i.DepartamentID,
                         AreaId = i.AreaID,
                         ClasificationId = i.ClasificationID,
                         CategoryId = i.CategoryID,
                         Reference1 = i.Reference1,
                         Reference2 = i.Reference2,
                         TypeEmployeeId = i.TypeEmployeeID,
                         HourCost = i.HourCost,
                         ParentEmployeeId = i.ParentEmployeeID,
                         StartOn = (i.StartOn.Value),
                         EndOn = (i.EndOn.Value),
                         StatusId = i.StatusID,
                         CreatedOn = i.CreatedOn,
                         CreatedIn = i.CreatedIn,
                         CreatedAt = i.CreatedAt,
                         CreatedBy = i.CreatedBy,
                         IsActive = i.IsActive,
                         FirstName = n.FirstName,
                         LastName = n.LastName
                     };
        return result.ToList();
    }

    public TbEmployeeDto? GetRowByEntityId(int companyId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
            join n in dbContext.TbNaturales on i.EntityID equals n.EntityID
            where i.CompanyID == companyId
                  && i.EntityID == entityId
                  && i.IsActive.Value
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyID,
                BranchId = i.BranchID,
                EntityId = i.EntityID,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeID,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryID,
                StateId = i.StateID,
                CityId = i.CityID,
                DepartamentId = i.DepartamentID,
                AreaId = i.AreaID,
                ClasificationId = i.ClasificationID,
                CategoryId = i.CategoryID,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeID,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeID,
                StartOn =(i.StartOn.Value),
                EndOn = (i.EndOn.Value),
                StatusId = i.StatusID,
                CreatedOn = i.CreatedOn,
                CreatedIn = i.CreatedIn,
                CreatedAt = i.CreatedAt,
                CreatedBy = i.CreatedBy,
                IsActive = i.IsActive,
                FirstName = n.FirstName,
                LastName = n.LastName
            };
        return result.SingleOrDefault();
    }
}