using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SeminarskiMobiteli.ViewModel
{
    public class AdministratorProizvodVM
    {
        public string Naziv { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int MobitelId { get; set; }
            public string Naziv { get; set; }
            public string Kategorija { get; set; }
            public double Cijena { get; set; }
            public int Kolicina{ get; set; }
            public string Kamera { get; set; }
            public string RamMemorija { get; set; }
            public string Memorija { get; set; }
            public string VelicinaEkrana { get; set; }
            public string imageLocation { get; set; }


        }
    }
}
