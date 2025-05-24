import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { environment } from '../Environment/environment';
import { Class } from '../models/class'; // asegurate de que la ruta sea correcta

@Injectable({
  providedIn: 'root'
})
export class SimulatorService {

  private apiUrl = environment.apiUrl;

  private homeCreatedSource = new Subject<void>();
  homeCreated$ = this.homeCreatedSource.asObservable();

  constructor(private http: HttpClient) {}

  createClass(newClass: Class): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/api/v1/classes`, newClass);
  }

  updateClass(id: string, updatedClass: Class): Observable<{ message: string }> {
    return this.http.put<{ message: string }>(`${this.apiUrl}/api/v1/classes/${id}`, updatedClass);
  }

  deleteClass(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/api/v1/classes/${id}`);
  }
}
