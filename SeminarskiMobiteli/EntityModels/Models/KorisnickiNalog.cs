using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Models
{
	public class KorisnickiNalog
	{
		public int ID { get; set; }
		public string KorisnickoIme { get; set; }

		public string Lozinka { get; set; }

		public string TipKorisnickogNaloga { get; set; }
		public string Salt { get; set; }

		public string Hash { get; set; }
		public Korisnik Korisnik { get; set; }

	}
}
