using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBasketballAssociation.Shared.DTOs
{
    public class PlayerBioDTO
    {
        public int Id { get; set; }
        public PlayerBioDataDTO bio { get; set; }
    }
}
