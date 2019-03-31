using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities
{
    public class TableFieldType
    {
        public TableFieldType()
        {
            FieldInfos = new HashSet<TableFieldInfo>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }

        public ICollection<TableFieldInfo> FieldInfos { get; private set; }
    }
}
