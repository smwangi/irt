using Microsoft.AspNetCore.Http;

namespace Irt.Application.Common;

public class HttpContextUserDetails(IHttpContextAccessor contextAccessor) : IUserDetails
{
    public string? UserId => contextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
    public string? UserName => contextAccessor.HttpContext?.User?.Identity?.Name;
    public string? Application => contextAccessor.HttpContext?.User?.Identity?.Name;
    public string? IpAddress => contextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
}