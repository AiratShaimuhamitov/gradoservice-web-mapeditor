using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Tables.Commands.InsertData
{
    public class InsertDataCommandHandler : IRequestHandler<InsertDataCommand, int>
    {
        private readonly TableRepository _tableRepository;
        private readonly GradoServiceDbContext _gradoServiceDbContext;
        private readonly IMapper _mapper;

        public InsertDataCommandHandler(GradoServiceDbContext dbContext, TableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _gradoServiceDbContext = dbContext;
            _mapper = mapper;
        }

        public Task<int> Handle(InsertDataCommand command, CancellationToken cancellationToken)
        {
            var tableMeta = _gradoServiceDbContext.TableInfos.Where(x => x.Id == command.TableId)
                                                                .Include(x => x.FieldInfos)
                                                                .FirstOrDefault();

            if (tableMeta == null) { throw new NotFoundException("Table", command.TableId.ToString()); }

            var dataDict = new Dictionary<Field, object>();

            foreach(var fieldMeta in tableMeta.FieldInfos)
            {
                command.Row.TryGetValue(fieldMeta.Name, out object obj);

                var field = _mapper.Map<Field>(fieldMeta);

                if (obj != null)
                    dataDict[field] = obj;
                else
                    dataDict[field] = null;
            }

            var row = new Row
            {
                TableId = command.TableId,
                Data = dataDict
            };
            var insertedRowId = _tableRepository.InsertData(command.TableId, row);

            return insertedRowId;
        }
    }
}
