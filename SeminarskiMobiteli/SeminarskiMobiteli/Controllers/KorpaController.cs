using ClassLibrary.Models;
using EntityModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarskiMobiteli.ViewModel;
using System.Linq;
using System.Security.Claims;

namespace SeminarskiMobiteli.Controllers
{
    public class KorpaController : Controller
    {
        private readonly Context _ctx;

        public KorpaController(Context ctx) { _ctx = ctx; }
        public IActionResult Index()
        {




            return View();
        }

        [HttpPost]
        public IActionResult Dodaj(int ProizvodId)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var korisnik = _ctx.Korisnik.Where(i => i.KorisnikId == userId).Include(i => i.Korpe).FirstOrDefault();

            var korpaStavka=korisnik.Korpe.FirstOrDefault(i=>i.ProizvodId==ProizvodId);

            if (korpaStavka != null)
            {
                return RedirectToAction("Korpa", "Proizvod", new { area = "Korisnik" });

            }
            korpaStavka = new Korpa
            {
                ProizvodId = ProizvodId,
                KorisnikId=userId,
                Kolicina=1

                
            };
            _ctx.Add(korpaStavka);
            _ctx.SaveChanges();
            return RedirectToAction("Korpa", "Proizvod", new { area = "Korisnik" });
        }
        public IActionResult Izbrisi(int ProizvodId) {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var korisnik = _ctx.Korisnik.Where(i => i.KorisnikId == userId).Include(i => i.Korpe).FirstOrDefault();

            var korpaStavka = korisnik.Korpe.FirstOrDefault(i => i.ProizvodId == ProizvodId);

            _ctx.Korpa.Remove(korpaStavka);
            _ctx.SaveChanges();
            return RedirectToAction("Korpa", "Proizvod", new { area = "Korisnik" });
        }

        [HttpPost]
        public IActionResult AzurirajKorpu(AzurirajKorpuVM model)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var korisnik = _ctx.Korisnik.Where(i => i.KorisnikId == userId).Include(i => i.Korpe).ThenInclude(i=>i.Proizvod).FirstOrDefault();

           var proizvod= korisnik.Korpe.FirstOrDefault(i => i.ProizvodId ==model.ProizvodId );
            

            var ProizvodNaStanju = _ctx.Proizvod.Where(i => i.ProizvodID == model.ProizvodId).Select(i => i.Kolicina).FirstOrDefault();
            if (model.Povecaj)
            {


                if (ProizvodNaStanju > proizvod.Kolicina)
                {
                    proizvod.Kolicina++;
                    _ctx.Korpa.Update(proizvod);

                }
                
            }
            else 
            {
                if ( proizvod.Kolicina > 1) {
                    proizvod.Kolicina--;
                    _ctx.Korpa.Update(proizvod);
                }
                else
                {
                    _ctx.Korpa.Remove(proizvod);
                    
                }
            }

            

            
            _ctx.SaveChanges();
            var korpaModel = new KorpaIndexVM
            {

                Rows = korisnik.Korpe.Select(
                      x => new KorpaIndexVM.Row
                      {
                          ProizvodId = x.ProizvodId,
                          Slika = x.Proizvod.imageLocation,
                          Cijena = x.Proizvod.Cijena,


                          Naziv = x.Proizvod.NazivProizvoda,
                          Kolicina = x.Kolicina
                      }

                      ).ToList()


            };
            return PartialView("~/Areas/Korisnik/Views/Proizvod/_KorpaProizvodi.cshtml",korpaModel);


        }
    }
}
