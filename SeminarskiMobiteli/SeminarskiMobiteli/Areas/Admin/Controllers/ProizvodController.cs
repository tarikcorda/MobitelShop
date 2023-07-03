using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeminarskiMobiteli.Controllers;
using SeminarskiMobiteli.ViewModel;
using EntityModels.Models;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeminarskiMobiteli.Helper;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace SeminarskiMobiteli.Areas.Administrator.Controllers
{
	
	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class ProizvodController : Controller
	{

		private readonly Context MojContext;
		public ProizvodController(Context ctx)
		{
			MojContext = ctx;
		}
	 
		public IActionResult Index(AdministratorProizvodVM obj)
		{
            obj ??= new AdministratorProizvodVM();
			var query = MojContext.Proizvod.AsQueryable();
			if (!string.IsNullOrEmpty(obj.Naziv)) {

			query=query.Where(x => x.NazivProizvoda.ToLower().Contains(obj.Naziv.ToLower()));
			
			}
			obj.Rows = query
			.Select(
				x => new AdministratorProizvodVM.Row
				{
					MobitelId = x.ProizvodID,
					Naziv = x.NazivProizvoda,
					Cijena = x.Cijena,
					Kolicina = x.Kolicina,
					Kategorija =x.kategorija.NazivKategorije,
					imageLocation = x.imageLocation
				 
				}
				).ToList();
			
			return View(obj);
		}
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
					imageLocation = x.imageLocation

				}
				).ToList();
            return PartialView("Index", proizvodi);

        }

		public IActionResult Izbrisi(int id)
		{

			List<Recenzija> rec = MojContext.Recenzija
				.Where(x => x.ProizvodId == id).ToList();
			List<NarudzbaStavka> nas = MojContext.NarudzbaStavka
				.Where(x => x.ProizvodId == id).ToList();

			MojContext.Recenzija.RemoveRange(rec);
			MojContext.NarudzbaStavka.RemoveRange(nas);

			Proizvod x = MojContext.Proizvod.Find(id);
			
				MojContext.Proizvod.Remove(x);
				MojContext.SaveChanges();
				return RedirectToAction("Index");
		}

		public IActionResult Dodaj() {

			var model = new ProizvodDodajVM();
			model.Kategorija = MojContext.Kategorija.Select(x => new SelectListItem {
				Value = x.KategorijaID.ToString(),
				Text = x.NazivKategorije
			}


			).ToList();

			model.Proizvodjac = MojContext.Proizvodjac.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.NazivProizvodjaca
			}


			).ToList();
			model.Uvoznik = MojContext.Uvoznik.Select(x => new SelectListItem
			{
				Value = x.UvoznikID.ToString(),
				Text = x.NazivUvoznika
			}


			).ToList();


			return View(model);
		
		}
		public IActionResult Snimi(ProizvodDodajVM model) {

			Proizvod proizvod = new Proizvod
			{
				Cijena = model.Cijena,
				kategorijaId = model.KategorijaId,
				Kolicina = model.Kolicina,
				uvoznikId = model.UvoznikId,
				ProizvodjacId = model.ProizvodjacId,
				NazivProizvoda = model.Naziv,
				Kamera=model.Kamera,
				VelicinaEkrana = model.VelicinaEkrana,
				RamMemorija=model.RAMMemorija,
				Memorija=model.Memorija,
			};

			MojContext.Add(proizvod);
			MojContext.SaveChanges();
			return RedirectToAction("Index");
		
		
		
		}
		public IActionResult Uredi(int id) {

			var Proizvod = MojContext.Proizvod.Find(id);
			var model = new ProizvodDetaljiVM
			{
				Kolicina = Proizvod.Kolicina,
				Cijena=Proizvod.Cijena,
				ProizvodId=id
			 

			};

			return View(model);
		
		
		}
		public IActionResult SnimiUredi(ProizvodDetaljiVM model)
		{
			var Proizvod = MojContext.Proizvod.Find(model.ProizvodId);
			Proizvod.Cijena = model.Cijena;
			Proizvod.Kolicina = model.Kolicina;
			MojContext.SaveChanges();
			return RedirectToAction("Index");
		}
		


	}
}
