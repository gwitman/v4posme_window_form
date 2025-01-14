﻿using System.ComponentModel.Design;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionMasterModel
{
    void DeleteAppPosme(int companyId, int transactionId, int transactionMasterId);

    int InsertAppPosme(TbTransactionMaster? data);

    void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, TbTransactionMaster? data, DataContext? dataContext = null);

    TbTransactionMasterDto? GetRowByPk(int companyId, int transactionId, int transactionMasterId);

    TbTransactionMaster? GetRowByPKK(int transactionMasterId);

    TbTransactionMaster? GetRowByTransactionMasterId(int companyId, int transactionMasterId);

    TbTransactionMaster? GetRowByTransactionNumber(int companyId, string? transactionNumber);

    List<TbTransactionMaster> GetRowByNotification(int companyId);

    List<TbTransactionMasterDto> GetRowInStatusRegister(int companyId, int transactionMasterId);

    List<TbTransactionMaster> getRowByTransactionIDAndEntityID(int companyID, int transactionID, int entityID);

    List<TbTransactionMasterDto> GetRowbyNumberExoneration(int userCompanyId, string exonerationNumber);
}