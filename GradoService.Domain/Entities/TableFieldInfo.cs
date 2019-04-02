using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities
{
    public class TableFieldInfo
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }
        
        public int FieldTypeId { get; set; }
 
        public TableInfo Table { get; set; }

        public TableFieldType FieldType { get; set; }
    }
}
