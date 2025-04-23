export interface CreateStavke_racunaDTO {
    KOLICINA: number;
    CIJENA: number;
    POPUST: number;
    IZNOS_POPUSTA: number;
    VRIJEDNOST: number;
    BROJ: number;
    SIFRA: number;
}

export interface GetStavke_racunaDTO {
    KOLICINA: number;
    CIJENA: number;
    POPUST: number;
    IZNOS_POPUSTA: number;
    VRIJEDNOST: number;
    BROJ: number;
    PROIZVODId: number;
}

export interface UpdateStavke_racunaDTO {
    KOLICINA: number;
    CIJENA: number;
    POPUST: number;
    IZNOS_POPUSTA: number;
    VRIJEDNOST: number;
    BROJ: number;
    PROIZVODId: number;
    ZAGLAVLJE_RACUNAId: number;
}