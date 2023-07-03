import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route } from '@angular/router';
import { ProizvodiUrediModel } from 'src/app/models/proizvodi-uredi.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
@Component({
  selector: 'app-proizvod-uredi',
  templateUrl: './proizvod-uredi.component.html',
  styleUrls: ['./proizvod-uredi.component.css']
})
export class ProizvodUrediComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute,private httpClient:HttpClient, private Router:Router) { }
  proizvodId:number;
    kolicina:number;
    cijena:number;
ngOnInit(): void {
    this.proizvodId=parseInt(this.activatedRoute.snapshot.paramMap.get("id")!);
    this.httpClient.get<ProizvodiUrediModel>(environment.apiUrl+"api/proizvodi/"+this.proizvodId).subscribe(data=>{
      this.cijena=data.cijena;
      this.kolicina=data.kolicina;
    });
  }
spremi(){


  this.httpClient.post<boolean>(environment.apiUrl+"api/proizvodi/uredi",{cijena:this.cijena,kolicina:this.kolicina,proizvodId:this.proizvodId}).subscribe(data=>{
  });
  this.Router.navigateByUrl('proizvodi/index');


}
}
