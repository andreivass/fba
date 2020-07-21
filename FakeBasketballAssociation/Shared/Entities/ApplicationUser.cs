using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBasketballAssociation.Shared.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Vote> Votes { get; set; }
        public int VotesUsed { get; set; }
    }
}
