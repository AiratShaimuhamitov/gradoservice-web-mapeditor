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

namespace GradoService.Application.Tables.Commands.DeleteData
{
    public class DeleteDataCommandHandler : IRequestHandler<DeleteDataCommand, Unit>
    {
        private readonly TableRepository _tableRepository;
        private readonly GradoServiceDbContext _gradoServiceDbContext;

        public DeleteDataCommandHandler(GradoServiceDbContext dbContext, TableRepository tableRepository)
        {
            _tableRepository = tableRepository;
            _gradoServiceDbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteDataCommand command, CancellationToken cancellationToken)
        {
            var tableMeta = _gradoServiceDbContext.TableInfos.Where(x => x.Id == command.TableId)
                                                                .Include(x => x.FieldInfos)
                                                                .FirstOrDefault();

            if (tableMeta == null) { throw new NotFoundException("Table", command.TableId.ToString()); }

            await _tableRepository.DeleteData(command.TableId, command.RowId);

            return Unit.Value;
        }
    }
}
