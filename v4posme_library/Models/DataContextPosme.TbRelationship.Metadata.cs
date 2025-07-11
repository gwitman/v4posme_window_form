﻿using System.ComponentModel.DataAnnotations;

namespace v4posme_library.Models
{
    [MetadataType(typeof(TbRelationship.Metadata))]
    public partial class TbRelationship
    {
        public partial class Metadata
        {
    
            [Key]
            public object RelationshipID { get; set; }
    
            public object EmployeeID { get; set; }
    
            public object CustomerID { get; set; }
    
            public object StartOn { get; set; }
    
            public object EndOn { get; set; }
    
            public object IsActive { get; set; }
    
            public object OrderNo { get; set; }
    
            public object Reference1 { get; set; }
    
            public object Reference2 { get; set; }
    
            public object Reference3 { get; set; }
    
            public object CustomerIDAfter { get; set; }
        }
    }
}
