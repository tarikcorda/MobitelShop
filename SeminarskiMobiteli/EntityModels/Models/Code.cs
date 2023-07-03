using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Models
{
    public class Code
    {
        public int ID { get; set; }
        public int kod { get; set; }
        public bool IsValid { get; set; }
        public DateTime VrijemeSlanja { get; set; }
    }
}
