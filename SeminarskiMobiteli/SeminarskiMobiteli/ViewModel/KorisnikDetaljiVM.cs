using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class KorisnikDetaljiVM
	{
		public string ime { get; set; }
		public string prezime { get; set; }
		public DateTime datum { get; set; }
		public bool pretplacen { get; set; }
		public char spol { get; set; }
		public string KorisnickoIme { get; set; }
		public string Lozinka { get; set; }
	}
}
