using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityModels.Migrations
{
    public partial class AddKolone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kamera",
                table: "Proizvod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Memorija",
                table: "Proizvod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RamMemorija",
                table: "Proizvod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VelicinaEkrana",
                table: "Proizvod",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Korisnik",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "Korisnik",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Korisnik",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kamera",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Memorija",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "RamMemorija",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "VelicinaEkrana",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Korisnik");
        }
    }
}
