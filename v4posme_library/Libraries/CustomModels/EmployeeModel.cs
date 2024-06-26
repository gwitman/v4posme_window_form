﻿using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class EmployeeModel : IEmployeeModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbEmployee data)
    {
        using var context = new DataContext();
        var find = context.TbEmployees
            .Single(employee => employee.CompanyId == companyId
                                && employee.BranchId == branchId
                                && employee.EntityId == entityId);
        data.EmployeeId = find.EmployeeId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var find = context.TbEmployees
            .Single(employee => employee.CompanyId == companyId
                                && employee.BranchId == branchId
                                && employee.EntityId == entityId);
        find.IsActive = 0;
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbEmployee data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.EmployeeId;
    }

    public List<TbEmployeeDto> GetRowByBranchIdAndType(int companyId, int branchId, int typeEmployer)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
            join n in dbContext.TbNaturales on i.EntityId equals n.EntityId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.DepartamentId == typeEmployer
                  && i.IsActive == 1
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeId,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                DepartamentId = i.DepartamentId,
                AreaId = i.AreaId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeId,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeId,
                StartOn = i.StartOn,
                EndOn = i.EndOn,
                StatusId = i.StatusId,
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
            join n in dbContext.TbNaturales on i.EntityId equals n.EntityId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.IsActive == 1
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeId,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                DepartamentId = i.DepartamentId,
                AreaId = i.AreaId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeId,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeId,
                StartOn = i.StartOn,
                EndOn = i.EndOn,
                StatusId = i.StatusId,
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
            join n in dbContext.TbNaturales on i.EntityId equals n.EntityId
            where i.CompanyId == companyId
                  && i.BranchId == branchId
                  && i.EntityId == entityId
                  && i.IsActive==1
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeId,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                DepartamentId = i.DepartamentId,
                AreaId = i.AreaId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeId,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeId,
                StartOn = i.StartOn,
                EndOn = i.EndOn,
                StatusId = i.StatusId,
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
                     join n in dbContext.TbNaturales on i.EntityId equals n.EntityId
                     where i.CompanyId == companyId 
                           && i.IsActive == 1
                     select new TbEmployeeDto
                     {
                         CompanyId = i.CompanyId,
                         BranchId = i.BranchId,
                         EntityId = i.EntityId,
                         EmployeNumber = i.EmployeNumber,
                         NumberIdentification = i.NumberIdentification,
                         IdentificationTypeId = i.IdentificationTypeId,
                         SocialSecurityNumber = i.SocialSecurityNumber,
                         Address = i.Address,
                         CountryId = i.CountryId,
                         StateId = i.StateId,
                         CityId = i.CityId,
                         DepartamentId = i.DepartamentId,
                         AreaId = i.AreaId,
                         ClasificationId = i.ClasificationId,
                         CategoryId = i.CategoryId,
                         Reference1 = i.Reference1,
                         Reference2 = i.Reference2,
                         TypeEmployeeId = i.TypeEmployeeId,
                         HourCost = i.HourCost,
                         ParentEmployeeId = i.ParentEmployeeId,
                         StartOn = i.StartOn,
                         EndOn = i.EndOn,
                         StatusId = i.StatusId,
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

    public TbEmployeeDto GetRowByEntityId(int companyId, int entityId)
    {
        using var dbContext = new DataContext();
        var result = from i in dbContext.TbEmployees
            join n in dbContext.TbNaturales on i.EntityId equals n.EntityId
            where i.CompanyId == companyId
                  && i.EntityId == entityId
                  && i.IsActive==1
            select new TbEmployeeDto
            {
                CompanyId = i.CompanyId,
                BranchId = i.BranchId,
                EntityId = i.EntityId,
                EmployeNumber = i.EmployeNumber,
                NumberIdentification = i.NumberIdentification,
                IdentificationTypeId = i.IdentificationTypeId,
                SocialSecurityNumber = i.SocialSecurityNumber,
                Address = i.Address,
                CountryId = i.CountryId,
                StateId = i.StateId,
                CityId = i.CityId,
                DepartamentId = i.DepartamentId,
                AreaId = i.AreaId,
                ClasificationId = i.ClasificationId,
                CategoryId = i.CategoryId,
                Reference1 = i.Reference1,
                Reference2 = i.Reference2,
                TypeEmployeeId = i.TypeEmployeeId,
                HourCost = i.HourCost,
                ParentEmployeeId = i.ParentEmployeeId,
                StartOn = i.StartOn,
                EndOn = i.EndOn,
                StatusId = i.StatusId,
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
}