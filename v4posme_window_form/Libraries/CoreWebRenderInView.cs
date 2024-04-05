using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using v4posme_window.Views;

namespace v4posme_window.Libraries
{
    public class CoreWebRenderInView
    {
        public void RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, int displayLength,
            Control form)
        {
            if (dataViewDto.Config is null)
            {
                throw new Exception("Dto is null");
            }

            var gridControl = new GridControl()
            {
                Name = nameGridView,
                Parent = form,
                Dock = DockStyle.Fill
            };
            var viewData = dataViewDto.Data;
            gridControl.DataSource = viewData;
            var gridView = gridControl.MainView as GridView;

            var summaryColumns = dataViewDto.Config.SummaryColumns!.Split(",");
            var formatColumns = dataViewDto.Config.FormatColumns!.Split("");
            var noVisibleColumns = dataViewDto.Config.NonVisibleColumns!.Split(",");
            var visibleColumns = dataViewDto.Config.VisibleColumns!.Split(",");
            var columns = noVisibleColumns.Concat(visibleColumns).ToList();
            if (summaryColumns.Length > 0)
            {
                gridView.OptionsView.ShowFooter = true;
            }

            var agregarLineaSumaRow = summaryColumns;
            for (int i = 0; i < summaryColumns.Length; i++)
            {
                if (summaryColumns[i] == "true")
                {
                    agregarLineaSumaRow.SetValue(0, i);
                }
                else if (i == 0)
                {
                    agregarLineaSumaRow.SetValue("Total", i);
                }
                else
                {
                    agregarLineaSumaRow.SetValue("Total", i);
                }
            }

            var columnsGrid = gridView.Columns.ToList();
            var aux = 0;
            var noVisibleLowerCase = noVisibleColumns.Select(s => s.ToLowerInvariant()).ToList();
            foreach (var column in columnsGrid)
            {
                if (noVisibleLowerCase.Contains(column.Name.ToLowerInvariant()))
                {
                    column.Visible = false;
                }

                if (summaryColumns.Length > 0)
                {
                    if (summaryColumns[aux] == "true")
                    {
                        var summary = new GridColumnSummaryItem
                        {
                            SummaryType = SummaryItemType.Sum,
                            DisplayFormat = agregarLineaSumaRow[aux] + @": {0:#.#}"
                        };
                        column.Summary.Add(summary);
                    }
                }

                aux++;
            }
        }

        public void GetMessageAlert(Image image, string title, string body)
        {
            var toast = new ToastNotification();
            toast.Image = image;
            toast.Body = body;
            toast.Header = title;
            var toastNotificationsManager = new ToastNotificationsManager();
            if (toastNotificationsManager.Notifications.Count > 0)
            {
                toastNotificationsManager.Notifications.Clear();
            }

            toastNotificationsManager.Notifications.Add(toast);
            toastNotificationsManager.ShowNotification(toastNotificationsManager.Notifications[0]);
        }

        public List<AccordionControlElement> RenderMenuLeft(TbCompany company, List<TbMenuElement> data)
        {
            return RenderItemLeft(company, data, null);
        }

        private List<AccordionControlElement> RenderItemLeft(TbCompany company, List<TbMenuElement> data,
            int? parent=null)
        {
            var list = new List<AccordionControlElement>();
            foreach (var menuElement in data)
            {
                if (menuElement.ParentMenuElementId == parent)
                {
                    var x = RenderItemLeft(company, data, parent);
                    var element = new AccordionControlElement();
                    element.Text = menuElement.Display;
                    //en este caso si no se sabe el nivel del menu, no le puedo asignar el tipo al elemento
                    //existen dos tipos para el acordion
                    //element.Style = ElementStyle.Item;
                    //element.Style = ElementStyle.Group;
                    element.Elements.AddRange(x.ToArray());
                    if (!string.IsNullOrEmpty(menuElement.IconWindowForm))
                    {
                        element.Image = new Bitmap(menuElement.IconWindowForm!);
                    }

                    if (!string.IsNullOrEmpty(menuElement.FormRedirectWindowForm))
                    {
                        //en este caso este seria el View, se asigna el nombre del Form
                        //luego se manda a llamar de una lista donde se agina la clase
                        element.Name = menuElement.FormRedirectWindowForm;
                    }

                    list.Add(element);
                }
            }

            return list;
        }
    }
}