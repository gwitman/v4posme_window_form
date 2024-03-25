using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanyModel
{
    TbCompany GetRowByPk(int companyId);

    List<TbCompany> GetRows();
    
    List<TbCompany> FnMergeGetRowsDbPosMeMergeRowByCompanyId(int companyId);
    
    int FnMergeUpdateRowsDbPosMe(TbCompany data);
}