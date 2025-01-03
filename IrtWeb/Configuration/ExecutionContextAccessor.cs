using Irt.Application;
using Microsoft.AspNetCore.Http;
using System;

namespace IrtWeb.Configuration
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid CorrelationId
        {
            get
            {
                if (IsAvailable && _httpContextAccessor.HttpContext?.Request.Headers.Keys.Any(x => x == CorrelationMiddleware.CorrelationIdHeaderKey) == true)
                {
                    string? correlationIdHeader = _httpContextAccessor?.HttpContext?.Request?.Headers[CorrelationMiddleware.CorrelationIdHeaderKey];
                    if (!string.IsNullOrEmpty(correlationIdHeader))
                    {
                        return Guid.Parse(correlationIdHeader);
                    }
                }
                throw new ApplicationException("Http context and CorrelationId is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}