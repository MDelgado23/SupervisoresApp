import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../enviroments/enviroment';

@Injectable({ providedIn: 'root' })
export class ActividadesService {
  private baseUrl = `${environment.apiUrl}/actividades`;

  constructor(private http: HttpClient) {}

  getActividades() {
    return this.http.get<any[]>(this.baseUrl);
  }
}
