export interface CreateZaglavlje_racunaDTO {
    BROJ: number;
    NAPOMENA: string;
    KUPACId: number;
}

export interface GetZaglavlje_racunaDTO {
    BROJ: number;
    NAPOMENA: string;
    KUPACId: number;
    DATUM: Date;
}

export interface UpdateZaglavlje_racunaDTO {
    BROJ: number;
    NAPOMENA: string;
    KUPACId: number;
}