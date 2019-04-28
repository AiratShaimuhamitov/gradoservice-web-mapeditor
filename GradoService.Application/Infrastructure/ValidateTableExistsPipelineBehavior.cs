using GradoService.Application.Exceptions;
using GradoService.Application.Interfaces;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Infrastructure
{
    public class ValidateTableExistsPipelineBehavior<TRquest, TResponse> : IPipelineBehavior<TRquest, TResponse>
        where TRquest : ITableRequest
    {
        private readonly GradoServiceDbContext dbContext;

        public ValidateTableExistsPipelineBehavior(GradoServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRquest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var table = dbContext.TableInfos.Where(x => x.Id == request.TableId)
                                    .FirstOrDefault();

            if (table == null)
            {
                throw new NotFoundException("Table", request.TableId.ToString());
            }

            return await next();
        }
    }
}
