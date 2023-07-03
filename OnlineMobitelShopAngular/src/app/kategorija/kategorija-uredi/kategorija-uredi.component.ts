import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { KategorijaUrediModel } from 'src/app/models/kategorija-uredi.model';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router'

@Component({
  selector: 'app-proizvodi-uredi',
  templateUrl: './kategorija-uredi.component.html',
  styleUrls: ['./kategorija-uredi.component.css']
})
export class KategorijaUrediComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute,private httpClient:HttpClient, private Router: Router) { }
    kategorijaId:number;
    naziv:string;
    ngOnInit(): void {
      this.kategorijaId=parseInt(this.activatedRoute.snapshot.paramMap.get("id")!);
      this.httpClient.get<KategorijaUrediModel>(environment.apiUrl+"api/kategorija/"+this.kategorijaId).subscribe(data=>{
        this.naziv=data.naziv;
      
      });

    }
    spremi(){
      this.httpClient.post<boolean>(environment.apiUrl+"api/kategorija/uredi",{naziv:this.naziv,kategorijaId:this.kategorijaId}).subscribe(data=>{
       this.Router.navigateByUrl('/kategorija/index');
      });
    }
}
