using System;

namespace SeminarskiMobiteli.ViewModel
{
    public class KorisnikOglasDetalji
    {
		public string Naslov { get; set; }
		public int Pozicija { get; set; }
		public string Lokacija { get; set; }
		public string Sadrzaj { get; set; }
		public DateTime DatumObjave { get; set; }
		public DateTime DatumIsteka { get; set; }
		public bool AKtivan { get; set; }
	}
}
