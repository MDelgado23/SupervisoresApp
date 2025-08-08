import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../enviroments/enviroment';

@Injectable({ providedIn: 'root' })
export class VigiladoresService {
  private baseUrl = `${environment.apiUrl}/vigiladores`;

  constructor(private http: HttpClient) {}

  getVigiladores() {
    return this.http.get<any[]>(this.baseUrl);
  }
}
