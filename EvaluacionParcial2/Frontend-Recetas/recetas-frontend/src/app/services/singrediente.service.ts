import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import type { Iingrediente } from '../interfaces/iingrediente';

@Injectable({ providedIn: 'root' })
export class IngredienteService {
  private apiUrl = 'https://localhost:7007/api/ingredientes';
  private http = inject(HttpClient);

  private manejarError(e: HttpErrorResponse) {
    const msg = e.error?.message || e.message || `HTTP ${e.status}`;
    return throwError(() => new Error(msg));
  }

  listarIngredientes(): Observable<Iingrediente[]> {
    return this.http.get<Iingrediente[]>(this.apiUrl).pipe(catchError(this.manejarError));
  }
  crearIngrediente(ingrediente: Iingrediente): Observable<Iingrediente> {
    return this.http.post<Iingrediente>(this.apiUrl, ingrediente).pipe(catchError(this.manejarError));
  }
  eliminarIngrediente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(catchError(this.manejarError));
  }

  obtenerIngrediente(id: number): Observable<Iingrediente> {
    return this.http.get<Iingrediente>(`${this.apiUrl}/${id}`).pipe(catchError(this.manejarError));
  }
  actualizarIngrediente(id: number, ingrediente: Iingrediente): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, ingrediente).pipe(catchError(this.manejarError));
  }
}
