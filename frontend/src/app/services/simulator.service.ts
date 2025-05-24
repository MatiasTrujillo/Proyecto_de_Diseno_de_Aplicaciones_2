import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../Environment/environment';
import { Session } from '../models/Session';

@Injectable({
  providedIn: 'root'
})
export class SimulatorService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  signIn(session: Session): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/sessions`, session);
  }

  logIn(token: string): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/sessions/logins/${token}`, null);
  }

  logOut(token: string): Observable<{ message: string }> {
    return this.http.patch<{ message: string }>(`${this.apiUrl}/sessions/logout/${token}`, null);
  }
}
