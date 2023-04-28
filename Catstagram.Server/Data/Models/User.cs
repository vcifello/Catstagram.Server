using System.ComponentModel.DataAnnotations;
using Catstagram.Server.Data.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace Catstagram.Server.Data.Models
{
    public class User : IdentityUser, IEntity
    {
        public DateTime CreatedOn { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string? ModifiedBy { get; set; }
        public IEnumerable<Cat> Cats { get; } = new HashSet<Cat>();
    }
}
