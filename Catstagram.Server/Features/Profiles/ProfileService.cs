using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Profiles.Models;
using Catstagram.Server.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Profiles;

public class ProfileService : IProfileService
{
    private readonly CatstagramDbContext data;

    public ProfileService(CatstagramDbContext data)
    {
        this.data = data;
    }

    public async Task<ProfileServiceModel> ByUser(string userId)
    {
        return await this.data
            .Users
            .Where(u => u.Id == userId)
            .Select(u => new ProfileServiceModel
            {
                Name = u.Profile.Name,
                Biography = u.Profile.Biography,
                Gender = u.Profile.Gender.ToString(),
                MainPhotoUrl = u.Profile.MainPhotoUrl,
                Website = u.Profile.Website,
                IsPrivate = u.Profile.IsPrivate
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Result> Update(
        string userId,
        string email,
        string userName,
        string name,
        string mainPhotoUrl,
        string website,
        string biography,
        Gender gender,
        bool isPrivate)
    {
        var user = await this.data.Users.FindAsync(userId);

        if (user == null)
        {
            return "User does not exist";
        }

        var result = await this.ChangeProfileEmail(user, userId, email);

        if (result.Failure)
        {
            return result;
        }

        result = await this.ChangeUserName(user, userId, userName);
        if (result.Failure)
        {
            return result;
        }

        this.ChangeProfile(
            user,
            name,
            mainPhotoUrl,
            website,
            biography,
            gender,
            isPrivate);

        await this.data.SaveChangesAsync();

        return true;
    }

    private async Task<Result> ChangeProfileEmail(User user, string userId, string email)
    {
        if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
        {
            var emailExists = await this.data
                .Users
                .AnyAsync(u => u.Id != userId && u.Email == email);

            if (emailExists)
            {
                return "The provided email is already taken";
            }

            user.Email = email;

        }

        return true;
    }

    private async Task<Result> ChangeUserName(User user, string userId, string userName)
    {
        if (!string.IsNullOrWhiteSpace(userName) && user.UserName != userName)
        {
            var userNameExists = await this.data
                .Users
                .AnyAsync(u => u.Id != userId && u.UserName == userName);

            if (userNameExists)
            {
                return "The provided user name is already taken";
            }

            user.UserName = userName;

        }

        return true;
    }

    private void ChangeProfile(
        User user,
        string name,
        string mainPhotoUrl,
        string website,
        string biography,
        Gender gender,
        bool isPrivate)
    {
        if (user.Profile.Name != name)
        {
            user.Profile.Name = name;
        }

        if (user.Profile.MainPhotoUrl != mainPhotoUrl)
        {
            user.Profile.MainPhotoUrl = mainPhotoUrl;
        }
        if (user.Profile.Website != website)
        {
            user.Profile.Website = website;
        }
        if (user.Profile.Biography != biography)
        {
            user.Profile.Biography = biography;
        }
        if (user.Profile.Gender != gender)
        {
            user.Profile.Gender = gender;
        }
        if (user.Profile.IsPrivate != isPrivate)
        {
            user.Profile.IsPrivate = isPrivate;
        }
    }
}
