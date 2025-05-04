import { GetStavke_racunaDTO } from "./stavke_racuna";

export class shopResgister{
    stavke: GetStavke_racunaDTO[] = [];
    totalCost: number = 0;
    totalDiscount: number = 0;

    constructor(){
        this.stavke = [];
        this.totalCost = 0;
        this.totalDiscount = 0;
    }

    addStavka(stavka: GetStavke_racunaDTO){
        this.stavke.push(stavka);
        this.totalCost += stavka.cijena
    }

    calculateTotal(): number {
        return this.stavke.reduce((sum, stavka) => {
            const neto = stavka.cijena * stavka.kolicina;
            const iznosPopusta = (stavka.popust / 100) * neto;
            const ukupno = neto - iznosPopusta;
            return sum + ukupno;
        }, 0);
    }
}

//<!--  ' '  <div>  []  {}  || -->