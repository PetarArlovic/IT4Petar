import { GetStavke_racunaDTO } from "./stavke_racuna";

export class shopRegister{
    stavke: GetStavke_racunaDTO[] = [];
    totalCost: number = 0;
    totalDiscount: number = 0;
    brojRacuna?: number;

    constructor(){
        this.stavke = [];
        this.totalCost = 0;
        this.totalDiscount = 0;
    }

    addStavka(stavka: GetStavke_racunaDTO){
        this.stavke.push(stavka);
        this.totalCost += stavka.cijena
    }

    calculateTotal(){
        let total = this.stavke.reduce((acc, stavka) => acc + stavka.cijena, 0);
        return total - this.totalDiscount;
    }
}

//<!--  ' '  <div>  []  {}  || -->