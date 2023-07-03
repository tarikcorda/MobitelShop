using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using SeminarskiMobiteli.ViewModel;
using System.Linq;

namespace SeminarskiMobiteli.Controllers
{
    public class PretraziController : Controller
    {
        private readonly Context MojContext;

        public PretraziController(Context ctx) { MojContext = ctx; }
		[HttpGet]
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
		[HttpGet]
		public IActionResult Ajax([FromQuery] string naziv)
		{



			var query = MojContext.Proizvod.AsQueryable();
			if (!string.IsNullOrEmpty(naziv))
			{

				query = query.Where(x => x.NazivProizvoda.ToLower().Contains(naziv.ToLower()));


			}
			var proizvodi = query.Select(
				x => new AdministratorProizvodVM.Row
				{
					MobitelId = x.ProizvodID,
					Naziv = x.NazivProizvoda,
					Cijena = x.Cijena,
					Kolicina = x.Kolicina,
					Kategorija = x.kategorija.NazivKategorije,
					Memorija = x.Memorija,
					RamMemorija = x.RamMemorija,
					Kamera = x.Kamera,
					VelicinaEkrana = x.VelicinaEkrana

				}
				).ToList();
			return Ok(proizvodi);

		}
	}
}
