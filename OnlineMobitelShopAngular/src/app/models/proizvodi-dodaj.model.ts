
export class ProizvodiDodajModel{
mobitelId:number 
naziv:string
kategorijaId:number
kategorija:SelectListItem[]
proizvodjacId:number
proizvodjac:SelectListItem[]
uvoznikId:number
uvoznik:SelectListItem[]
cijena:number
kolicina:number
ramMemorija:string
velicinaEkrana:string
kamera:string
memorija:string
}

export class SelectListItem{
value:string;
text:string;

}