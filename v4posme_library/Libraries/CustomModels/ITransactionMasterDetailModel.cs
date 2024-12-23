using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterDetailModel
{
    int InsertAppPosme(TbTransactionMasterDetail data, DataContext? dataContext = null);

    void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, int transactionMasterDetailId, TbTransactionMasterDetail data, DataContext? dataContext = null);

    TbTransactionMasterDetailDto GetRowByPk(int companyId, int transactionId, int transactionMasterId, int transactionMasterDetailId, int componentId = 33);

    TbTransactionMasterDetail? GetRowByPKK(int transactionMasterDetailId);

    TbTransactionMasterDetailDto GetRowByTransactionAndItems(int companyId, int transactionId, int transactionMasterId, List<int> listTmdId);

    List<TbTransactionMasterDetailDto> GetRowByTransactionAndWarehouse(int companyId, int transactionId, int transactionMasterId);

    List<TbTransactionMasterDetailDto> GetRowByTransactionAndComponent(int companyId, int transactionId, int transactionMasterId, int componentId);

    List<TbTransactionMasterDetailDto> GetRowByTransaction(int companyId, int transactionId, int transactionMasterId);

    List<TbTransactionMasterDetail> GetRowByTransactionToShare(int companyId, int transactionId, int transactionMasterId);

    void DeleteWhereTm(int companyId, int transactionId, int transactionMasterId);

    void DeleteWhereIdNotIn(int companyId, int transactionId, int transactionMasterId, List<int> listTmdId, DataContext? dataContext=null);

    List<TbTransactionMasterDetailDto> GlobalProGetRowBySalesByEmployeerMonthOnlySales(int companyId, DateTime dateFirst,
        DateTime dateLast);

    List<TbTransactionMasterDetailDto> GlobalProGetRowBySalesByEmployeerMonthOnlyTenico(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> GlobalProGetMonthOnlySales(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> GlobalProGetDaySales(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetClienteFuenteDeContacto(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetClientesInteres(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetClientesTipoPropiedad(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetClientesPorAgentes(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetClientesClasificacionPorAgentes(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetClientesCerrados(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetAgenteEfectividad(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetPropiedadesPorAgentes(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetPropiedadesPorAgentesMetas(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetPropiedadesRendimientoAnualVentas(int companyId, DateTime dateFirst, DateTime dateLast);

    List<TbTransactionMasterDetailDto> RealStateGetPropiedadesRendimientoAnualEnlistamiento(int companyId, DateTime dateFirst, DateTime dateLast);
    List<TbTransactionMasterDetailDto> GetRowByShareId(int companyId, int transactionMasterId);
    List<TbTransactionMasterDetailDto> DefaultVentasDeContadoMensuales(int userCompanyId, DateTime dateFirst, DateTime dateLast);
    List<TbTransactionMasterDetailDto> DefaultVentasDeContadoMesActual(int userCompanyId, DateTime dateFirst, DateTime dateLast);
    List<TbTransactionMasterDetailDto> DefaultVentasDeCreditoMesActual(int userCompanyId, DateTime dateFirst, DateTime dateLast);
    List<TbTransactionMasterDetailDto> DefaultPagossMensuales(int userCompanyId, DateTime dateFirst, DateTime dateLast);
}