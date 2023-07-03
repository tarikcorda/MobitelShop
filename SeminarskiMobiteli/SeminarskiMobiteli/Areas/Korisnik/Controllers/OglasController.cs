using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeminarskiMobiteli.Controllers;
using SeminarskiMobiteli.ViewModel;
using EntityModels.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SeminarskiMobiteli.Helper;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using MimeKit;
using System.Net;

namespace SeminarskiMobiteli.Areas.Korisnik.Controllers
{
	[Area("Korisnik")]
	[Authorize(Roles = "User")]
	public class OglasController : Controller
    {
		private readonly Context MojContext;
		EmailConfiguration emailConfiguration;
		public OglasController(Context ctx, EmailConfiguration email)
		{
			MojContext = ctx;
			emailConfiguration = email;
		}

		//public IActionResult Index()
		//{
		//	var model = new AdminOglasIndexVM
		//	{
		//		Rows = MojContext.Oglas
		//		.Select
		//		(
		//		x => new AdminOglasIndexVM.Row
		//		{
		//			OglasId = x.Id,
		//			DatumIsteka = x.DatumIsteka,
		//			DatumObjave = x.DatumObjave,
		//			AKtivan = x.Aktivan,
		//			Lokacija = x.Lokacija,
		//			Naslov = x.Naslov,
		//			Pozicija = x.BrojPozicija,


		//		}
		//		).ToList()
		//	};
		//	return View(model);
		//}
		//public IActionResult Detalji(int id)
		//{
		//	Oglas x = MojContext.Oglas.Find(id);
		//	KorisnikOglasDetalji model = new KorisnikOglasDetalji
		//	{
		//		AKtivan=x.Aktivan,
		//		DatumIsteka=x.DatumIsteka,
		//		DatumObjave=x.DatumObjave,
		//		Lokacija=x.Lokacija,
		//		Naslov=x.Naslov,
		//		Pozicija=x.BrojPozicija,
		//		Sadrzaj=x.Sadrzaj
		//	};
		//	return View("Detalji", model);
		//}
		public IActionResult Prikazi()
		{
			var oglasi = MojContext.Oglas.Where(x => x.Aktivan == true).ToList();
			return View(oglasi);
		}

		public IActionResult DetaljiOglasa(int id)
		{
			var detaljiO = MojContext.Oglas.Where(x => x.Id == id).SingleOrDefault();
			return View(detaljiO);
		}

		public IActionResult Prijava(int id)
		{
			var model = new KorisnikOglasPrijavaVM {

				OglasId = id
			
			};
			return View("Prijava",model);
		}
		[HttpPost]
		public IActionResult Back(KorisnikOglasPrijavaVM model)
        {
			
			var fromAddress = new MailAddress(emailConfiguration.From, "From Name");
			var toAddress = new MailAddress(model.Email, "To Name");
			 string fromPassword = emailConfiguration.Password;
			const string subject = "Prijava na oglas";
			const string body = "Uspjesno ste se prijavili na oglas!";

			var smtp = new SmtpClient
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
			};
			using (var message = new MailMessage(fromAddress, toAddress)
			{
				Subject = subject,
				Body = body
			})
			{
				smtp.Send(message);
			}

			return Redirect("/Korisnik/Oglas/Prikazi");

		}
	}
}
