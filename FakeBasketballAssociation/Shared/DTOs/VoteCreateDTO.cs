using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBasketballAssociation.Shared.DTOs
{
    public class VoteCreateDTO
    {
        public virtual int PlayerId { get; set; }
        public string UserName { get; set; }
    }
}
