using DevExpress.XtraEditors.DXErrorProvider;
using System.ComponentModel;

namespace v4posme_window.Dto;

public class FormCustomerEditCustomerFrecuencyActuationDTO : IDXDataErrorInfo
{
    public int? EntityID { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string? Name { get; set; }
    public string? SituationDisplay { get; set; }
    public string? FrecuencyDisplay { get; set; }

    public int? SituationID { get; set; }

    public int? FrecuencyContactID { get; set; }
    public int? CustomerFreFrecuencyActuations { get; set; }
    public int? IsApply { get; set; }
    public int? IsActive { get; set; }

    public void GetPropertyError(string propertyName, ErrorInfo info)
    {
        if (propertyName == "Name" && Name == "" ||
            propertyName == "SituationDisplay" && SituationDisplay == "" ||
            propertyName == "FrecuencyDisplay" && FrecuencyDisplay == "") {
            info.ErrorText = "El campo no puede estar vacio";
        }
    }

    public void GetError(ErrorInfo info)
    {
        
    }
}