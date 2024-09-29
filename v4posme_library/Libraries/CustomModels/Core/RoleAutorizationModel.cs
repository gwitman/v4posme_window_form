using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

class RoleAutorizationModel : IRoleAutorizationModel
{
    public int DeleteByRole(int companyId, int branchId, int roleId)
    {
        using var context = new DataContext();
        return context.TbRoleAutorizations
            .Where(autorization => autorization.CompanyID == companyId
                                   && autorization.BranchID == branchId
                                   && autorization.RoleID == roleId)
            .ExecuteDelete();
    }

    public int DeleteAppPosme(int companyId, int branchId, int roleId, int componentAutorizationId)
    {
        using var context = new DataContext();
        return context.TbRoleAutorizations
            .Where(autorization => autorization.CompanyID == companyId
                                   && autorization.BranchID == branchId
                                   && autorization.RoleID == roleId
                                   && autorization.ComponentAutorizationID == componentAutorizationId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbRoleAutorization data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.RoleAurotizationID;
    }

    public List<TbRoleAutorizationDto> GetRowByRoleAutorization(int companyId, int branchId, int roleId)
    {
        using var context = new DataContext();
        var query = from ra in context.TbRoleAutorizations.AsNoTracking()
            join ca in context.TbComponentAutorizations.AsNoTracking()
                on new { ra.CompanyID, ra.ComponentAutorizationID } equals new
                    { ca.CompanyID, ca.ComponentAutorizationID }
            where ra.CompanyID == companyId
                  && ra.BranchID == branchId
                  && ra.RoleID == roleId
            select new TbRoleAutorizationDto
            {
                CompanyId = ra.CompanyID,
                BranchId = ra.BranchID,
                RoleId = ra.RoleID,
                ComponentAutorizationId = ra.ComponentAutorizationID,
                Name = ca.Name
            };
        return query.ToList();
    }

    public List<TbRoleAutorizationDto>? GetRowByRole(int companyId, int branchId, int roleId)
    {
        using var context = new DataContext();
        var query = from ra in context.TbRoleAutorizations.AsNoTracking()
            join ca in context.TbComponentAutorizations.AsNoTracking()
                on new { ra.CompanyID, ra.ComponentAutorizationID } equals new
                    { ca.CompanyID, ca.ComponentAutorizationID }
            join cad in context.TbComponentAutorizationDetails.AsNoTracking()
                on new { ca.CompanyID, ca.ComponentAutorizationID } equals new
                    { cad.CompanyID, cad.ComponentAutorizationID }
            where ra.CompanyID == companyId
                  && ra.BranchID == branchId
                  && ra.RoleID == roleId
            select new TbRoleAutorizationDto
            {
                CompanyId = ra.CompanyID,
                BranchId = ra.BranchID,
                RoleId = ra.RoleID,
                ComponentAutorizationId = cad.ComponentAutorizationID,
                ComponentId = cad.ComponentID,
                WorkflowId = cad.WorkflowID,
                WorkflowStageId = cad.WorkflowStageID
            };
        return query.ToList();
    }

    public List<TbRoleAutorizationDto> GetRowByPk(int companyId, int branchId, int roleId, int componentAutorizationId)
    {
        using var context = new DataContext();
        var query = from ra in context.TbRoleAutorizations.AsNoTracking()
            join ca in context.TbComponentAutorizations.AsNoTracking()
                on new { ra.CompanyID, ra.ComponentAutorizationID } equals new
                    { ca.CompanyID, ca.ComponentAutorizationID }
            join cad in context.TbComponentAutorizationDetails.AsNoTracking()
                on new { ca.CompanyID, ca.ComponentAutorizationID } equals new
                    { cad.CompanyID, cad.ComponentAutorizationID }
            where ra.CompanyID == companyId
                  && ra.BranchID == branchId
                  && ra.RoleID == roleId
                  && ra.ComponentAutorizationID == componentAutorizationId
            select new TbRoleAutorizationDto
            {
                CompanyId = ra.CompanyID,
                BranchId = ra.BranchID,
                RoleId = ra.RoleID,
                ComponentAutorizationId = cad.ComponentAutorizationID,
                ComponentId = cad.ComponentID,
                WorkflowId = cad.WorkflowID,
                WorkflowStageId = cad.WorkflowStageID
            };
        return query.ToList();
    }
}