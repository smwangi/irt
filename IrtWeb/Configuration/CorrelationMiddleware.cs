using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IrtWeb.Configuration
{
    public class CorrelationMiddleware(RequestDelegate next)
    {
        internal const string CorrelationIdHeaderKey = "X-Correlation-Id";
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid(); //context.Request.Headers[CorrelationIdHeader];
            context.Request?.Headers[CorrelationIdHeaderKey] = correlationId.ToString();

            await _next(context);
        }
    }
}
