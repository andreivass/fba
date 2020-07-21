using System;
using System.Collections.Generic;
using System.Text;

namespace FakeBasketballAssociation.Shared.DTOs
{
    public class PlayerBioDataDTO
    {
        public string id { get; set; }
        public string type { get; set; }
        public string display_name { get; set; }
        public string professional { get; set; }
        public string college { get; set; }
        public string highschool { get; set; }
        public string twitter { get; set; }
        public string other_label { get; set; }
        public string other_text { get; set; }
    }
}
