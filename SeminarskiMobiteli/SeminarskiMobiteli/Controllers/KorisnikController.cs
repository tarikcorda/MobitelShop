using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClassLibrary.Models;
using SeminarskiMobiteli.ViewModel;
using EntityModels.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeminarskiMobiteli.Helper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace SeminarskiMobiteli.Controllers
{
    public class KorisnikController : Controller
    {
        private Context _context;

        public KorisnikController(Context context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            ViewData["ulazniModel"] = new KorisnikIndexVM
            {

                Rows = _context.Korisnik
                .Where(y => y.KorisnikId > 6)
            .Select(x => new KorisnikIndexVM.Row
            {
                KorisnikID = x.KorisnikId,
                TipKorisnika = x.TipKorisnika.Naziv,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Spol = x.Spol,
                DatumRegistracije = x.DatumRegistracije,
                Pretplacen = x.Pretplacen

            })
            .ToList()






            };


            return View();
        }
        public IActionResult Obrisi(int id)
        {
            Korisnik x = _context.Korisnik.Find(id);
            _context.Korisnik.Remove(x);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Dodaj()
        {
            var ulazniModel = new KorisnikDodajVM();
           

            ulazniModel.Sjediste = _context.Drzava
              .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
              {
                  Value = x.Id.ToString(),
                  Text = x.Naziv
              })
              .ToList();

            ulazniModel.DatumRegistracije = DateTime.Now;


            return View(ulazniModel);
        }
        public IActionResult Detalji(int id) {
            
            Korisnik x = _context.Korisnik.Find(id);
            KorisnikDetaljiVM model = new KorisnikDetaljiVM
            {
                ime = x.Ime,
                prezime = x.Prezime,
                datum = x.DatumRegistracije,
                pretplacen = x.Pretplacen,
                spol = x.Spol,
                KorisnickoIme=x.KorisnickiNalog.KorisnickoIme,
                Lozinka=x.KorisnickiNalog.Lozinka
            };
            return View("Detalji", model);
        }
        
        
        public IActionResult Snimi(KorisnikDodajVM zapis)
        {
            if (!ModelState.IsValid) {
            
                zapis.Sjediste= _context.Drzava
              .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
              {
                  Value = x.Id.ToString(),
                  Text = x.Naziv
              })
              .ToList();
                return View("Dodaj",zapis);
            }
            var salt = StringGenerator.RandomString(8);
            var hash = SecurityHelper.ComputeSha256Hash(zapis.Lozinka + salt);
            Korisnik korisnik = new Korisnik
            {

                TipKorisnika = _context.TipKorisnika.FirstOrDefault(i => i.Naziv == Uloga.Administrator.ToString()),
                SjedisteId = zapis.SjedisteID,
                Ime = zapis.Ime,
                Prezime = zapis.Prezime,
                Spol = zapis.Spol,
                DatumRegistracije = zapis.DatumRegistracije,
                Email=zapis.Email,
                BrojTelefona=zapis.BrojTelefona,
                KorisnickiNalog = new KorisnickiNalog {
                    Hash = hash,
                    Salt = salt,
                    KorisnickoIme = zapis.KorisnickoIme,
                    Lozinka=zapis.Lozinka
                }
            };
            _context.Korisnik.Add(korisnik);
            _context.SaveChanges();
            return RedirectToAction("Index", "Autentifikacija", new { area = "" });
        }
        public IActionResult Back() {
            return RedirectToAction("Index");
        }
        public IActionResult DetaljiIzmijeni() {



           
            var id =int.Parse( User.FindFirst(ClaimTypes.NameIdentifier).Value);
             Korisnik pronadji = _context.Korisnik.Where(i=> i.KorisnikId==id).Include(i=> i.KorisnickiNalog).FirstOrDefault();
            


            var model = new KorisnikIzmijeniVM
            {
                KorisnikId = id,
                Ime = pronadji.Ime,
                Prezime = pronadji.Prezime,
                KorisnickoIme=pronadji.KorisnickiNalog.KorisnickoIme,
                Lozinka=pronadji.KorisnickiNalog.Lozinka


            };

            model.Sjediste = _context.Drzava.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Naziv
                
            }).ToList();

            return View(model);

        }

        public IActionResult SnimiUredi(KorisnikIzmijeniVM model) {
            var salt = StringGenerator.RandomString(8);
            var hash = SecurityHelper.ComputeSha256Hash(model.Lozinka + salt);
            var Stavka = _context.Korisnik.Where(i => i.KorisnikId == model.KorisnikId).Include(i => i.KorisnickiNalog).FirstOrDefault();
            Stavka.SjedisteId = model.SjedisteId;
            Stavka.Prezime=model.Prezime;
            Stavka.Ime = model.Ime;
            Stavka.KorisnickiNalog.KorisnickoIme = model.KorisnickoIme;
            Stavka.KorisnickiNalog.Hash = hash;
            Stavka.KorisnickiNalog.Salt = salt;
            _context.SaveChanges();


            return Redirect("/Admin/Kategorija");
        }

        public IActionResult PrikaziNarudzbe() {

            var id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var narudzba=_context.Narudzba.Where(i => i.NaruciocId == id).FirstOrDefault();

            var model = new NarudzbePrikaziVM { 
            
            Rows=_context.NarudzbaStavka.Where(i=>i.NarudzbaId==narudzba.Id)
            .Include(i=>i.Proizvod)
            .Include(i=>i.Narudzba)
            .Select(
                
                i=>new NarudzbePrikaziVM.Row {
                Naziv=i.Proizvod.NazivProizvoda,
                Kolicina=i.Kolicina,
                Cijena=i.Cijena,
                Ukupno=i.Kolicina*i.Cijena,
                Datum=i.Narudzba.DatumKreiranjaNarudzbe.ToString()
                }
                
                ).ToList()
            
            };

            return View(model);

        }
        
    }
}