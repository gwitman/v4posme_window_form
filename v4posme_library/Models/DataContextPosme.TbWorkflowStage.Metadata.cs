using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbWorkflowStage.Metadata))]
    public partial class TbWorkflowStage
    {
        public partial class Metadata
        {
    
            [Key]
            public object WorkflowStageID { get; set; }
    
            public object ComponentID { get; set; }
    
            public object WorkflowID { get; set; }
    
            public object Name { get; set; }
    
            public object Description { get; set; }
    
            public object Display { get; set; }
    
            public object FlavorID { get; set; }
    
            public object EditableParcial { get; set; }
    
            public object EditableTotal { get; set; }
    
            public object Eliminable { get; set; }
    
            public object Aplicable { get; set; }
    
            public object Vinculable { get; set; }
    
            public object IsActive { get; set; }
    
            public object IsInit { get; set; }
        }
    }
}
