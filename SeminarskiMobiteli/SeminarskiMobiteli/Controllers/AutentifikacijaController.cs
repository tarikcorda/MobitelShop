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
using SeminarskiMobiteli.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Vonage;
using Vonage.Request;
namespace SeminarskiMobiteli.Controllers
{
    public class AutentifikacijaController : Controller
    {
        private readonly Context _db;

        public AutentifikacijaController(Context db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(new LoginVM()
            {
                ZapamtiPassword = true,
            });
        }

        public async Task<IActionResult> Login(LoginVM input)
        {
            KorisnickiNalog korisnik = await _db.KorisnickiNalog
                .Where(x => x.KorisnickoIme == input.username)
                .Include(i => i.Korisnik)
                .ThenInclude(i => i.TipKorisnika)
                .FirstOrDefaultAsync();

            TabelaGreska g = new TabelaGreska();

            if (korisnik == null)
            {
                g.greska = "pogrešan username ";
                g.VrijemeGreske = DateTime.Now;

                _db.TabelaGreska.Add(g);
                _db.SaveChanges();

                TempData["error_poruka"] = "pogrešan username ";
                return RedirectToAction("Index", input);
            }

            //HttpContext.SetLogiraniKorisnik(korisnik, input.ZapamtiPassword);

            var LozinkaHash = SecurityHelper.ComputeSha256Hash(input.password + korisnik.Salt);

            if (korisnik.Hash == LozinkaHash)
            {


                var claimsIdentity = new ClaimsIdentity(
                    new List<Claim>() {
                    new Claim(ClaimTypes.Name,korisnik.KorisnickoIme),
                    new Claim(ClaimTypes.Role,korisnik.Korisnik.TipKorisnika.Naziv),
                    new Claim(ClaimTypes.NameIdentifier,korisnik.Korisnik.KorisnikId.ToString())
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                if (korisnik.Korisnik.TipKorisnika.Naziv == Uloga.User.ToString())
                {

                    //Random generator = new Random();
                    //int r = generator.Next(100000, 1000000);
                    //Code kod = new Code
                    //{
                    //    kod = r,
                    //    IsValid = true,
                    //    VrijemeSlanja = DateTime.Now
                    //};

                    //var credentials = Credentials.FromApiKeyAndSecret(
                    //        "2b172781",
                    //        "JhF4r66P63STQMMU"
                    //     );

                    //var VonageClient = new VonageClient(credentials);
                    //var response = VonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
                    //{
                    //    To = "387603021718",
                    //    From = "Vonage APIs",
                    //    Text = "Vas aktivacijski kod: " + r.ToString()
                    //});



                    //return RedirectToAction("PotvrdiIndex");
                    return RedirectToAction("Index", "Proizvod", new { area = "Korisnik" });



                }
                //return RedirectToAction("Index", "Proizvod", new { area = "Korisnik" });
                else
                {
                    return RedirectToAction("Index", "Proizvod", new { area = "Admin" });
                }


            }


            else
            {
                g.greska = "pogrešan password";
                g.VrijemeGreske = DateTime.Now;

                _db.TabelaGreska.Add(g);
                _db.SaveChanges();

                TempData["error_poruka"] = "pogrešan password";
                return RedirectToAction("Index", input);
            }




        }



        public IActionResult PotvrdiIndex(int kod)
        {
            return View();
        }

        public IActionResult Potvrdi(int kod)
        {
            List<int> kodovi = _db.Code.Where(t => t.kod == kod).Select(n => n.ID).ToList();

            foreach (var item in kodovi)
            {
                Code k = _db.Code.Find(item);
                if (k.IsValid != true && k.VrijemeSlanja.Hour - DateTime.Now.Hour > 5 && k.kod != kod)
                {
                    TempData["act_poruka"] = ("Kod nije aktiviran. Pokušajte ponovo.");
                    return RedirectToAction("Login");
                }

                k.IsValid = false;
            }
            return RedirectToAction("Index", "Proizvod", new { area = "Korisnik" });

        }
        public async Task<IActionResult> LogoutAsync()
        {

            await HttpContext.SignOutAsync(
    CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}

