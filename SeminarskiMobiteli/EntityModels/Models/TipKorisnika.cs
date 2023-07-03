using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Models
{
    public enum Uloga { Administrator, User }
    public class TipKorisnika
    {
        public int TipKorisnikaId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }


    }
}
