using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Infrastructure
{
    /// <summary>
    /// Estimates handle time of each request
    /// </summary>
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            if (_timer.ElapsedMilliseconds > 500) // TODO put this value to configuration
            {
                var name = typeof(TRequest).Name;

                _logger.LogWarning("GradoService Long Running Request: {Name} ({ElaspsedMilliseconds} milliseconds) {@Request}", 
                    name, _timer.ElapsedMilliseconds, request); ;
            }

            return response;
        }
    }
}
