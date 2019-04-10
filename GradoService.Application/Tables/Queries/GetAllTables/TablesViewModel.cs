using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Application.Tables.Model;
using GradoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetAllTables
{
    public class TablesViewModel
    {
        public IEnumerable<TableInfoDto> Tables { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
