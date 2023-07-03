import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { KategorijaDodajModel } from 'src/app/models/kategorija-dodaj.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ActivatedRoute, Route } from '@angular/router';

@Component({
  selector: 'app-kategorija-dodaj',
  templateUrl: './kategorija-dodaj.component.html',
  styleUrls: ['./kategorija-dodaj.component.css']
})
export class KategorijaDodajComponent implements OnInit {
  obj:KategorijaDodajModel;
  kategorijaId:number;
  naziv:string;
  constructor(private activatedRoute:ActivatedRoute,private httpClient:HttpClient, private Router:Router) {}

  ngOnInit(): void {
    this.httpClient.get<KategorijaDodajModel>(environment.apiUrl+"api/kategorija/dodajj").subscribe(data=>{
      this.obj=data;
      this.obj.naziv=data.naziv;
     
     
  });

  }
  spremi(){

    this.httpClient.post<boolean>(environment.apiUrl+"api/kategorija/dodaj",
    {naziv:this.naziv,kategorijaId:this.kategorijaId}).subscribe(data=>{
    });
    this.Router.navigateByUrl('kategorija/index');

  



  }


}
