using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbWorkflow.Metadata))]
    public partial class TbWorkflow
    {
        public partial class Metadata
        {
    
            [Key]
            public object WorkflowID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object IsActive { get; set; }
        }
    }
}
