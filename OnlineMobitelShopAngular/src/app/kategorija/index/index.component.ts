import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { KategorijaIndexModel } from 'src/app/models/kategorija-index.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'kategorija-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class KategorijaIndexComponent implements OnInit {
  kategorija:KategorijaIndexModel;
  pretraga:string;
  constructor(private httpClient:HttpClient, private Router: Router) { }

  ngOnInit(): void {
    this.httpClient.get<KategorijaIndexModel>(environment.apiUrl+"api/kategorija").subscribe(data=>{this.kategorija=data});
  }
  pretrazi(){
    this.httpClient.get<KategorijaIndexModel>(environment.apiUrl+"api/kategorija?naziv="+this.pretraga).subscribe(data=>{this.kategorija=data});
  
  
  }
  izbrisi(id:number){
    this.httpClient.post<boolean>(environment.apiUrl+"api/kategorija/izbrisi/"+id,null).subscribe(data=>{
      this.kategorija.rows=this.kategorija.rows.filter(i=>i.kategorijaId!==id);
    });

  }
  Uredi(id : number){
    this.Router.navigate(['/kategorija/uredi/', id]);
  }

  DodajIndex()
  {
    this.Router.navigate(['/kategorija/dodaj/']);
  }
}
