export interface CreateKupacDTO {
    SIFRA: number;
    NAZIV: string;
    ADRESA: string;
    MJESTO: string;
}

export interface GetKupacDTO {
    Id: number;
    SIFRA: number;
    NAZIV: string;
    ADRESA: string;
    MJESTO: string;
}

export interface UpdateKupacDTO {
    SIFRA: number;
    NAZIV: string;
    ADRESA: string;
    MJESTO: string;
}