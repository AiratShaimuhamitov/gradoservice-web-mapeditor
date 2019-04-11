using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Table
{
    public class Field
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; } // Can be replaced whith System.Data.SqlDbType type
    }
}
