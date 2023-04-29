using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catstagram.Server.Features.Profiles;

public class ProfilesController : ApiController
{
    private readonly IProfileService profiles;
    private readonly ICurrentUserService currentUser;

    public ProfilesController(IProfileService profiles, ICurrentUserService currentUser)
    {
        this.profiles = profiles;
        this.currentUser = currentUser;
    }

    [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProfileServiceModel>> Mine()
            => await this.profiles.ByUser(this.currentUser.GetId());
}
