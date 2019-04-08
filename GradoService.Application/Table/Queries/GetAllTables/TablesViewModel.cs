using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Application.Table.Model;
using GradoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Table.Queries.GetAllTables
{
    public class TablesViewModel
    {
        public IEnumerable<TableDto> Tables { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
