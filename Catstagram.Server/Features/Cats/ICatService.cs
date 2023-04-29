using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Cats;

public interface ICatService
{
    public Task<int> Create(string imageUrl, string description, string userId);

    public Task<IEnumerable<CatListingServiceModel>> ByUser(string userId);

    public Task<CatDetailsServiceModel?> Details(int id);

    public Task<Result> Update(int id, string description, string userId);

    public Task<Result> Delete(int id, string userId);
}
