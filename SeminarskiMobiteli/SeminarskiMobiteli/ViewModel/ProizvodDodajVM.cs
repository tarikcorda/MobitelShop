using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
    public class ProizvodDodajVM
    {
        public int MobitelId { get; set; }
        public string Naziv { get; set; }
        public int KategorijaId { get; set; }

        public List<SelectListItem> Kategorija { get; set; }
        public int ProizvodjacId { get; set; }

        public List<SelectListItem> Proizvodjac { get; set; }
        public int UvoznikId { get; set; }
        public List<SelectListItem> Uvoznik { get; set; }
        public float Cijena { get; set; }
        public int Kolicina { get; set; }
        public string RAMMemorija { get; set; }
        public string VelicinaEkrana { get; set; }
        public string Kamera { get; set; }
        public string Memorija { get; set; }


    }
}
