namespace Irt.Application.Common;

public interface IUserDetails
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Application { get; set; }
}