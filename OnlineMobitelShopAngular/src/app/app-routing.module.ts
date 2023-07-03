import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProizvodDodajComponent } from './proizvodi/proizvod-dodaj/proizvod-dodaj.component';
import { ProizvodiIndexComponent } from './proizvodi/index/index.component';
import { ProizvodUrediComponent } from './proizvodi/proizvod-uredi/proizvod-uredi.component';
import { KategorijaIndexComponent } from './kategorija/index/index.component';
import { KategorijaUrediComponent } from './kategorija/kategorija-uredi/kategorija-uredi.component';
import { KategorijaDodajComponent } from './kategorija/kategorija-dodaj/kategorija-dodaj.component';
const routes: Routes = [
  {
    path:'proizvodi/index',
    component: ProizvodiIndexComponent
  },
  {
path:"proizvodi/uredi/:id",
component:ProizvodUrediComponent

  },
  {
path:"proizvodi/dodaj",
component:ProizvodDodajComponent

  },
  {
    path:"kategorija/index",
    component:KategorijaIndexComponent
    
      },
      {
        path:"kategorija/uredi/:id",
        component:KategorijaUrediComponent
        
          },
          {
            path:"kategorija/dodaj",
            component:KategorijaDodajComponent
            
              }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
