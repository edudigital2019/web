import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { ILogin } from '../models/i-login';
import { IAuthRespuesta } from '../models/i-auth-respuesta';
import { Observable, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoginServicios {
  private readonly conexionHttp = inject(HttpClient);
  private readonly urlBaseApi = environment.apiBaseUrl;
  private readonly rutaLogin = '/api/Usuarios/login';

  iniciarSesion(credenciales: ILogin): Observable<IAuthRespuesta> {
    const urlCompleta = `${this.urlBaseApi}${this.rutaLogin}`;
    return this.conexionHttp.post<IAuthRespuesta>(urlCompleta, credenciales).pipe(
      tap((respuesta) => {
        localStorage.setItem('usuarioAutenticado', JSON.stringify(respuesta));
        if (respuesta.token) localStorage.setItem('token', respuesta.token);
      })
    );
  }

  cerrarSesion(): void {
    localStorage.removeItem('usuarioAutenticado');
    localStorage.removeItem('token');
  }

  obtenerUsuarioAutenticado(): IAuthRespuesta | null {
    const datos = localStorage.getItem('usuarioAutenticado');
    return datos ? (JSON.parse(datos) as IAuthRespuesta) : null;
  }

  estaAutenticado(): boolean {
    return !!this.obtenerUsuarioAutenticado();
  }
}
