using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeminarskiMobiteli.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace SeminarskiMobiteli.Areas.Korisnik.Controllers
{
	[Route("api/proizvodi")]
    public class KorisnikAngular : Controller
    {
        private readonly Context _ctx;

        public KorisnikAngular(Context ctx) { _ctx = ctx; }
        [Route("")]
			public IActionResult Index(string naziv)
			{
				var obj = new AdministratorProizvodVM();
				var query = _ctx.Proizvod.AsQueryable();
				if (!string.IsNullOrEmpty(naziv))
				{

					query = query.Where(x => x.NazivProizvoda.ToLower().Contains(naziv.ToLower()));

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

				return Json(obj);
			}
		[Route("izbrisi/{id}")]
		[HttpPost]
		public IActionResult Izbrisi(int id)
		{
			List<Recenzija> rec = _ctx.Recenzija
				.Where(x => x.ProizvodId == id).ToList();
			List<NarudzbaStavka> nas = _ctx.NarudzbaStavka
				.Where(x => x.ProizvodId == id).ToList();

			_ctx.Recenzija.RemoveRange(rec);
			_ctx.NarudzbaStavka.RemoveRange(nas);

			Proizvod x = _ctx.Proizvod.Find(id);
			_ctx.Proizvod.Remove(x);
			_ctx.SaveChanges();
			return Json(true);
		}
		[Route("{id}")]
		public IActionResult Uredi(int id)
		{

			var Proizvod = _ctx.Proizvod.Find(id);
			var model = new ProizvodDetaljiVM
			{
				Kolicina = Proizvod.Kolicina,
				Cijena = Proizvod.Cijena,
				ProizvodId = id


			};

			return Json(model);


		}
		[Route("uredi")]
		[HttpPost]
		public IActionResult SnimiUredi([FromBody]ProizvodDetaljiVM model)
		{
			var Proizvod = _ctx.Proizvod.Find(model.ProizvodId);
			Proizvod.Cijena = model.Cijena;
			Proizvod.Kolicina = model.Kolicina;
			_ctx.SaveChanges();
			return Json(true);
		}
		[Route("dodajj")]
		public IActionResult Dodaj()
		{

			var model = new ProizvodDodajVM();
			model.Kategorija = _ctx.Kategorija.Select(x => new SelectListItem
			{
				Value = x.KategorijaID.ToString(),
				Text = x.NazivKategorije
			}


			).ToList();

			model.Proizvodjac = _ctx.Proizvodjac.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.NazivProizvodjaca
			}


			).ToList();
			model.Uvoznik = _ctx.Uvoznik.Select(x => new SelectListItem
			{
				Value = x.UvoznikID.ToString(),
				Text = x.NazivUvoznika
			}


			).ToList();


			return Json(model);

		}
		[Route("dodaj")]
		[HttpPost]
		public IActionResult Snimi([FromBody]ProizvodDodajVM model)
		{

			Proizvod proizvod = new Proizvod
			{
				Cijena = model.Cijena,
				kategorijaId = model.KategorijaId,
				Kolicina = model.Kolicina,
				uvoznikId = model.UvoznikId,
				ProizvodjacId = model.ProizvodjacId,
				NazivProizvoda = model.Naziv,
				Kamera = model.Kamera,
				VelicinaEkrana = model.VelicinaEkrana,
				RamMemorija = model.RAMMemorija,
				Memorija = model.Memorija,
			};

			_ctx.Add(proizvod);
			_ctx.SaveChanges();
			return Json(true);



		}

	}
}
