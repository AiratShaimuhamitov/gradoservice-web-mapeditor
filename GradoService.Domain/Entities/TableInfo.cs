using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities
{
    public class TableInfo
    {
        public TableInfo()
        {
            FieldInfos = new HashSet<TableFieldInfo>();
        }

        public int Id { get; set; }

        public string SchemeMap { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }

        public ICollection<TableFieldInfo> FieldInfos { get; private set; }
    }
}
