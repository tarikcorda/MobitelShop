using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class AdminOglasIndexVM
	{
		public int Id { get; set; }
		public List<Row> Rows { get; set; }
		public class Row {
		public int OglasId { get; set; }
		public string Naslov { get; set; }
		public int Pozicija { get; set; }
		public string Lokacija { get; set; }
		public DateTime DatumObjave { get; set; }
		public DateTime DatumIsteka { get; set; }
		public bool AKtivan { get; set; }
		}
	}
}
