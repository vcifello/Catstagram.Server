using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catstagram.Server.Infrastructure.Extensions;

namespace Catstagram.Server.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly ClaimsPrincipal user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        => this.user = httpContextAccessor.HttpContext?.User;
    public string GetName()
        => this.user?.Identity?.Name;

    public string GetId()
        => this.user?.GetId();

}
