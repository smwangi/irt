using System.Diagnostics;
using Irt.Application.Configuration.Commands;
using Microsoft.Extensions.Logging;

namespace Irt.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        ICommandHandler<TRequest, TResponse> next) : ICommandHandler<TRequest, TResponse>
        where TRequest :  ICommand<TResponse>
        where TResponse : notnull
    {

        public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            
            logger.LogInformation("[START] Handle request={Request} - Response={Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = Stopwatch.StartNew();
            timer.Start();
            try
            {
                // Calling next() delegate
                var response = await next.HandleAsync(request, cancellationToken: cancellationToken);

                timer.Stop();
                var elapsed = timer.ElapsedMilliseconds;
                if (elapsed > 3000)
                {
                    logger.LogWarning($"[Performance] Long running request: {typeof(TRequest).Name} ({elapsed} milliseconds)");
                }

                logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

                return response;
            }
            catch (Exception e)
            {
                timer.Stop();
                Console.WriteLine(e);
                logger.LogError(e, "IrtError handling {RequestName} after {ElapsedMilliseconds}ms", typeof(TRequest).Name, timer.ElapsedMilliseconds);
                throw;
            }
        }
    }
}