using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Tables.Commands.UpdateData
{
    public class UpdateDataCommandHandler : IRequestHandler<UpdateDataCommand, Unit>
    {
        private readonly TableRepository _tableRepository;
        private readonly GradoServiceDbContext _gradoServiceDbContext;
        private readonly IMapper _mapper;

        public UpdateDataCommandHandler(GradoServiceDbContext dbContext, TableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _gradoServiceDbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateDataCommand command, CancellationToken cancellationToken)
        {
            var tableMeta = await _gradoServiceDbContext.TableInfos.Where(x => x.Id == command.TableId)
                                                               .Include(x => x.FieldInfos)
                                                               .FirstOrDefaultAsync();

            var dataDict = new Dictionary<Field, object>();

            foreach (var fieldMeta in tableMeta.FieldInfos)
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

            await _tableRepository.UpdateData(command.TableId, row);

            return Unit.Value;
        }
    }
}
