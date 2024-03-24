using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterDetailModel
{
    int InsertAppPosme(TbTransactionMasterDetail data);

    void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId,
        int transactionMasterDetailId, TbTransactionMasterDetail data);

    TbTransactionMasterDetail GetRowByPk(int companyId, int transactionId, int transactionMasterId,
        int transactionMasterDetailId, int componentId = 33);

    TbTransactionMasterDetail GetRowByTransactionAndItems(int companyId, int transactionId,
        int transactionMasterId, List<int> listTmdId);

    List<TbTransactionMasterDetail> GetRowByTransactionAndWarehouse(int companyId, int transactionId,
        int transactionMasterId);

    List<TbTransactionMasterDetail> GetRowByTransactionAndComponent(int companyId, int transactionId,
        int transactionMasterId, int componentId);

    List<TbTransactionMasterDetail> GetRowByTransaction(int companyId, int transactionId, int transactionMasterId);

    List<TbTransactionMasterDetail> GetRowByTransactionToShare(int companyId, int transactionId,
        int transactionMasterId);

    void DeleteWhereTm(int companyId, int transactionId, int transactionMasterId);

    void DeleteWhereIdNotIn(int companyId, int transactionId, int transactionMasterId, List<int> listTmdId);

    List<TbTransactionMasterDetail> GlobalProGetRowBySalesByEmployeerMonthOnlySales(int companyId, DateTime dateFirst,
        DateTime dateLast);
    
    List<TbTransactionMasterDetail> GlobalProGetRowBySalesByEmployeerMonthOnlyTenico(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  GlobalProGetMonthOnlySales(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  GlobalProGetDaySales(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetClienteFuenteDeContacto(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetClientesInteres(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetClientesTipoPropiedad(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetClientesPorAgentes(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetClientesClasificacionPorAgentes(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetClientesCerrados(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetAgenteEfectividad(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetPropiedadesPorAgentes(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetPropiedadesPorAgentesMetas(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetPropiedadesRendimientoAnualVentas(int companyId, DateTime dateFirst, DateTime dateLast);
    
    List<TbTransactionMasterDetail>  RealStateGetPropiedadesRendimientoAnualEnlistamiento(int companyId, DateTime dateFirst, DateTime dateLast);
}