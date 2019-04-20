using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Metadata
{
    public class MetaTableFileInfo
    {
        public int Id { get; set; }

        public int TableId { get; set; }

        public string TableName { get; set; }

        public string FileField { get; set; }

        public string FieldFile { get; set; }

        public string IdFieldTable { get; set; }

        public string ViewNamePhoto => $"{TableName}_vw_photos";

        public string ViewNameFile => $"{TableName}_vw_files";

        public MetaTableInfo TableInfo { get; set; }
    }
}
