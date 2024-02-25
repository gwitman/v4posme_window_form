using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerConsultasSinRiesgoModel
{
    void UpdateAppPosme(int requestId, TbCustomerConsultasSinRiesgo data);

    void UpdateByCedula(int companyId, string cedula, TbCustomerConsultasSinRiesgo data);

    int InsertAppPosme(TbCustomerConsultasSinRiesgo data);

    TbCustomerConsultasSinRiesgo GetRowByPk(int requestId);
    
    TbCustomerConsultasSinRiesgo GetRowByCedulaLast(int companyId, string cedula);
    
    TbCustomerConsultasSinRiesgo GetRowValidOld(int requestId,int old);
    
    List<TbCustomerConsultasSinRiesgo> GetRowByCedulaFileName(int companyId,string cedula);
    
    List<VwSinRiesgoReporteCreditosToSystema> GetRowByCompany(int companyId);
}