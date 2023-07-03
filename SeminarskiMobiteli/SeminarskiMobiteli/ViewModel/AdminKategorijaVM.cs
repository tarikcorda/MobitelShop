using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace SeminarskiMobiteli.ViewModel
{
	public class AdminKategorijaVM
	{
        public string Naziv { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int KategorijaId { get; set; }
            public string Naziv { get; set; }
             


        }
    }
}
