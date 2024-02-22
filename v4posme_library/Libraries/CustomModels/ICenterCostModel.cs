using System.ComponentModel.Design;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICenterCostModel
{
    void DeleteAppPosme(int companyId, int classId);

    void UpdateAppPosme(int companyId, int classId, TbCenterCost data);

    int InsertAppPosme(TbCenterCost data);

    TbCenterCost GetByClassNumber(string classNumber, int companyId);

    TbCenterCost GetRowByPk(int companyId,int classId);

    List<TbCenterCost> GetByCompany(int companyId);
}