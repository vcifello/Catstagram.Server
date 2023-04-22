using System.ComponentModel.DataAnnotations;
using static Catstagram.Server.Data.Validation.Cat;

namespace Catstagram.Server.Models.Cats;

public class CreateCatRequestModel
{
    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;
}
