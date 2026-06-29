namespace Irt.Application.Common;

public interface IRequireMetadata
{
    string? UserId { get; }
    string? UserName { get; }
    string? Application { get; }
    string? IpAddress { get; }
    void SetMetadata(string userId, string userName, string application, string ipAddress);
}