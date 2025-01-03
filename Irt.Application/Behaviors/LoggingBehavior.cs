using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Irt.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = Stopwatch.StartNew();
            timer.Start();

            // Calling next() delegate
            var response = await next();

            timer.Stop();
            var elapsed = timer.ElapsedMilliseconds;
            if (elapsed > 3000)
            {
                _logger.LogWarning($"[Performance] Long running request: {typeof(TRequest).Name} ({elapsed} milliseconds)");
            }

            _logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}