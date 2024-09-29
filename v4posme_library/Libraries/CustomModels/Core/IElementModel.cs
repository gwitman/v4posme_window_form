using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IElementModel
{
    List<TbElement> GetRowByComponentIdNotIn(List<int> componentId,int elementTypeId);
    
    TbElement? GetRowByName(string? name,int elementTypeId);
    
    List<TbElement>? GetRowByTypeAndLayout(int elementTypeId,int layoutId);
    
    List<TbElement> GetRowByPk(int componentId,int elementTypeId);
}