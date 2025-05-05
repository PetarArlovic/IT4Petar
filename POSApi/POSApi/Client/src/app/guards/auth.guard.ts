import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { DecodedToken } from '../models/decodeToken';

export const authGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const router = inject(Router);
  const token = sessionStorage.getItem('token');

  if (route.data['public']) {
    return true;
  }

  if (!token) {
    return router.createUrlTree(['/login']);
  }

  try {
    const decodedToken = jwtDecode<DecodedToken>(token);
    const roles = getRolesFromToken(decodedToken);

    if (roles.includes('Admin') && !route.data['adminExcluded']) {
      return true;
    }

    else if (roles.includes('User') && route.data['userAllowed']) {
      return true;
    }

    else {
      return router.createUrlTree(['/login']);
    }

  }

  catch (error) {
    console.error('Greška prilikom dekodiranja tokena:', error);
    return router.createUrlTree(['/login']);
  }
};

const getRolesFromToken = (decodedToken: DecodedToken): string[] => {
  const roleClaim = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || decodedToken.role;

  if (Array.isArray(roleClaim)) return roleClaim;
  if (typeof roleClaim === 'string') return [roleClaim]
  return [];
}



//' '<>[]{}