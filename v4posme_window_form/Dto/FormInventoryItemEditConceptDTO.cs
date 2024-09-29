using DevExpress.XtraEditors.DXErrorProvider;

namespace v4posme_window.Dto;

public class FormInventoryItemEditConceptDTO(string NameConcept, decimal ValueIn, decimal ValueOut) : IDXDataErrorInfo
{
    
    public void GetPropertyError(string propertyName, ErrorInfo info)
    {
        if (propertyName == "NameConcept" && NameConcept == "" ||
            propertyName == "ValueIn" && ValueIn.CompareTo(decimal.Zero)<=0 ||
            propertyName == "ValueOut" && ValueOut.CompareTo(decimal.Zero)<=0) {
            info.ErrorText = String.Format("The '{0}' field cannot be empty", propertyName);
        }
    }

    public void GetError(ErrorInfo info)
    {
        
    }

    public string NameConcept { get; set; } = NameConcept;
    public decimal ValueIn { get; set; } = ValueIn;
    public decimal ValueOut { get; set; } = ValueOut;
}