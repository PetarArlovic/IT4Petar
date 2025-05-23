export interface CreateProizvodDTO {
    sifra: number;
    naziv: string;
    jedinica_mjere: string;
    cijena: number;
    stanje: number;
    popust: number;
    proizvodSlikaUrl: string;
}

export interface GetProizvodDTO {
    id: number;
    sifra: number;
    naziv: string;
    jedinica_mjere: string;
    cijena: number;
    stanje: number;
    popust: number;
    proizvodSlikaUrl: string;
}

export interface UpdateProizvodDTO {
    sifra: number;
    naziv: string;
    jedinica_mjere: string;
    cijena: number;
    stanje: number;
    popust: number;
    proizvodSlikaUrl: string;
}

export interface CartProizvodDTO extends GetProizvodDTO {
    kolicina: number;
    popust: number;
    vrijednost: number;
    iznos_popusta?: number;
    sifra: number;
}