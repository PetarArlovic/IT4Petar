export interface CreateProizvodDTO {
    sifra: number;
    naziv: string;
    jedinica_mjere: string;
    cijena: number;
    stanje: number;
    proizvodSlikaUrl: string;
}

export interface GetProizvodDTO {
    sifra: number;
    naziv: string;
    jedinica_mjere: string;
    cijena: number;
    stanje: number;
    proizvodSlikaUrl: string;
}

export interface UpdateProizvodDTO {
    sifra: number;
    naziv: string;
    jedinica_mjere: string;
    cijena: number;
    stanje: number;
    proizvodSlikaUrl: string;
}