using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Catstagram.Server.Infrastructure;
using Catstagram.Server.Data.Models;
using Catstagram.Server.Data;

namespace Catstagram.Server.Features.Cats;

public class CatsController : ApiController
{
    private readonly ICatService catService;

    public CatsController(ICatService catService) => this.catService = catService;

    [Authorize]
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
