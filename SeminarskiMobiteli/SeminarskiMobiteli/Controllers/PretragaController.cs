using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarskiMobiteli.ViewModel;
using System.Linq;

namespace SeminarskiMobiteli.Controllers
{
    public class PretragaController : Controller
    {
        private readonly Context MojContext;
        public PretragaController(Context ctx)
        {
            MojContext = ctx;
        }
        public IActionResult Index()
        {
			var obj = new AdministratorProizvodVM();
			var query = MojContext.Proizvod.AsQueryable();
			if (!string.IsNullOrEmpty(obj.Naziv))
			{

				query = query.Where(x => x.NazivProizvoda.ToLower().Contains(obj.Naziv.ToLower()));

			}
			obj.Rows = query
			.Select(
				x => new AdministratorProizvodVM.Row
				{
					MobitelId = x.ProizvodID,
					Naziv = x.NazivProizvoda,
					Cijena = x.Cijena,
					Kolicina = x.Kolicina,
					Kategorija = x.kategorija.NazivKategorije,
					imageLocation = x.imageLocation

				}
				).ToList();

			return View(obj);
		}
		public IActionResult Ajax([FromQuery]string naziv) {

		
			
			var query = MojContext.Proizvod.Include(i=>i.kategorija).AsEnumerable();
            if (!string.IsNullOrEmpty(naziv) && !string.IsNullOrWhiteSpace(naziv))
            {
				var parametri = naziv.Split(" ").Select(i => i.ToLower());

				query =query.Where(i => parametri.Any(j=>i.NazivProizvoda.ToLower().Contains(j.ToLower())) || parametri.Contains(i.Memorija.ToLower())
				  || parametri.Contains(i.RamMemorija.ToLower()) || parametri.Contains(i.Kamera.ToLower()) || parametri.Contains(i.VelicinaEkrana.ToLower()));

			}
			var proizvodi=query.Select(
				x => new AdministratorProizvodVM.Row
				{
					MobitelId = x.ProizvodID,
					Naziv = x.NazivProizvoda,
					Cijena = x.Cijena,
					Kolicina = x.Kolicina,
					Kategorija = x.kategorija.NazivKategorije,
					imageLocation = x.imageLocation

				}
				).ToList();
			return PartialView("_Proizvodi",proizvodi);
		}
    }
}
