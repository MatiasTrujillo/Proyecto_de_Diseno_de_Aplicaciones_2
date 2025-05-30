import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../Environment/environment';
import { Session } from '../models/Session';
import { Class } from '../models/class';
import { Type } from '../models/Type';
import { Method } from '../models/method';
import { Attribute } from '../models/attribute';
import { LocalVariable } from '../models/localvariable';

import { Interface } from '../models/Interface';

@Injectable({
  providedIn: 'root'
})
export class SimulatorService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  signIn(session: Session): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/sessions`, session).pipe(
      tap((res) => {
        localStorage.setItem('authToken', res.token);
      }),
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  logIn(token?: string): Observable<{ message: string }> {
    const resolvedToken = token ?? localStorage.getItem('authToken');
    if (!resolvedToken) {
      return throwError(() => new Error('Token not found in localStorage.'));
    }

    return this.http.patch<{ message: string }>(`${this.apiUrl}/sessions/logins/${resolvedToken}`, null);
  }

  logOut(token?: string): Observable<{ message: string }> {
    const resolvedToken = token ?? localStorage.getItem('authToken');
    if (!resolvedToken) {
      return throwError(() => new Error('Token not found in localStorage.'));
    }

    return this.http.patch<{ message: string }>(`${this.apiUrl}/sessions/logout/${resolvedToken}`, null);
  }

  createClass(classData: Class): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/classes`, classData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  getTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(`${this.apiUrl}/classes`).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createMethod(method: Method): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/methods`, method).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createAttribute(attribute: Attribute): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/attributes`, attribute).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  createLocalVariable(localVariable: LocalVariable): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/localVariables`, localVariable).pipe(
      catchError((error) => throwError(() => error))
    );
  }

  createInterface(interfaceData: Interface): Observable<{ id: string }> {
    return this.http.post<{ id: string }>(`${this.apiUrl}/interfaces`, interfaceData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }
}
