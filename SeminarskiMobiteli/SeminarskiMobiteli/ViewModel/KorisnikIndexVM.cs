using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace SeminarskiMobiteli.ViewModel
{
    public class KorisnikIndexVM
    {

        public List<Row> Rows { get; set; }

        public class Row { 
        
        public int KorisnikID { get; set; }
        public string TipKorisnika { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public char Spol { get; set; }
            public DateTime DatumRegistracije { get; set; }

            public bool Pretplacen { get; set; }



        }
    }
}
