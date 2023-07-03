using System.Collections.Generic;

namespace SeminarskiMobiteli.ViewModel
{
    public class NarudzbePrikaziVM
    {
        public List<Row>Rows { get; set; }

        public class Row {

            public string Naziv { get; set; }
            public double Cijena { get; set; }
            public int Kolicina { get; set; }
            public double Ukupno { get; set; }
            public string Datum { get; set; }
        }
    }
}
