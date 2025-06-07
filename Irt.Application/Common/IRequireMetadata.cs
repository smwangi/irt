namespace Irt.Application.Common;

public interface IRequireMetadata
{
    void SetMetadata(string userId, string userName, string application, string ipAddress);
}