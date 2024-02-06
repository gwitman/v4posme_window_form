using System.Collections.Generic;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public interface IElementSevice
    {
        List<Element> getRowByTypeAndLayout(int elementTypeId, int layoutId);
    }
}