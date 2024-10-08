using DevExpress.XtraEditors;

namespace v4posme_window.Libraries;

public class HelperMethods
{
    public static void OnlyNumberDecimals(TextEdit textEdit)
    {
        textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;  
        textEdit.Properties.Mask.EditMask = @"n2";  
        textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;  
    }

    public static void OnlyNumberInt(TextEdit textEdit)
    {
        textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;  
        textEdit.Properties.Mask.EditMask = @"n0";  
        textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;  
    }

    public static void FnClearTextEdits(Control control)
    {
        foreach (Control c in control.Controls)
        {
            if (c is DevExpress.XtraEditors.TextEdit textEdit)
            {
                textEdit.Text = string.Empty;
            }
            else if (c.HasChildren)
            {
                // Recursively clear TextEdit controls in child containers
                FnClearTextEdits(c);
            }
        }
    }
}