using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Table
{
    public class Table
    {
        public Table()
        {
            Fields = new List<Field>();
            Rows = new List<Row>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Schema { get; set; }

        public string Key { get; set; }

        public IEnumerable<Field> Fields { get; set; }

        public IEnumerable<Row> Rows { get; set; }
    }
}
