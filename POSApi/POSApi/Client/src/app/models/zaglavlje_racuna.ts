export interface CreateZaglavlje_racunaDTO {
    broj: number;
    napomena: string;
    kupacId: number;
}

export interface GetZaglavlje_racunaDTO {
    broj: number;
    napomena: string;
    kupacId: number;
    datum: Date;
}

export interface UpdateZaglavlje_racunaDTO {
    broj: number;
    napomena: string;
    kupacId: number;
}