import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../enviroments/enviroment';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private baseUrl = `${environment.apiUrl}/usuarios`;

  constructor(private http: HttpClient) {}

  getUsuarioActual() {
    return this.http.get<any>(`${this.baseUrl}/me`);
  }
}
