using System.Data;
using System.IO;
using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid;
using v4posme_library.Libraries;
using v4posme_library.Models;
using v4posme_library.ModelsDto;
using Control = System.Windows.Forms.Control;
using GridView = DevExpress.XtraGrid.Views.Grid.GridView;
using Image = System.Drawing.Image;

namespace v4posme_window.Libraries
{
    public class CoreWebRenderInView
    {
        public static void RenderGrid(TableCompanyDataViewDto dataViewDto, string nameGridView, int displayLength,
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
            if (dataViewDto.Data is null)
            {
                return;
            }

            var viewData = (List<Dictionary<string, object>>)dataViewDto.Data;
            //gridControl.DataSource = viewData;
            var table = FillGridControl(viewData);
            // Asignar el DataTable al GridControl
            gridControl.DataSource = table;

            // Ajustar la configuración del GridView
            var gridView = (GridView)gridControl.MainView;
            gridView.BestFitColumns();

            var summaryColumns = dataViewDto.Config.SummaryColumns is null
                ? []
                : dataViewDto.Config.SummaryColumns.Split(",");

            var formatColumns = dataViewDto.Config.FormatColumns is null
                ? []
                : dataViewDto.Config.FormatColumns.Split("");

            var noVisibleColumns = dataViewDto.Config.NonVisibleColumns is null
                ? []
                : dataViewDto.Config.NonVisibleColumns.Split(",");

            var visibleColumns = dataViewDto.Config.VisibleColumns is null
                ? []
                : dataViewDto.Config.VisibleColumns.Split(",");

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

            noVisibleColumns = noVisibleColumns.Select(s => $"col{s}").ToArray();

            var columnsGrid = gridView.Columns.ToList();
            var aux = 0;
            foreach (var column in columnsGrid)
            {
                if (noVisibleColumns.Contains(column.Name))
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

        private static DataTable? FillGridControl(List<Dictionary<string, object>>? data)
        {
            if (data == null || data.Count == 0)
                return null;

            var table = new DataTable();

            // Añadir columnas al DataTable basadas en las claves del primer diccionario
            foreach (var key in data.First().Keys)
            {
                table.Columns.Add(key, typeof(object));
            }

            // Añadir filas al DataTable
            foreach (var dict in data)
            {
                var row = table.NewRow();
                foreach (var kvp in dict)
                {
                    row[kvp.Key] = kvp.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public void GetMessageAlert(TypeMessage type, string title, string body, Form form)
        {
            var alert = new AlertControl();
            var image = type switch
            {
                TypeMessage.Informacion => Image.FromFile(
                    VariablesGlobales.ConfigurationBuilder["ICON_INFORMACION_PATH"]!),
                TypeMessage.Error => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_ERROR_PATH"]!),
                TypeMessage.Warning => Image.FromFile(VariablesGlobales.ConfigurationBuilder["ICON_WARNING_PATH"]!),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            alert.Show(form, title, body, image);
        }

        public static void RenderMenuLeft(List<TbMenuElement> data, AccordionControl menu)
        {
            // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
            foreach (var menuItem in data)
            {
                if (menuItem.ParentMenuElementId is null)
                {
                    var accordionElement = new AccordionControlElement();
                    accordionElement.Text = menuItem.Display;
                    accordionElement.Style = ElementStyle.Group;
                    if (!string.IsNullOrEmpty(menuItem.IconWindowForm))
                    {
                        accordionElement.ImageOptions.Image = Image.FromFile(menuItem.IconWindowForm);
                        accordionElement.ImageOptions.ImageLayoutMode = ImageLayoutMode.Stretch;
                    }

                    menu.Elements.Add(accordionElement);
                    RenderItemLeft(accordionElement,
                        data.Where(k => k.ParentMenuElementId == menuItem.MenuElementId).ToList());
                }
            }
        }

        private static void RenderItemLeft(AccordionControlElement parentElement, List<TbMenuElement> subItems)
        {
            foreach (var subItem in subItems)
            {
                var subElement = new AccordionControlElement();
                subElement.Text = subItem.Display;
                parentElement.Elements.Add(subElement);
                var subItemsInner = subItems.Where(k => k.ParentMenuElementId == subItem.MenuElementId).ToList();
                // Si el elemento tiene subelementos, llamamos recursivamente a esta función
                if (subItemsInner.Count > 0)
                {
                    subElement.Style = ElementStyle.Group;
                    RenderItemLeft(subElement, subItemsInner);
                }
                else
                {
                    subElement.Style = ElementStyle.Item;
                    if (!string.IsNullOrEmpty(subItem.IconWindowForm))
                    {
                        if (File.Exists(subItem.IconWindowForm))
                        {
                            subElement.ImageOptions.Image = Image.FromFile(subItem.IconWindowForm);
                            subElement.ImageOptions.ImageLayoutMode = ImageLayoutMode.Stretch;
                        }
                    }

                    if (!string.IsNullOrEmpty(subItem.FormRedirectWindowForm))
                    {
                        subElement.Name = subItem.FormRedirectWindowForm;
                    }
                }
            }
        }


        public static void RenderMenuTop(List<TbMenuElement> data, RibbonControl menu)
        {
            // Llamada inicial a la función recursiva para agregar elementos al AccordionControl
            foreach (var menuItem in data)
            {
                if (menuItem.ParentMenuElementId is null)
                {
                    var ribbonPage = new RibbonPage();
                    ribbonPage.Text = menuItem.Display;
                    menu.Pages.Add(ribbonPage);
                    RenderItemTop(ribbonPage,
                        data.Where(k => k.ParentMenuElementId == menuItem.MenuElementId).ToList());
                }
            }
        }

        private static void RenderItemTop(RibbonPage parentElement, List<TbMenuElement> subItems)
        {
            foreach (var subItem in subItems)
            {
                var subItemsInner = subItems.Where(k => k.ParentMenuElementId == subItem.MenuElementId).ToList();
                var subElement = new RibbonPageGroup();
                // Si el elemento tiene subelementos, llamamos recursivamente a esta función
                if (subItemsInner.Count > 0)
                {
                    subElement.Text = subItem.Display;
                    parentElement.Groups.Add(subElement);
                    RenderItemTop(parentElement, subItems);
                }
                else
                {
                    subElement.Text = "";
                    var barButtonItem = new BarLargeButtonItem();
                    barButtonItem.Caption = subItem.Display;
                    if (!string.IsNullOrEmpty(subItem.IconWindowForm))
                    {
                        barButtonItem.ImageOptions.Image = Image.FromFile(subItem.IconWindowForm);
                        barButtonItem.ImageToTextAlignment = BarItemImageToTextAlignment.AfterText;
                    }

                    if (!string.IsNullOrEmpty(subItem.FormRedirectWindowForm))
                    {
                        barButtonItem.Name = subItem.FormRedirectWindowForm;
                    }

                    parentElement.Ribbon.Items.AddRange([parentElement.Ribbon.ExpandCollapseItem, barButtonItem]);
                    subElement.ItemLinks.Add(barButtonItem);
                    parentElement.Groups.Add(subElement);
                }
            }
        }
    }
}