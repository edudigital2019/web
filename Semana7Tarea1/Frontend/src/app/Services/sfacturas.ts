import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class SFacturas {
  private http = inject(HttpClient);


  private readonly baseUrl = 'https://localhost:7056/api/Factura';

  private handleError(error: HttpErrorResponse) {
    let msg = 'Error de red';
    if (error.error) {
      if (typeof error.error === 'string') msg = error.error;
      else if (error.error.title) msg = error.error.title;
      else if (error.error.message) msg = error.error.message;
    }
    return throwError(() => new Error(msg));
  }

  getAll(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl).pipe(catchError(this.handleError));
  }

  getById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`).pipe(catchError(this.handleError));
  }

  create(f: any): Observable<any> {
    return this.http.post<any>(this.baseUrl, f).pipe(catchError(this.handleError));
  }

  update(id: number, f: any): Observable<void> {
  
    const payload = { id, ...f };
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload).pipe(catchError(this.handleError));
  }

  remove(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`).pipe(catchError(this.handleError));
  }
}
