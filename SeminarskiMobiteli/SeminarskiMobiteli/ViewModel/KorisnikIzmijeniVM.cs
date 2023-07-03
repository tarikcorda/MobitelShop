using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SeminarskiMobiteli.ViewModel
{
    public class KorisnikIzmijeniVM
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int SjedisteId { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public List<SelectListItem> Sjediste { get; set; }

    }
}
