using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Models
{
    public class Korpa
    {
        public int Id { get; set; }
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public int Kolicina { get; set; }
    }
}
