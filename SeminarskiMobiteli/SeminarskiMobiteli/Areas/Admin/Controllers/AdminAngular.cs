using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using SeminarskiMobiteli.ViewModel;
using System.Linq;

namespace SeminarskiMobiteli.Areas.Admin.Controllers
{
	[Route("api/kategorija")]
	public class AdminAngular : Controller
    {

        private readonly Context _ctx;

        public AdminAngular(Context ctx) { _ctx = ctx; }
		[Route("")]
		public IActionResult Index(string naziv)
        {
			var obj = new AdminKategorijaVM();
			var query = _ctx.Kategorija.AsQueryable();
			if (!string.IsNullOrEmpty(naziv))
			{

				query = query.Where(x => x.NazivKategorije.ToLower().Contains(naziv.ToLower()));

			}

			obj.Rows = query
			  .Select(
				  x => new AdminKategorijaVM.Row
				  {
					  KategorijaId = x.KategorijaID,
					  Naziv = x.NazivKategorije

				  }
				  ).ToList();
			
			return Json(obj);
		}
		[Route("izbrisi/{id}")]
		[HttpPost]
		public IActionResult Izbrisi(int id)
		{
			Kategorija x = _ctx.Kategorija.Find(id);
			_ctx.Kategorija.Remove(x);
			_ctx.SaveChanges();
			return Json(true);
		}
		[Route("{id}")]
		public IActionResult Uredi(int id)
		{

			var Kategorija = _ctx.Kategorija.Find(id);
			var model = new AdminAngularVM
			{
				KategorijaId=id,
				Naziv=Kategorija.NazivKategorije


			};

			return Json(model);


		}
		[Route("uredi")]
		[HttpPost]
		public IActionResult SnimiUredi([FromBody] AdminAngularVM model)
		{
			var Kategorija = _ctx.Kategorija.Find(model.KategorijaId);
			Kategorija.NazivKategorije = model.Naziv;
			_ctx.SaveChanges();
			return Json(true);
		}
		
		 
		[Route("dodajj")]
        public IActionResult Dodaj(string kateg)
        {
            var model = new AdminDodajAngularVM

            {
                Naziv = kateg
            };
            return Json(model);


        }

        [Route("dodaj")]
		[HttpPost]
		public IActionResult Snimi([FromBody] AdminDodajAngularVM model)
		{

			Kategorija kategorija = new Kategorija
			{
				KategorijaID=model.KategorijaId,
				NazivKategorije=model.Naziv
			};

			_ctx.Add(kategorija);
			_ctx.SaveChanges();
			return Json(true);



		}
	}

}
