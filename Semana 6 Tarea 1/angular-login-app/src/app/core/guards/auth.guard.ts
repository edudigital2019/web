import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { LoginServicios } from '../services/login-servicios';

export const authGuard: CanActivateFn = () => {
  const servicioAutenticacion = inject(LoginServicios);
  const rutaNavegacion = inject(Router);

  if (servicioAutenticacion.estaAutenticado()) {
    return true;
  }

  rutaNavegacion.navigate(['/login']);
  return false;
};
