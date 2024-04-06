using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        public void RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, int displayLength,System.Windows.Forms.Control form)
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
            var gridView = gridControl.MainView as DevExpress.XtraGrid.Views.Grid.GridView;

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

        public void GetMessageAlert(string type, string title, string body)
        {
            var toast = new ToastNotification();

            //type: information
            //type: error
            //type: worrarging
            //leer la variable de configuracion
            //ICON_ERROR_PATH 
            //toast.Image = image; 


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

        public  void RenderMenuLeft(TbCompany company, List<TbMenuElement> data,AccordionControl menu)
        {
            // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
            foreach (var menuItem in data)
            {
                AccordionControlElement accordionElement = new AccordionControlElement();
                accordionElement.Text = menuItem.Display;
                menu.Elements.Add(accordionElement);
                RenderItemLeft(accordionElement, data.Where(k => k.ParentMenuElementId == menuItem.MenuElementId).ToList());
            }


        }


        private void RenderItemLeft(AccordionControlElement parentElement, List<TbMenuElement> subItems)
        {
            foreach (var subItem in subItems)
            {
                AccordionControlElement subElement = new AccordionControlElement();
                subElement.Text = subItem.Display;
                parentElement.Elements.Add(subElement);
                List<TbMenuElement> subItemsInner = subItems.Where(k => k.ParentMenuElementId == subItem.MenuElementId).ToList();

                // Si el elemento tiene subelementos, llamamos recursivamente a esta función
                if (subItemsInner != null && subItemsInner.Count > 0)
                {
                    RenderItemLeft(subElement, subItemsInner);
                }
                else
                {
                    // abrir formularios
                }
            }
        }


        ///elimninar esta funcion 
        //private void RenderItemLeft2(TbCompany company, List<TbMenuElement> data, AccordionControl menu,int? parent=null )
        //{
        //    var list = new List<AccordionControlElement>();
        //    foreach (var menuElement in data)
        //    {
        //        if (menuElement.ParentMenuElementId == parent)
        //        {
        //            RenderItemLeft(company, data, menu, menuElement.MenuElementId);
        //
        //
        //            var element     = new AccordionControlElement();
        //            element.Text    = menuElement.Display;
        //            //en este caso si no se sabe el nivel del menu, no le puedo asignar el tipo al elemento
        //            //existen dos tipos para el acordion
        //
        //            if(menuElement.ParentMenuElementId is null)                    
        //            element.Style = ElementStyle.Group;
        //            else
        //            element.Style = ElementStyle.Item;
        //
        //            if (!string.IsNullOrEmpty(menuElement.IconWindowForm))
        //            {
        //                element.Image = new Bitmap(menuElement.IconWindowForm!);
        //            }
        //
        //
        //            if(element.Style is ElementStyle.Group)
        //            {
        //
        //            }
        //
        //            if (!string.IsNullOrEmpty(menuElement.FormRedirectWindowForm))
        //            {
        //                //en este caso este seria el View, se asigna el nombre del Form
        //                //luego se manda a llamar de una lista donde se agina la clase
        //                element.Name = menuElement.FormRedirectWindowForm;
        //            }
        //
        //            
        //                
        //            menu.Elements.Add(element);
        //        }
        //    }
        //
        //
        //    
        //}
    }
}