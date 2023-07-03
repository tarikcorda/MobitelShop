using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class RecenzijaKorisnikVM
	{
        public List<Row> rows { get; set; }
        public float procjecnaOcjena { get; set; }

        public int id { get; set; }
        public class Row
        {
            public int ProizvodID { get; set; }
            public string NazivProizvoda { get; set; }
            public int Ocjena { get; set; }
            public string Komentar { get; set; }



        }
    }
}
