export interface DecodedToken {
    role?: string | string[];
    [key: string]: any; // Ovo omogućava da uključite i druge ključeve koje vaš token može imati
}