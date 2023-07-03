using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
    public class AdminPostIndexVM
    {
        public int Id { get; set; }
        public List<Row> Rows { get; set; }
        public class Row
        {
            public int PostId { get; set; }
            public string Naslov { get; set; }
            public string Sadrzaj { get; set; }
            public string Autor { get; set; }
            public int AutorId { get; set; }
            public DateTime Datum { get; set; }

        }
    }
}
