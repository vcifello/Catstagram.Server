using Catstagram.Server.Data.Models;

//using static Catstagram.Server.Data.Validation.User;

namespace Catstagram.Server.Features.Profiles.Models;

public class ProfileServiceModel
{

    public string? Name { get; set; }

    public string? MainPhotoUrl { get; set; }

    public string? Website { get; set; }

    //[MaxLength(MaxBiographyLength)]
    public string? Biography { get; set; }

    public string Gender { get; set; }

    public bool IsPrivate { get; set; } = false;

}
