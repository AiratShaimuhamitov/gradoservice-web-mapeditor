using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Metadata
{
    public class MetaTableFieldInfo
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }
        
        public int FieldTypeId { get; set; }
 
        public MetaTableInfo Table { get; set; }

        public MetaTableFieldType FieldType { get; set; }
    }
}
