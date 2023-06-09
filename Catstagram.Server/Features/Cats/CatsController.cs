using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Catstagram.Server.Features.Cats.Models;
using Catstagram.Server.Infrastructure.Extensions;

using static Catstagram.Server.Infrastructure.WebConstants;
using Catstagram.Server.Infrastructure.Services;

namespace Catstagram.Server.Features.Cats;

[Authorize]
public class CatsController : ApiController
{
    private readonly ICurrentUserService currentUser;

    private readonly ICatService cats;

    public CatsController(
        ICatService cats,
        ICurrentUserService currentUser)
    {
        this.cats = cats;
        this.currentUser = currentUser;
    }

    [HttpGet]
    public async Task<IEnumerable<CatListingServiceModel>> Mine()
    {
        var userId = this.currentUser.GetId()!;

        return await this.cats.ByUser(userId);

    }

    [HttpGet]
    [Route(Id)]
    public async Task<ActionResult<CatDetailsServiceModel?>> Details(int id)
        => await this.cats.Details(id);


    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateCatRequestModel model)
    {
        var userId = this.currentUser.GetId();

        var id = await this.cats.Create(
            model.ImageUrl,
            model.Description,
            userId!);

        return Created(nameof(this.Create), id);
    }

    [HttpPut]
    [Route(Id)]
    public async Task<ActionResult> Update(int id, UpdateCatRequestModel model)
    {
        var userId = this.currentUser.GetId();
        var result = await this.cats.Update(
            id,
            model.Description,
            userId);

        if (result.Failure)
        {
            return BadRequest();
        }
        return Ok();
    }

    [HttpDelete]
    [Route(Id)]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = this.currentUser.GetId();

        var result = await this.cats.Delete(id, userId);

        if (result.Failure)
        {
            return BadRequest();
        }

        return Ok();

    }
}
