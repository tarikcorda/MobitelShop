using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
    public class AdminPostDodajVM
    {
        public int PostId { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
      
        public int AutorId { get; set; }
        public string Autor { get; set; }
        public string ImageLocation { get; set; }
        public DateTime DatumObjave { get; set; }
    }
}
