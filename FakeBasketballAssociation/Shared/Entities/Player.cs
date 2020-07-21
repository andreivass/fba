using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeBasketballAssociation.Shared.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Required]
        public string NbaId { get; set; }
        public int PlayerVotes { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

    }
}
