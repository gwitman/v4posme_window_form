using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbMenuElement.Metadata))]
    public partial class TbMenuElement
    {
        public partial class Metadata
        {
    
            [Key]
            public object MenuElementID { get; set; }
    
            public object CompanyID { get; set; }
    
            public object ElementID { get; set; }
    
            public object ParentMenuElementID { get; set; }
    
            public object Display { get; set; }
    
            public object Address { get; set; }
    
            public object Orden { get; set; }
    
            public object Icon { get; set; }
    
            public object Template { get; set; }
    
            public object Nivel { get; set; }
    
            public object TypeMenuElementID { get; set; }
    
            public object IsActive { get; set; }
    
            public object IconWindowForm { get; set; }
    
            public object FormRedirectWindowForm { get; set; }
    
            public object TypeUrlRedirect { get; set; }
        }
    }
}
