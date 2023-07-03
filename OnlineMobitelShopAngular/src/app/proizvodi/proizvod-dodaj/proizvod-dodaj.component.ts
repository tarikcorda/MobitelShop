import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProizvodiDodajModel } from 'src/app/models/proizvodi-dodaj.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ActivatedRoute, Route } from '@angular/router';

@Component({
  selector: 'app-proizvod-dodaj',
  templateUrl: './proizvod-dodaj.component.html',
  styleUrls: ['./proizvod-dodaj.component.css']
})
export class ProizvodDodajComponent implements OnInit {
obj:ProizvodiDodajModel;
mobitelId:number ;
naziv:string;
kategorijaId:string;
proizvodjacId:string;
uvoznikId:string;
cijena:number;
kolicina:number;
ramMemorija:string;
velicinaEkrana:string;
kamera:string;
memorija:string;

  constructor(private activatedRoute:ActivatedRoute,private httpClient:HttpClient, private Router:Router) { }

  ngOnInit(): void {
    this.httpClient.get<ProizvodiDodajModel>(environment.apiUrl+"api/proizvodi/dodajj").subscribe(data=>{
      this.obj=data;
      this.obj.kategorija=data.kategorija;
      this.obj.uvoznik=data.uvoznik;
      this.obj.proizvodjac=data.proizvodjac;
    });


  //   const body = { title: 'Angular PUT Request Example' };
  //   this.httpClient.put<any>(environment.apiUrl+"api/proizvodi", this.obj)
  //       .subscribe(data=>{
  //         this.obj.proizvodjacId=data.proizvodjacId;
  //         this.obj.kategorijaId=data.kategorijaId;
  //         this.obj.mobitelId=data.mobitelId;
  //         this.obj.cijena=data.cijena;
  //         this.obj.kamera=data.kamera;
  //         this.obj.kolicina=data.kolicina;
  //         this.obj.kategorija=data.kategorija;
  //         this.obj.naziv=data.naziv;
  //         this.obj.ramMemorija=data.ramMemorija;
  //       });
  }
  spremi(){

    this.httpClient.post<boolean>(environment.apiUrl+"api/proizvodi/dodaj",
    {naziv:this.naziv,kategorijaId:parseInt(this.kategorijaId),proizvodjacId:parseInt(this.proizvodjacId),uvoznikId:parseInt(this.uvoznikId),cijena:this.cijena,kolicina:this.kolicina,
      ramMemorija:this.ramMemorija,velicinaEkrana:this.velicinaEkrana,kamera:this.kamera,memorija:this.memorija}).subscribe(data=>{
    });
    this.Router.navigateByUrl('proizvodi/index');
  



  }

}
