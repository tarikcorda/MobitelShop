using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class ProizvodPrikaziVM
	{
        public int ID { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int ProizvodId { get; set; }
            public string Naziv { get; set; }
            public string Kolicina { get; set; }
            public double Cijena { get; set; }
            public string Opis { get; set; }
        }
         
    }
}

