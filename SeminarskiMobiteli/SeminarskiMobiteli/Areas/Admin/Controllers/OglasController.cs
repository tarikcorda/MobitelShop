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
using Microsoft.AspNetCore.Authorization;

namespace SeminarskiMobiteli.Areas.Admin.Controllers
{
	 
	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class OglasController : Controller
	{
		private readonly Context MojContext;
		public OglasController(Context ctx)
		{
			MojContext = ctx;
		}

		public IActionResult Index()
		{
			var model = new AdminOglasIndexVM
			{
				Rows=MojContext.Oglas
				.Select
				(
				x=>new AdminOglasIndexVM.Row
				{ 
				OglasId=x.Id,
				DatumIsteka=x.DatumIsteka,
				DatumObjave=x.DatumObjave,
				AKtivan=x.Aktivan,
				Lokacija=x.Lokacija,
				Naslov=x.Naslov,
				Pozicija=x.BrojPozicija
				}
				).ToList()
			};
			return View(model);
		}
		public IActionResult Dodaj(Oglas oglas) {
			var model = new AdminOglasDodajVM
			{
				ProizvodjacId=oglas.Id,
				Naslov=oglas.Naslov,
				DatumIsteka=oglas.DatumIsteka,
				DatumObjave=DateTime.Now,
				AKtivan=oglas.Aktivan,
				Lokacija=oglas.Lokacija,
				Pozicija=oglas.BrojPozicija,
				Trajanje=oglas.Trajanje
			};
			return View(model);
		}
		public IActionResult Snimi(AdminOglasDodajVM model)
		{
			Oglas oglas = new Oglas
			{
				Id = model.ProizvodjacId,
				Aktivan=model.AKtivan,
				BrojPozicija=model.Pozicija,
				Naslov=model.Naslov,
				DatumIsteka=model.DatumIsteka,
				DatumObjave=model.DatumObjave,
				Lokacija=model.Lokacija,
				Sadrzaj=model.Sadrzaj,
				Trajanje=model.Trajanje
				 
			};
			MojContext.Add(oglas);
			MojContext.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Izbrisi(int id)
		{
			Oglas  x = MojContext.Oglas.Find(id);
			MojContext.Oglas.Remove(x);
			MojContext.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Uredi(int id)
		{

			var Oglas = MojContext.Oglas.Find(id);
			var model = new AdminOglasDodajVM
			{
				ProizvodjacId=id,
				AKtivan=Oglas.Aktivan,
				DatumIsteka=Oglas.DatumIsteka,
				DatumObjave=Oglas.DatumObjave,
				Lokacija=Oglas.Lokacija,
				Naslov=Oglas.Naslov,
				Pozicija=Oglas.BrojPozicija,
				Sadrzaj=Oglas.Sadrzaj,
				Trajanje=Oglas.Trajanje


			};

			return View(model);
		}
		public IActionResult SnimiUredi(AdminOglasDodajVM model)
		{
			var Oglas = MojContext.Oglas.Find(model.ProizvodjacId);
			Oglas.Id = model.ProizvodjacId;
			Oglas.DatumObjave = model.DatumObjave;
			Oglas.DatumIsteka = model.DatumIsteka;
			Oglas.Aktivan = model.AKtivan;
			Oglas.BrojPozicija = model.Pozicija;
			Oglas.Lokacija = model.Lokacija;
			Oglas.Naslov = model.Naslov;
			Oglas.Sadrzaj = model.Sadrzaj;
			Oglas.Trajanje = model.Trajanje;
			MojContext.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
