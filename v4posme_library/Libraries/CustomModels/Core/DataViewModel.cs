using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class DataViewModel : IDataViewModel
{
    public TbDataview? GetListByCompanyComponentCaller(int componentId, int callerId)
    {
        using var context = new DataContext();
        return context.TbDataviews.AsNoTracking()
            .SingleOrDefault(dataview => dataview!.ComponentId == componentId
                                         && dataview.CallerId == callerId
                                         && dataview.IsActive!.Value);
    }

    public TbDataview? GetViewByName(int componentId, string name, int callerId)
    {
        using var context = new DataContext();
        var resultado =  context.TbDataviews.AsNoTracking().FirstOrDefault(dataview => dataview!.ComponentId == componentId
                                         && dataview.CallerId == callerId
                                         && dataview.Name == name
                                         && dataview.IsActive!.Value);

        return resultado;

    }
}