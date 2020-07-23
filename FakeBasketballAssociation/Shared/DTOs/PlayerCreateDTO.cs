using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FakeBasketballAssociation.Shared.DTOs
{
    public class PlayerCreateDTO
    {
        [Required]
        [MinLength(4)]
        [MaxLength(7)]
        public string NbaId { get; set; }
    }
}
