using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class AdminProizvodjac
	{
      
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int ProizvodjacId { get; set; }
            public string Naziv { get; set; }
            public string Drzava { get; set; }



        }
    }
}
