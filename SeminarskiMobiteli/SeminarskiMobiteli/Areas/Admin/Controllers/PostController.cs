using System.Linq;
using System.Threading.Tasks;
using SeminarskiMobiteli.Controllers;
using SeminarskiMobiteli.ViewModel;
using EntityModels.Models;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SeminarskiMobiteli.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class PostController : Controller
    {
        

        private readonly Context MojContext;
        public PostController(Context ctx)
        {
            MojContext = ctx;
        }
        public IActionResult Index()
        {
            var model = new AdminPostIndexVM {
            Rows=MojContext.Post.Select(
                
                x=>new AdminPostIndexVM.Row {
                
                PostId=x.Id,
                Autor=x.Autor.Ime+" "+x.Autor.Prezime,
                AutorId=x.AutorId,
                Naslov=x.Naslov,
                Sadrzaj=x.Sadrzaj,
                Datum=x.DatumObjave
                }
                ).ToList()
            
            
            };

            return View(model);
        }
        public IActionResult Izbrisi(int id)
        {
            Post x = MojContext.Post.Find(id);
            MojContext.Post.Remove(x);
            MojContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Dodaj(Post post) {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var model = new AdminPostDodajVM {
             PostId = post.Id,
            AutorId=userId,
            Autor=MojContext.Korisnik.Where(i=> i.KorisnikId == userId).Select(i=> i.Ime ).FirstOrDefault() + " " + MojContext.Korisnik.Where(i => i.KorisnikId == userId).Select(i => i.Prezime).FirstOrDefault(),
            DatumObjave=post.DatumObjave,
            Naslov=post.Naslov,
            Sadrzaj=post.Sadrzaj,
            ImageLocation=post.ImageLocation
            };
            return View(model);
        
        }
        public IActionResult Snimi(AdminPostDodajVM vm) {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            Post post = new Post {
            Id=vm.PostId,
            AutorId=userId,
            DatumObjave=vm.DatumObjave,
            Naslov=vm.Naslov,
            Sadrzaj=vm.Sadrzaj,
            
            };
            MojContext.Add(post);
            MojContext.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var Post = MojContext.Post.Find(id);
            var model = new AdminPostDodajVM
            {
                PostId=id,
                AutorId=userId,
                DatumObjave=Post.DatumObjave,
                Naslov=Post.Naslov,
                Sadrzaj=Post.Sadrzaj,
                Autor = MojContext.Korisnik.Where(i => i.KorisnikId == userId).Select(i => i.Ime).FirstOrDefault() + " " + MojContext.Korisnik.Where(i => i.KorisnikId == userId).Select(i => i.Prezime).FirstOrDefault(),

            };

            return View(model);
        }
        public IActionResult SnimiUredi(AdminPostDodajVM model) {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var post = MojContext.Post.Find(model.PostId);
            post.Id = model.PostId;
            post.Naslov = model.Naslov;
            post.Sadrzaj = model.Sadrzaj;
            post.AutorId =userId;
            post.DatumObjave = model.DatumObjave;
            MojContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    
}
