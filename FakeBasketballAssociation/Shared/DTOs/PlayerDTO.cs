using System;
using System.Collections.Generic;
using System.Text;
using FakeBasketballAssociation.Shared.Entities;

namespace FakeBasketballAssociation.Shared.DTOs
{
    public class PlayerDTO
    {
        public Player Player { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ppg { get; set; }
        public string Rpg { get; set; }
        public string Bpg { get; set; }
        public string Spg { get; set; }
        public string Apg { get; set; }
        public string Fgp { get; set; }
        public string NbaId { get; set; }
        public int VotesTemp { get; set; }
        public int PlayerId { get; set; }
    }
}
