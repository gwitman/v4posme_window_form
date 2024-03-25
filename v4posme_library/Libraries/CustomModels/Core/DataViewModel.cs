using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class DataViewModel : IDataViewModel
{
    public TbDataview GetListByCompanyComponentCaller(int componentId, int callerId)
    {
        using var context = new DataContext();
        return context.TbDataviews
            .First(dataview => dataview.ComponentId == componentId
                               && dataview.CallerId == callerId
                               && dataview.IsActive!.Value);
    }

    public TbDataview GetViewByName(int componentId, string name, int callerId)
    {
        using var context = new DataContext();
        return context.TbDataviews
            .First(dataview => dataview.ComponentId == componentId
                               && dataview.CallerId == callerId
                               && dataview.Name == name
                               && dataview.IsActive!.Value);
    }
}