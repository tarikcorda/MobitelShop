using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EntityModels.Models
{
   public class Korisnik
    {
        public int KorisnikId { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public int TipKorisnikaId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public char Spol { get; set; }
        public string Adresa { get; set; }
      
        public string Email { get; set; }

        public string BrojTelefona { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public Drzava Sjediste { get; set; }
        public int SjedisteId { get; set; }
        public DateTime? PosljednjiLoginDate { get; set; }
        public bool Pretplacen { get; set; }
        [ForeignKey(nameof(KorisnickiNalog))]
        public int? KorisnickiNalogID { get; set; }
        public KorisnickiNalog KorisnickiNalog { get; set; }
        public List<Narudzba> GetNarudzbe()
        
        {
            Context connection = new Context();
            var narudzbe = connection.Narudzba.Where(x => x.NaruciocId == this.KorisnikId).ToList();
            connection.Dispose();
            return narudzbe;
        }
      
       
        public List<Korpa> Korpe { get; set; }


    }
}
