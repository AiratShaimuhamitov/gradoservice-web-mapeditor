using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Metadata
{
    public class MetaTableGeomType
    {
        public int Id { get; set; }

        public string Name { get; set;  }

        public string Type { get; set; }

        public MetaTableInfo TableInfo { get; set; }
    }
}
