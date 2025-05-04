export interface CreateKupacDTO {
    naziv: string;
    adresa: string;
    mjesto: string;
}

export interface GetKupacDTO {
    id: number;
    sifra: number;
    naziv: string;
    adresa: string;
    mjesto: string;
}

export interface UpdateKupacDTO {
    sifra: number;
    naziv: string;
    adresa: string;
    mjesto: string;
}