import { GetProizvodDTO } from "./proizvodi";

export interface CreateStavke_racunaDTO {
    kolicina: number;
    cijena: number;
    popust: number;
    iznos_popusta: number;
    vrijednost: number;
    broj: number;
    sifra: number;
    proizvodId: number;
}

export interface GetStavke_racunaDTO {
    kolicina: number;
    cijena: number;
    popust: number;
    iznos_popusta: number;
    vrijednost: number;
    broj: number;
    proizvodId: number;
    naziv?: string;
    proizvod?: GetProizvodDTO
}

export interface UpdateStavke_racunaDTO {
    kolicina: number;
    cijena: number;
    popust: number;
    iznos_popusta: number;
    vrijednost: number;
    broj: number;
    proizvodId: number;
    zaglavlje_racunaId: number;
}