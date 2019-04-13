using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.Exceptions
{
    public class TableNotFoundException : Exception
    {
        public int TableId { get; set; }

        public TableNotFoundException(int id)
            : base($"Table id = \'{id}\' not found.")
        {
            TableId = id;
        }
    }
}
