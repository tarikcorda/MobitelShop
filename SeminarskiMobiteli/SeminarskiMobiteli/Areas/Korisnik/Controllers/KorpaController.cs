using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeminarskiMobiteli.ViewModel;
using System.Linq;

namespace SeminarskiMobiteli.Areas.Korisnik.Controllers
{
    [Authorize(Roles = "User")]
    public class KorpaController : Controller
    {
        private readonly Context ctx;

        public KorpaController(Context _ctx) { ctx = _ctx; }
        public IActionResult Index(int id)
        {

            var proizvod = ctx.Proizvod.Find(id);

            var model = new KorpaIndexVM {
                
                Rows = ctx.Proizvod.Where(i => i.ProizvodID == id).Select(
                    x => new KorpaIndexVM.Row
                    {
                        ProizvodId = proizvod.ProizvodID,
                        Slika = proizvod.imageLocation,
                        Cijena=proizvod.Cijena,
                        Kolicina=proizvod.Kolicina,
                        Ukupno = proizvod.Cijena * proizvod.Kolicina,
                        Naziv =proizvod.NazivProizvoda,
                        
                    }
                    
                    ).ToList()
            
            
            };


            return View(model);
        }
    }
}
