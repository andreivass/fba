using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FakeBasketballAssociation.Shared.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Vote> Votes { get; set; }
        public int VotesUsed { get; set; }
    }
}
