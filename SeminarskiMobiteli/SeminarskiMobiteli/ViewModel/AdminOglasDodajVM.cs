using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class AdminOglasDodajVM
	{
		public int ProizvodjacId { get; set; }
		public string Naslov { get; set; }
		public int Pozicija { get; set; }
		public string Lokacija { get; set; }
		public DateTime DatumObjave { get; set; }
		public DateTime DatumIsteka { get; set; }
		public bool AKtivan { get; set; }
		public string Sadrzaj { get; set; }
		public int Trajanje { get; set; }
	}
}
