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

        public string SchemeName { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }

        public string GeomField { get; set; }

        public string StyleField { get; set; }

        public int GeomType { get; set; }

        public int Type { get; set; }

        public bool DefaultStyle { get; set; }

        public bool ContainsDocument { get; set; }

        public string ViewQuery { get; set; }

        public ICollection<TableFieldInfo> FieldInfos { get; private set; }
    }
}
