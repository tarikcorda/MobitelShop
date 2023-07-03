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
namespace SeminarskiMobiteli.Helper
{
	public static class Autentifikacija
	{
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, KorisnickiNalog korisnik, bool snimiUCookie = false)
        {
            context.Session.Set(LogiraniKorisnik, korisnik);

            if (snimiUCookie)
            {
                //Preuzimamo DbContext preko app services
                Context _db = context.RequestServices.GetService<Context>();

                string token = Guid.NewGuid().ToString();
                _db.AutorizacijskiToken.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnickiNalogId = korisnik.ID,
                    VrijemeEvidentiranja = DateTime.Now
                });
                _db.SaveChanges();
                context.Response.SetCookieJson(LogiraniKorisnik, token);
            }
            else
            {
                context.Response.RemoveCookie(LogiraniKorisnik);
            }
        }

        public static KorisnickiNalog GetLogiraniKorisnik(this HttpContext context)
        {
            KorisnickiNalog korisnik = context.Session.Get<KorisnickiNalog>(LogiraniKorisnik);

            if (korisnik == null)
            {
                Context _db = context.RequestServices.GetService<Context>();

                string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);
                if (token == null)
                    return null;

                korisnik = _db.AutorizacijskiToken
                    .Where(x => x.Vrijednost == token)
                    .Select(s => s.KorisnickiNalog)
                    .SingleOrDefault();

                if (korisnik != null)
                {
                    context.Session.Set(LogiraniKorisnik, korisnik);
                }

            }
            return korisnik;
        }

        public static void DeleteLogiraniKorisnik(this HttpContext context)
        {
            KorisnickiNalog korisnik = context.Session.Get<KorisnickiNalog>(LogiraniKorisnik);
            if (korisnik != null)
            {
                context.Session.Remove(LogiraniKorisnik);

                context.Response.RemoveCookie(LogiraniKorisnik);
            }
        }
    }
}

