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

namespace SeminarskiMobiteli.Areas.Korisnik.Controllers
{

	[Area("Korisnik")]

	[Authorize(Roles = "User")]
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
					imageLocation=x.imageLocation

				}
				).ToList();
			
			return View(obj);
		}
		public IActionResult Korpa(int id)
		{
			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			var korisnik = MojContext.Korisnik.Where(i => i.KorisnikId == userId).Include(i => i.Korpe).ThenInclude(i=>i.Proizvod).FirstOrDefault();

			//var proizvod = MojContext.Proizvod.Find(id);

			var model = new KorpaIndexVM
			{

				Rows = korisnik.Korpe.Select(
					x => new KorpaIndexVM.Row
					{
						ProizvodId = x.ProizvodId,
						Slika = x.Proizvod.imageLocation,
						Cijena = x.Proizvod.Cijena,
						Naziv = x.Proizvod.NazivProizvoda,
						Kolicina = x.Kolicina
					}

					).ToList()


			};


			return View(model);
		}
		public IActionResult Poredi([FromQuery] int proizvodId1, [FromQuery] int proizvodId2)
		{
			var p = MojContext.Proizvod.Where(x => x.ProizvodID == proizvodId1 || x.ProizvodID == proizvodId2).ToList();
			var sviProizvodi = MojContext.Proizvod.Where(x => x.ProizvodID != proizvodId1).ToList();
			return View(new ProizvodPorediVM()
			{
				Proizvod1 = p[0],
				Proizvod2 = p.Count > 1 ? p[1] : null,
				Proizvodi = sviProizvodi
			});
		}
		public IActionResult Narudzba (){


			var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			var korisnik = MojContext.Korisnik.Where(i => i.KorisnikId == userId).Include(i => i.Korpe).ThenInclude(i => i.Proizvod).FirstOrDefault();
		

           

			var model = new KorisnikNarudzbaVM {
			
			Dostavljac=MojContext.Dostavljac.Select(
				i=>new SelectListItem {
				Value = i.Id.ToString(),
				Text=i.NazivDostave
				
				}
				
				).ToList(),
			KorisnikId=userId,
			KorinsikImePrezime=korisnik.Ime+" "+korisnik.Prezime,
			Adresa=korisnik.Adresa,
			BrojTelefona=korisnik.BrojTelefona,
			Email=korisnik.Email
			
				
				


			};


			return View(model);
		}
		public IActionResult SnimiNarudzbu(KorisnikNarudzbaVM model) {

			var narudzba = new Narudzba
			{ 
			DostavljacId=model.DostavljacId,
			Aktivna=false,
			Potvrdjena=false,
			DatumKreiranjaNarudzbe=DateTime.UtcNow,
			NaruciocId=model.KorisnikId,
				
			};
			MojContext.Add(narudzba);
			MojContext.SaveChanges();

			var temp = MojContext.Korpa
				.Include(i=>i.Proizvod)
				.Where(i => i.KorisnikId == model.KorisnikId).ToList();

			var korpa2=MojContext.Korpa.Where(i => i.KorisnikId == model.KorisnikId).ToList();


			foreach (var item in temp)
            {
				var narudzbaStavka = new NarudzbaStavka {
				Cijena=item.Proizvod.Cijena*model.Kolicina,
				NarudzbaId=narudzba.Id,
				Kolicina=item.Kolicina,
				ProizvodId=item.ProizvodId
				
				};
				MojContext.Add(narudzbaStavka);
				var izmjeniKolicinu = MojContext.Proizvod.Where(i => i.ProizvodID == item.ProizvodId).FirstOrDefault();
				izmjeniKolicinu.Kolicina -= item.Kolicina;
				MojContext.Proizvod.Update(izmjeniKolicinu);
            }
            foreach (var item in korpa2)
            {
				MojContext.Korpa.Remove(item);
			}
			
			MojContext.SaveChanges();
		
			return Redirect("/Korisnik/Proizvod/Index");
		}
		public IActionResult Back()
        {
			return Redirect("/Korisnik/Proizvod/Index");

        }
	
	}
}
