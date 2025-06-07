namespace Irt.Application.Common;

public class UserDetails : IUserDetails
{
    public string UserId { get; set; } = "123"; // Default value for demonstration purposes
    public string UserName { get; set; } = "DefaultUser"; // Default value for demonstration purposes
    public string Application { get; set; } = "IrtApplication"; // Default value for demonstration purposes"
    public string IpAddress { get; set; } = "127.0.0.1";
}