using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarskiMobiteli.ViewModel
{
	public class LoginVM
	{

        [StringLength(100, ErrorMessage = "Korisničko ime mora sadržavati minimalno 3 karaktera!!!", MinimumLength = 3)]
        public string username { get; set; }
        [StringLength(100, ErrorMessage = "Lozinka mora sadržavati minimalno 4 karaktera!!!", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string password { get; set; }
        public bool ZapamtiPassword { get; set; }
        public string Tip { get; set; }
    }
}
