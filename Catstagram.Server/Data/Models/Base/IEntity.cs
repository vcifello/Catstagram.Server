using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catstagram.Server.Data.Models.Base;

public interface IEntity
{
    DateTime CreatedOn { get; set; }

    string? CreatedBy { get; set; }

    DateTime? ModifiedOn { get; set; }

    string? ModifiedBy { get; set; }
}
