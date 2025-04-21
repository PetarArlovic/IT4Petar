export interface CreateKupacDTO {
    sifra: number;
    naziv: string;
    adresa: string;
    mjesto: string;
}

export interface GetKupacDTO {
    int: number;
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