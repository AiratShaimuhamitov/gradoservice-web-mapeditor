using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Domain.Entities.File
{
    public class File
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public byte[] Data { get; set; }

        public byte[] ImagePreview { get; set; }

        public string Name { get; set; }

        public bool IsPhoto { get; set; }
    }
}
