using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModels.Models
{
	public class TabelaGreska
	{
		public int ID { get; set; }
		public string greska { get; set; }

		public DateTime VrijemeGreske { get; set; }
	}
}
