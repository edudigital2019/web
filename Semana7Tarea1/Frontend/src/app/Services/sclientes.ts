import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ICliente } from '../Interfaces/icliente';

@Injectable({ providedIn: 'root' })
export class SClientes {
  private http = inject(HttpClient);
  private readonly baseUrl = 'https://localhost:7056/api/Cliente';

  private handleError(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => new Error(msg));
  }

  getAll(): Observable<ICliente[]> {
    return this.http.get<ICliente[]>(this.baseUrl).pipe(catchError(this.handleError));
  }
  getById(id: number): Observable<ICliente> {
    return this.http.get<ICliente>(`${this.baseUrl}/${id}`).pipe(catchError(this.handleError));
  }
  create(cliente: Omit<ICliente, 'id'>): Observable<ICliente> {
    return this.http.post<ICliente>(this.baseUrl, cliente).pipe(catchError(this.handleError));
  }
  update(id: number, cliente: Omit<ICliente, 'id'>) {
  const payload: ICliente = { id, ...cliente } as ICliente;
  return this.http.put<void>(`${this.baseUrl}/${id}`, payload)
}
  remove(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(catchError(this.handleError));
  }
}
