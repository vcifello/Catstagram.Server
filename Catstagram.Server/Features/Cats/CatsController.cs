using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Catstagram.Server.Infrastructure;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Data;
using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Extensions;
using static Catstagram.Server.Infrastructure.WebConstants;

namespace Catstagram.Server.Features.Cats;

[Authorize]
public class CatsController : ApiController
{
    private readonly ICatService catService;

    public CatsController(ICatService catService) => this.catService = catService;

    [HttpGet]
    public async Task<IEnumerable<CatListingServiceModel>> Mine()
    {
        var userId = this.User.GetId()!;

        return await this.catService.ByUser(userId);

    }

    [HttpGet]
    [Route(Id)]
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

    [HttpPut]
    public async Task<ActionResult> Update(UpdateCatRequestModel model)
    {
        var userId = this.User.GetId();
        var updated = await this.catService.Update(
            model.Id,
            model.Description,
            userId);

        if (!updated)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    [Route(Id)]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = this.User.GetId();

        var deleted = await this.catService.Delete(id, userId);

        if (!deleted)
        {
            return BadRequest();
        }
        
        return Ok();

    }
}
