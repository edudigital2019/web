import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { IPersonas } from '../Interfaces/i-personas';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class PersonasService {
  private apiUrl = 'https://localhost:7128/api/PersonasApi'; // Cambia esto por tu endpoint real

  constructor(private http: HttpClient) {}

  getAll(): Observable<IPersonas[]> {
    var personas = this.http
      .get<IPersonas[]>(this.apiUrl)
      .pipe(catchError(this.manejoErrores));
    return personas;
  }

  getById(id: number): Observable<IPersonas> {
    return this.http
      .get<IPersonas>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.manejoErrores));
  }

  create(persona: IPersonas): Observable<IPersonas> {
    return this.http.post<IPersonas>(this.apiUrl, persona);
  }

  update(cliente: IPersonas): Observable<IPersonas> {
    return this.http
      .put<IPersonas>(`${this.apiUrl}/${cliente.id}`, cliente)
      .pipe(catchError(this.manejoErrores));
  }

  delete(id:number): Observable<number>{
    return this.http
      .delete<number>(`${this.apiUrl}/${id}`)
      .pipe(catchError(this.manejoErrores));
  }
  manejoErrores(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => {
      new Error(msg);
    });
  }
}
