using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Profiles;

public interface IProfileService
{
    Task<ProfileServiceModel> ByUser(string userId);

    Task<Result> Update(
            string userId,
            string Email,
            string UserName,
            string Name,
            string MainPhotoUrl,
            string Website,
            string Biography,
            Gender gender,
            bool IsPrivate);
}
