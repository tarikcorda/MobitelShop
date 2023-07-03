import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProizvodiIndexComponent } from './proizvodi/index/index.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ProizvodUrediComponent } from './proizvodi/proizvod-uredi/proizvod-uredi.component';
import { ProizvodDodajComponent } from './proizvodi/proizvod-dodaj/proizvod-dodaj.component';
import { KategorijaIndexComponent } from './kategorija/index/index.component';
import { KategorijaUrediComponent } from './kategorija/kategorija-uredi/kategorija-uredi.component';
import { KategorijaDodajComponent } from './kategorija/kategorija-dodaj/kategorija-dodaj.component';




@NgModule({
  declarations: [
    AppComponent,
    ProizvodiIndexComponent,
    ProizvodUrediComponent,
    ProizvodDodajComponent,
    KategorijaIndexComponent,
    KategorijaUrediComponent,
    KategorijaDodajComponent,
    
 

   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
