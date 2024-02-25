using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CustomerConsultasSinRiesgoModel : ICustomerConsultasSinRiesgoModel
{
    private static IQueryable<TbCustomerConsultasSinRiesgo> FindByRequestId(int requestId, DataContext context)
    {
        return context.TbCustomerConsultasSinRiesgoes
            .Where(riesgo => riesgo.RequestId == requestId);
    }

    private static IQueryable<TbCustomerConsultasSinRiesgo> FindByCedulaAndCompanyId(int companyId, string cedula, DataContext context)
    {
        return context.TbCustomerConsultasSinRiesgoes
            .Where(riesgo => riesgo.CompanyId == companyId 
                             && riesgo.Id.Equals(cedula));
    }
    public void UpdateAppPosme(int requestId, TbCustomerConsultasSinRiesgo data)
    {
        using var context = new DataContext();
        var find = FindByRequestId(requestId, context).Single();
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public void UpdateByCedula(int companyId, string cedula, TbCustomerConsultasSinRiesgo data)
    {
        using var context = new DataContext();
        var find = FindByCedulaAndCompanyId(companyId, cedula, context).Single();
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomerConsultasSinRiesgo data)
    {
        using var context = new DataContext();
        var add = context.TbCustomerConsultasSinRiesgoes.Add(data);
        context.BulkSaveChanges();
        return add.Entity.RequestId;
    }

    public TbCustomerConsultasSinRiesgo GetRowByPk(int requestId)
    {
        using var context = new DataContext();
        return FindByRequestId(requestId, context).Single();
    }

    public TbCustomerConsultasSinRiesgo GetRowByCedulaLast(int companyId, string cedula)
    {
        using var context = new DataContext();
        return FindByCedulaAndCompanyId(companyId, cedula, context)
            .OrderByDescending(riesgo=> riesgo.CreatedOn)
            .First();
    }

    public TbCustomerConsultasSinRiesgo GetRowValidOld(int requestId, int old)
    {
        using var context = new DataContext();
        return FindByRequestId(requestId, context)
            .Single(riesgo => old > 0 && (DateTime.Now - riesgo.CreatedOn).Days > 0);
    }

    public List<TbCustomerConsultasSinRiesgo> GetRowByCedulaFileName(int companyId, string cedula)
    {
        using var context = new DataContext();
        return FindByCedulaAndCompanyId(companyId, cedula, context)
            .OrderByDescending(riesgo=> riesgo.File)
            .ToList();
    }

    public List<VwSinRiesgoReporteCreditosToSystema> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.VwSinRiesgoReporteCreditosToSystemas
            .Where(system => system.CompanyId == companyId)
            .AsNoTracking()
            .ToList();
    }
}