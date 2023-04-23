using System.ComponentModel.DataAnnotations;
using static Catstagram.Server.Data.Validation.Cat;

namespace Catstagram.Server.Features.Cats;

public class CreateCatRequestModel
{
    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = string.Empty;

}
