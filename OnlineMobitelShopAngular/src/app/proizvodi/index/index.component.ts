import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ProizvodiIndexModel } from 'src/app/models/proizvodi-index.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Component({
  selector: 'proizvodi-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class ProizvodiIndexComponent implements OnInit {

proizvodi:ProizvodiIndexModel;
pretraga:string;

 
  constructor(private httpClient:HttpClient, private Router: Router) { }

  ngOnInit(): void {
    this.httpClient.get<ProizvodiIndexModel>(environment.apiUrl+"api/proizvodi").subscribe(data=>{this.proizvodi=data});
    
  }
pretrazi(){
  this.httpClient.get<ProizvodiIndexModel>(environment.apiUrl+"api/proizvodi?naziv="+this.pretraga).subscribe(data=>{this.proizvodi=data});


}
izbrisi(id:number){
  this.httpClient.post<boolean>(environment.apiUrl+"api/proizvodi/izbrisi/"+id,null).subscribe(data=>{
    this.proizvodi.rows=this.proizvodi.rows.filter(i=>i.mobitelId!==id);
  });


}
Uredi(id : number){
  this.Router.navigate(['/proizvodi/uredi/', id]);
}
}
