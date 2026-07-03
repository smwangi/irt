using Asp.Versioning;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace IrtWeb.Configuration;

[ApiVersion("1.0")]
public abstract class VersionedODataController : ODataController
{
    
}