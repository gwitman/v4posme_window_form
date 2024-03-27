using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ITransactionProfileDetailModel
{
    List<TbTransactionProfileDetailDto> GetByCompanyAndTransactionAndCausal(int companyId,
        int transactionId, int causalId);

    TbTransactionProfileDetail GetByCompanyAndTransactionAndCausalAndProfileDetailId(int companyId,
        int transactionId, int causalId, int profileDetailId);
    
    int DeleteAppPosme(int companyId,int transactionId,int causalId,int profileDetailId);
    
    int InsertAppPosme(TbTransactionProfileDetail data);
}