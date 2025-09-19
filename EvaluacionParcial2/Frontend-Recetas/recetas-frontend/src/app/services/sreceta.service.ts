import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import type { Ireceta } from '../interfaces/ireceta';

@Injectable({ providedIn: 'root' })
export class RecetaService {

  private apiUrl = 'https://localhost:7007/api/recetas';
  private http = inject(HttpClient);

  private manejarError(e: HttpErrorResponse) {
    const msg = e.error?.message || e.message || `HTTP ${e.status}`;
    return throwError(() => new Error(msg));
  }

  listarRecetas(): Observable<Ireceta[]> {
    return this.http.get<Ireceta[]>(this.apiUrl).pipe(catchError(this.manejarError));
  }

  obtenerReceta(id: number): Observable<Ireceta> {
    return this.http.get<Ireceta>(`${this.apiUrl}/${id}`).pipe(catchError(this.manejarError));
  }

  crearReceta(receta: Ireceta): Observable<Ireceta> {
    return this.http.post<Ireceta>(this.apiUrl, receta).pipe(catchError(this.manejarError));
  }

  actualizarReceta(id: number, receta: Ireceta): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, receta).pipe(catchError(this.manejarError));
  }

  eliminarReceta(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(catchError(this.manejarError));
  }
}
