using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Catstagram.Server.Data.Models;
using static Catstagram.Server.Data.Validation.User;

namespace Catstagram.Server.Features.Profiles.Models;

public class UpdateProfileRequestModel
{
    [EmailAddress]
    public string Email {get;set;}

    public string UserName {get;set;}

    public string? Name { get; set; }

    public string? MainPhotoUrl { get; set; }

    public string? Website { get; set; }

    [MaxLength(MaxBiographyLength)]
    public string? Biography { get; set; }

    public Gender Gender { get; set; }

    public bool IsPrivate { get; set; } = false;
}
