using System;

namespace FakeBasketballAssociation.Shared.Entities
{
    public class Vote
    {
        public int VoteId { get; set; }

        public virtual Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual int PlayerId { get; set; }
        public virtual Player Player { get; set; }
    }
}
