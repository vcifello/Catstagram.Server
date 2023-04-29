using Catstagram.Server.Data;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Features.Cats.Models;
using Microsoft.EntityFrameworkCore;

namespace Catstagram.Server.Features.Cats;

public class CatService : ICatService
{
    public readonly CatstagramDbContext data;

    public CatService(CatstagramDbContext data)
    {
        this.data = data;

    }
    public async Task<int> Create(string imageUrl, string description, string userId)
    {

        var cat = new Cat
        {
            Description = description,
            ImageUrl = imageUrl,
            UserId = userId
        };

        this.data.Add(cat);

        await this.data.SaveChangesAsync();

        return cat.Id;
    }

    public async Task<IEnumerable<CatListingServiceModel>> ByUser(string userId)

        => await this.data
            .Cats
            .Where(c => c.UserId == userId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new CatListingServiceModel
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl
            })
            .ToListAsync();

    public async Task<CatDetailsServiceModel?> Details(int id)
    => await this.data
            .Cats
            .Where(c => c.Id == id)
            .Select(c => new CatDetailsServiceModel
            {
                Id = c.Id,
                UserId = c.UserId,
                ImageUrl = c.ImageUrl,
                Description = c.Description,
                UserName = c.User!.UserName
            })
            .FirstOrDefaultAsync();

    public async Task<bool> Update(int id, string description, string userId)
    {
        var cat = await this.GetByIdAndUserId(id, userId);

        if (cat == null)
        {
            return false;
        }

        cat.Description = description;

        await this.data.SaveChangesAsync();

        return true;

    }

    public async Task<bool> Delete(int id, string userId)
    {
        var cat = await this.GetByIdAndUserId(id, userId);

        if (cat == null)
        {
            return false;
        }
        this.data.Cats.Remove(cat);

        await this.data.SaveChangesAsync();

        return true;
    }

    private async Task<Cat?> GetByIdAndUserId(int id, string userId)
        => await this.data
                .Cats
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync();

}
