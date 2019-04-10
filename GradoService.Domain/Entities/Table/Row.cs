﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.Table
{
    public class Row
    {
        public int TableId { get; set; }

        public Dictionary<string, object> Data { get; set; }

        public Row()
        {
            Data = new Dictionary<string, object>();
        }
    }
}
