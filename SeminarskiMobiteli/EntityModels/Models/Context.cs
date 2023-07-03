using System.Linq;
using EntityModels.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        { }
        public Context() { }

        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<TipKorisnika> TipKorisnika { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Uvoznik> Uvoznik { get; set; }
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<Recenzija> Recenzija { get; set; }
        public DbSet<Oglas> Oglas { get; set; }
        public DbSet<KorisnikOglas> KorisnikOglas { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Banka> Banka { get; set; }
        public DbSet<BankovniRacun> Racun { get; set; }
        public DbSet<Proizvodjac> Proizvodjac { get; set; }
        public DbSet<Dostavljac> Dostavljac { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<NarudzbaStavka> NarudzbaStavka { get; set; }
		public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<AutorizacijskiToken> AutorizacijskiToken { get; set; }
        public DbSet<TabelaGreska> TabelaGreska { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Code> Code { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Oglas>().Property(oglas => oglas.Trajanje)
                .HasDefaultValue(0);

            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}