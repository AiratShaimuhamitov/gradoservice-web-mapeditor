using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Metadata
{
    public class MetaTableInfo
    {
        public MetaTableInfo()
        {
            FileInfos = new HashSet<MetaTableFileInfo>();
            FieldInfos = new HashSet<MetaTableFieldInfo>();
        }

        public int Id { get; set; }

        public string Schema { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }

        public string Geom { get; set; }

        public string StyleField { get; set; }

        public int GeomType { get; set; }

        public int Type { get; set; }

        public bool DefaultStyle { get; set; }

        public bool ContainsFiles { get; set; }

        public string ViewQuery { get; set; }

        public string PkKey { get; set; }

        public ICollection<MetaTableFileInfo> FileInfos { get; set; }

        public ICollection<MetaTableFieldInfo> FieldInfos { get; private set; }
    }
}
