using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IDataViewModel
{
    TbDataview? GetListByCompanyComponentCaller(int componentId,int callerId);
    
    TbDataview? GetViewByName(int componentId, string name, int callerId);
}