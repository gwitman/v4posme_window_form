
using DevExpress.XtraGrid;

namespace v4posme_window.Interfaz
{
    public interface IFormTypeList
    {
        int? DataViewId { get; set; }
        DateTime? Fecha { get; set; }
        GridControl ObjGridControl { get; set; }
        void List();
        void Delete(object? sender, EventArgs? args);
        void Edit(object? sender, EventArgs? args);
        void New(object? sender, EventArgs? args);
        void PreRender();
    }
}
