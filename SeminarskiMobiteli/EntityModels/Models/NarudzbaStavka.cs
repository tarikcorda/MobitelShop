namespace ClassLibrary.Models
{
    public class NarudzbaStavka
    {

        public int Id { get; set; }
        public Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }
        public Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set; }
        public int Kolicina { get; set; }
        public double Cijena { get; set; }



    }
}
