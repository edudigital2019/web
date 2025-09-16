import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IProducto } from '../Interfaces/iproducto';

@Injectable({ providedIn: 'root' })
export class SProductos {
  private http = inject(HttpClient);

  private readonly baseUrl = 'https://localhost:7056/api/Producto';

  private handleError(error: HttpErrorResponse) {
    const msg = error.error?.message || error.statusText || 'Error de red';
    return throwError(() => new Error(msg));
  }

  getAll(): Observable<IProducto[]> {
    return this.http.get<IProducto[]>(this.baseUrl).pipe(catchError(this.handleError));
  }
  getById(id: number): Observable<IProducto> {
    return this.http.get<IProducto>(`${this.baseUrl}/${id}`).pipe(catchError(this.handleError));
  }
  create(p: Omit<IProducto, 'id'>): Observable<IProducto> {
    return this.http.post<IProducto>(this.baseUrl, p).pipe(catchError(this.handleError));
  }
  update(id: number, p: Omit<IProducto, 'id'>) {
  const payload: IProducto = { id, ...p } as IProducto;   // ← incluye id para scaffold .NET
  return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
}
  remove(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(catchError(this.handleError));
  }
}
