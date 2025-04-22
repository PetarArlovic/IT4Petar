export interface CreateProizvodDTO {
    SIFRA: number;
    NAZIV: string;
    JEDINICA_MJERE: string;
    CIJENA: number;
    STANJE: number;
    PROIZVODSlikaUrl: string;
}

export interface GetProizvodDTO {
    SIFRA: number;
    NAZIV: string;
    JEDINICA_MJERE: string;
    CIJENA: number;
    STANJE: number;
    PROIZVODSlikaUrl: string;
}

export interface UpdateProizvodDTO {
    SIFRA: number;
    NAZIV: string;
    JEDINICA_MJERE: string;
    CIJENA: number;
    STANJE: number;
    PROIZVODSlikaUrl: string;
}