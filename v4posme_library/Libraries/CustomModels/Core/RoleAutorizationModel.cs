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
            .Where(autorization => autorization.CompanyId == companyId
                                   && autorization.BranchId == branchId
                                   && autorization.RoleId == roleId)
            .ExecuteDelete();
    }

    public int DeleteAppPosme(int companyId, int branchId, int roleId, int componentAutorizationId)
    {
        using var context = new DataContext();
        return context.TbRoleAutorizations
            .Where(autorization => autorization.CompanyId == companyId
                                   && autorization.BranchId == branchId
                                   && autorization.RoleId == roleId
                                   && autorization.ComponentAutorizationId == componentAutorizationId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbRoleAutorization data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.RoleAurotizationId;
    }

    public List<TbRoleAutorizationDto> GetRowByRoleAutorization(int companyId, int branchId, int roleId)
    {
        using var context = new DataContext();
        var query = from ra in context.TbRoleAutorizations.AsNoTracking()
            join ca in context.TbComponentAutorizations.AsNoTracking()
                on new { ra.CompanyId, ra.ComponentAutorizationId } equals new
                    { ca.CompanyId, ca.ComponentAutorizationId }
            where ra.CompanyId == companyId
                  && ra.BranchId == branchId
                  && ra.RoleId == roleId
            select new TbRoleAutorizationDto
            {
                CompanyId = ra.CompanyId,
                BranchId = ra.BranchId,
                RoleId = ra.RoleId,
                ComponentAutorizationId = ra.ComponentAutorizationId,
                Name = ca.Name
            };
        return query.ToList();
    }

    public List<TbRoleAutorizationDto>? GetRowByRole(int companyId, int branchId, int roleId)
    {
        using var context = new DataContext();
        var query = from ra in context.TbRoleAutorizations.AsNoTracking()
            join ca in context.TbComponentAutorizations.AsNoTracking()
                on new { ra.CompanyId, ra.ComponentAutorizationId } equals new
                    { ca.CompanyId, ca.ComponentAutorizationId }
            join cad in context.TbComponentAutorizationDetails.AsNoTracking()
                on new { ca.CompanyId, ca.ComponentAutorizationId } equals new
                    { cad.CompanyId, cad.ComponentAutorizationId }
            where ra.CompanyId == companyId
                  && ra.BranchId == branchId
                  && ra.RoleId == roleId
            select new TbRoleAutorizationDto
            {
                CompanyId = ra.CompanyId,
                BranchId = ra.BranchId,
                RoleId = ra.RoleId,
                ComponentAutorizationId = cad.ComponentAutorizationId,
                ComponentId = cad.ComponentId,
                WorkflowId = cad.WorkflowId,
                WorkflowStageId = cad.WorkflowStageId
            };
        return query.ToList();
    }

    public List<TbRoleAutorizationDto> GetRowByPk(int companyId, int branchId, int roleId, int componentAutorizationId)
    {
        using var context = new DataContext();
        var query = from ra in context.TbRoleAutorizations.AsNoTracking()
            join ca in context.TbComponentAutorizations.AsNoTracking()
                on new { ra.CompanyId, ra.ComponentAutorizationId } equals new
                    { ca.CompanyId, ca.ComponentAutorizationId }
            join cad in context.TbComponentAutorizationDetails.AsNoTracking()
                on new { ca.CompanyId, ca.ComponentAutorizationId } equals new
                    { cad.CompanyId, cad.ComponentAutorizationId }
            where ra.CompanyId == companyId
                  && ra.BranchId == branchId
                  && ra.RoleId == roleId
                  && ra.ComponentAutorizationId == componentAutorizationId
            select new TbRoleAutorizationDto
            {
                CompanyId = ra.CompanyId,
                BranchId = ra.BranchId,
                RoleId = ra.RoleId,
                ComponentAutorizationId = cad.ComponentAutorizationId,
                ComponentId = cad.ComponentId,
                WorkflowId = cad.WorkflowId,
                WorkflowStageId = cad.WorkflowStageId
            };
        return query.ToList();
    }
}