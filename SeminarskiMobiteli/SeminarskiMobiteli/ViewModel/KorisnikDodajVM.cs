using EntityModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace SeminarskiMobiteli.ViewModel
{
    public class KorisnikDodajVM
    {

        [Required(ErrorMessage ="Polje je obavezno")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Prezime { get; set; }
        public char Spol { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public int SjedisteID { get; set; }
        public List<SelectListItem> Sjediste { get; set; }
        public bool Pretplacen { get; set; }
        public int TipKorisnikaID { get; set; }
        public List<SelectListItem> TipKorisnika { get; set; }
       
        
        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Unesite ispravnu email adresu")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Unesite broj telefona u ispravnom formatu")]
        public string BrojTelefona { get; set; }

        [Required(ErrorMessage = "Polje je obavezno")]
        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati minimalno 3 karaktera!!!", MinimumLength = 3)]
        public string KorisnickoIme { get; set; }
        
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati minimalno 4 karaktera!!!", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Polje je obavezno")]
        public string Lozinka { get; set; }



    }
}
