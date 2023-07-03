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

namespace SeminarskiMobiteli.Areas.Administrator.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Administrator")]
	public class KategorijaController : Controller
	{
		private readonly Context MojContext;
		public KategorijaController(Context ctx)
		{
			MojContext = ctx;
			
		}
		public IActionResult Index()
		{
			var model = new AdminKategorijaVM
			{
				Rows = MojContext.Kategorija
			.Select(
				x => new AdminKategorijaVM.Row
				{
					KategorijaId = x.KategorijaID,
					Naziv = x.NazivKategorije

				}
				).ToList()
			};
			return View(model);
		}
		public IActionResult Dodaj(string kateg)
		{
			var model = new AdminKategorijaDodajVM

			{
				Kategorija = kateg
			};
			return View(model);

		 
		 
		}
		public IActionResult Snimi(AdminKategorijaDodajVM model)
		{

			Kategorija kategorija = new Kategorija
			{
                KategorijaID = model.KategorijaId,
                NazivKategorije =model.Kategorija
				
				

			};

			MojContext.Add(kategorija);
			MojContext.SaveChanges();
			return RedirectToAction("Index");



		}
	}
}
