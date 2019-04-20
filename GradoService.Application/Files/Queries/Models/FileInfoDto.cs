using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Queries.Models
{
    public class FileInfoDto
    {
        public int Id { get; set; }

        public int ObjectId { get; set; }

        public string Name { get; set; }

        public bool IsPhoto { get; set; }
    }
}
