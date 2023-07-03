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
using System.Security.Claims;
namespace SeminarskiMobiteli.Controllers
{
    public class ProizvodiGuestController : Controller
    {
		private readonly Context MojContext;
		public ProizvodiGuestController(Context ctx)
		{
			MojContext = ctx;
		}

		public IActionResult Index(AdministratorProizvodVM obj)
		{
			obj ??= new AdministratorProizvodVM();
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
	}
}
