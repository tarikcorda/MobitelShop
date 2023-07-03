using EntityModels.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SeminarskiMobiteli.ViewModel
{
    public class KorisnikNarudzbaVM
    {

        public List<Korpa> Korpe { get; set; }
       
        public int DostavljacId { get; set; }
        public List<SelectListItem> Dostavljac { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public int KorisnikId { get; set; }
        public string KorinsikImePrezime { get; set; }

    }
    
}
