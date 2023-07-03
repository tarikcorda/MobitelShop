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
using Microsoft.AspNetCore.Authorization;

namespace SeminarskiMobiteli.Areas.Korisnik.Controllers
{
	[Area("Korisnik")]
	[Authorize(Roles = "User")]

	public class RecenzijaController : Controller
	{
		private readonly Context MojContext;
		public RecenzijaController(Context ctx)
		{
			MojContext = ctx;
		}
		public IActionResult Index(int id)
		{
			var recenzija = MojContext.Recenzija.Where(i => i.ProizvodId == id).FirstOrDefault();
			if (recenzija == null)
			{
				return RedirectToAction("Dodaj", "Recenzija", new { area = "Korisnik" });
			}
			else
			{
				var model = new RecenzijaKorisnikVM
				{
					id = id,
					procjecnaOcjena = (float)MojContext.Recenzija.Where(x => x.ProizvodId == id).Select(x => x.Ocjena).Average(),
					rows = MojContext.Recenzija.Where(x => x.ProizvodId == id).Select(x => new RecenzijaKorisnikVM.Row
					{
						ProizvodID = x.ProizvodId,
						NazivProizvoda = x.Proizvod.NazivProizvoda,
						Komentar = x.Komentar,
						Ocjena = x.Ocjena


					}).ToList(),
				};

				return View(model);
			}
		}
		public IActionResult Dodaj(int id)
		{
			
			var model = new RecenzijaDodajKorisnikVM
			{

				ProizvodID = id,
				Ocjena = MojContext.Recenzija.Where(i => i.ProizvodId == id).Select(x => x.Ocjena).FirstOrDefault(),
				Komentar = MojContext.Recenzija.Where(i => i.ProizvodId == id).Select(x => x.Komentar).FirstOrDefault()
			};
			return View(model);
		}
		public IActionResult Snimi(RecenzijaDodajKorisnikVM rec)
		{
			var m = new Recenzija
			{
				ProizvodId = rec.ProizvodID,
				Komentar = rec.Komentar,
				Ocjena = rec.Ocjena
			};
			MojContext.Recenzija.Add(m);
			MojContext.SaveChanges();


			return RedirectToAction("Index", new { id = rec.ProizvodID });
		}
	}
}
