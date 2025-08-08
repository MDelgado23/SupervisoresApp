import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../enviroments/enviroment';

@Injectable({ providedIn: 'root' })
export class VehiculosService {
  private baseUrl = `${environment.apiUrl}/vehiculos`;

  constructor(private http: HttpClient) {}

  getVehiculos() {
    return this.http.get<any[]>(this.baseUrl);
  }
}
