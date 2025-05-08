import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export const potvrdaSifreV: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
    const password = control.get('password');
    const potvrdiSifru = control.get ('potvrdiSifru');

    if (!password || !potvrdiSifru) {
        return null;
    }

    return password.value === potvrdiSifru.value ? null : {sifraPogresna: true}
}