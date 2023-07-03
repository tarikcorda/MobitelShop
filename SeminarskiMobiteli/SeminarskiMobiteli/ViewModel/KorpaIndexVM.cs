using System.Collections.Generic;

namespace SeminarskiMobiteli.ViewModel
{
    public class KorpaIndexVM
    {
        public List<Row> Rows { get; set; }

        
        public class Row {

            public int ProizvodId { get; set; }
            public string Slika { get; set; }
            public string Naziv { get; set; }

            public double Cijena { get; set; }

            public int Kolicina { get; set; }
            

            public double Ukupno { get; set; }


        }
    }
}
