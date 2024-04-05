using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using v4posme_library.ModelsDto;

namespace v4posme_window.Libraries
{
    public class CoreWebRenderInView
    {
        public GridControl? RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, int displayLength,Form form)
        {
            if (dataViewDto.Config is null)
            {
                throw new Exception("Dto is null");
            }
            var gridControl = new GridControl() {
                Name = nameGridView,
                Parent = form,
                Dock = DockStyle.Fill
            };
            var viewData = dataViewDto.Data;
            gridControl.DataSource = viewData;
            if (gridControl.MainView is not GridView gridView)
            {
                return null;
            }
            var summaryColumns = dataViewDto.Config.SummaryColumns!.Split(",");
            var formatColumns = dataViewDto.Config.FormatColumns!.Split("");
            var noVisibleColumns = dataViewDto.Config.NonVisibleColumns!.Split(",");
            var visibleColumns = dataViewDto.Config.VisibleColumns!.Split(",");
            var columns = noVisibleColumns.Concat(visibleColumns).ToList();

            var agregarLineaSumaRow = summaryColumns;
            for (int i = 0; i < summaryColumns.Length; i++)
            {
                if (summaryColumns[i]=="true")
                {
                    agregarLineaSumaRow.SetValue(0, i);
                }else if (i==0)
                {
                    agregarLineaSumaRow.SetValue("Total", i);
                }
                else
                {
                    agregarLineaSumaRow.SetValue("Total", i);
                }
            }
            var columnsGrid = gridView.Columns.ToList();
            foreach (var item in noVisibleColumns)
            {
                if (columnsGrid.Contains(gridView.Columns[item]))
                {
                    gridView.Columns[item].Visible = false;
                }
                
            }
            return gridControl;
        }
    }
}