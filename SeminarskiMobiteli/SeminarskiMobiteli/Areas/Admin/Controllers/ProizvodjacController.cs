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

namespace SeminarskiMobiteli.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class ProizvodjacController : Controller
	{
		private readonly Context MojContext;
		public ProizvodjacController(Context ctx)
		{
			MojContext = ctx;
		}
		public IActionResult Index()
		{
			var model = new AdminProizvodjac
			{
				Rows = MojContext.Proizvodjac
		.Select(
			x => new AdminProizvodjac.Row
			{
			ProizvodjacId=x.Id,
			Naziv=x.NazivProizvodjaca,
			Drzava=MojContext.Drzava.Where(z=>z.Id==x.SjedisteId).Select(g=>g.Naziv).FirstOrDefault()


			}
			).ToList()
			};
			return View(model);
			 
		}
		public IActionResult Dodaj()
		{

			var model = new AdminProizvodjacDodajVM();
			model.Drzava = MojContext.Drzava.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Naziv
			}


			).ToList();

		 


			return View(model);

		}
		public IActionResult Snimi(AdminProizvodjacDodajVM model)
		{

			Proizvodjac proizvodjac = new Proizvodjac
			{
				Id=model.ProizvodjacId,
				NazivProizvodjaca=model.Naziv,
				SjedisteId=model.DrzavaId


			};

			MojContext.Add(proizvodjac);
			MojContext.SaveChanges();
			return RedirectToAction("Index");



		}
	}
}
