using System.Xml.Schema;
using System.ComponentModel.DataAnnotations;
using static Catstagram.Server.Data.Validation.Cat;
using Catstagram.Server.Data.Models.Base;

namespace Catstagram.Server.Data.Models;

public class Cat : DeletableEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(MaxDescriptionLength)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public string UserId { get; set; } = string.Empty;
    public User? User { get; set; }

}
