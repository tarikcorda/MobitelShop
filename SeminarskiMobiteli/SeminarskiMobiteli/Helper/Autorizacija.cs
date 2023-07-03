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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
namespace SeminarskiMobiteli.Helper
{
	public class AutorizacijaAttribute : TypeFilterAttribute
	{
		public AutorizacijaAttribute(bool admin, bool klijent)
			: base(typeof(MyAuthorizeImpl))
		{
			Arguments = new object[] { admin, klijent };

		}
	}



	public class MyAuthorizeImpl : IAsyncActionFilter
	{
		public MyAuthorizeImpl(bool admin, bool klijent)
		{
			_admin = admin;
			_klijent = klijent;
		}
		private readonly bool _admin;
		private readonly bool _klijent;

		public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
		{

			KorisnickiNalog k = filterContext.HttpContext.GetLogiraniKorisnik();
			if (k == null)
			{
				if (filterContext.Controller is Controller controller)
				{
					controller.TempData["error_poruka"] = "Niste logirani!";
				}

				filterContext.Result = new RedirectToActionResult("Index", "Autentifikacija", new { @area = "" });
				return;
			}

			//Preuzimamo DbContext preko app servisa
			Context _db = filterContext.HttpContext.RequestServices.GetService<Context>();

			//klijenti mogu pristupiti
			if (_klijent && _db.Korisnik.Any(s => s.KorisnickiNalogID == k.ID))
			{
				await next(); // ok - ima pravo pristupa
				return;
			}



			//admin mogu pristupiti
			if (_admin && _db.Korisnik.Any(s => s.KorisnickiNalogID == k.ID))
			{
				await next(); // ok - ima pravo pristupa
				return;

			}

			if (filterContext.Controller is Controller c1)
			{
				c1.TempData["error-poruka"] = "Nemate pravo pristupa!";
			}
			filterContext.Result = new RedirectToActionResult("Index", "Home", new { @area = "" });
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			 throw new NotImplementedException();
		}
	}
}

