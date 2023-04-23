using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Catstagram.Server.Infrastructure;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Data;
using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Extensions;

namespace Catstagram.Server.Features.Cats;

[Authorize]
public class CatsController : ApiController
{
    private readonly ICatService catService;

    public CatsController(ICatService catService) => this.catService = catService;

    [HttpGet]
    public async Task<IEnumerable<CatListingServiceModel>> Mine()
    {
        var userId =this.User.GetId()!;

        return await this.catService.ByUser(userId);

    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<CatDetailsServiceModel?>> Details(int id)
        => await this.catService.Details(id);


    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
    {
        var userId = this.User.GetId();

        var id = await this.catService.Create(
            model.ImageUrl,
            model.Description,
            userId!);

        return Created(nameof(this.Create), id);
    }
}
