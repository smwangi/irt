namespace Irt.Application.Common;

public interface IUserDetails
{
    string? UserId { get; }
    string? UserName { get; }
    string? Application { get; }
    string? IpAddress { get; }
}