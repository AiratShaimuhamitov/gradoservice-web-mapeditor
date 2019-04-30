using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GradoService.Domain.Entities.Metadata
{
    public class MetaTableFieldType
    {
        public MetaTableFieldType()
        {
            FieldInfos = new HashSet<MetaTableFieldInfo>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public ICollection<MetaTableFieldInfo> FieldInfos { get; private set; }
    }
}
