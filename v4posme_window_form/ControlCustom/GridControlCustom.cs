using System.ComponentModel;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;

namespace v4posme_window.ControlCustom;

[ToolboxItem(true)]
public class GridControlCustom : GridControl
{
    protected override BaseView CreateDefaultView()
    {
        return CreateView("CustomGridView");
    }

    protected override void RegisterAvailableViewsCore(InfoCollection collection)
    {
        base.RegisterAvailableViewsCore(collection);
        collection.Add(new CustomGridView1InfoRegistrator());
    }
}

public class CustomGridView1InfoRegistrator : GridInfoRegistrator
{
    public override string ViewName => "CustomGridView";

    public override BaseView CreateView(GridControl grid)
    {
        return new CustomGridView(grid);
    }

    public override BaseViewHandler CreateHandler(BaseView view)
    {
        return new CustomGridView1Handler((view as CustomGridView)!);
    }
}

public class CustomGridView : DevExpress.XtraGrid.Views.Grid.GridView
{
    public CustomGridView()
    {
    }

    public CustomGridView(GridControl grid) : base(grid)
    {
    }

    protected override string ViewName => "CustomGridView";

    protected override List<IDataColumnInfo> GetFindToColumnsCollection()
    {
        List<IDataColumnInfo> res = base.GetFindToColumnsCollection();
        foreach (GridColumn column in Columns) {
            if(!column.Visible && column.GroupIndex == -1) {
                res.Add(CreateIDataColumnInfoForFilter(column));
            }
        }
        return res;
    }
}

public class CustomGridView1Handler : DevExpress.XtraGrid.Views.Grid.Handler.GridHandler
{
    public CustomGridView1Handler(DevExpress.XtraGrid.Views.Grid.GridView view) : base(view)
    {
    }
}