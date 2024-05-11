using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class LogModel : ILogModel
{
    public TbLog GetRowByPk(int companyId, int branchId, int loginId, string token)
    {
        using var context = new DataContext();
        return context.TbLogs.AsNoTracking()
            .Where(log => log.CompanyId == companyId
                          && log.BranchId == branchId
                          && log.LoginId == loginId
                          && log.Token == token)
            .OrderByDescending(log => log.CreatedOn)
            .First();
    }

    public TbLog GetRowByNameParameterOutput(int companyId, int branchId, int loginId, string token,
        string nameParameterOutput)
    {
        using var context = new DataContext();
        return context.TbLogs.AsNoTracking()
            .Where(log => log.CompanyId == companyId
                          && log.BranchId == branchId
                          && log.LoginId == loginId
                          && log.Token == token
                          && log.ProcedureName==nameParameterOutput)
            .OrderByDescending(log => log.CreatedOn)
            .First();
    }
}