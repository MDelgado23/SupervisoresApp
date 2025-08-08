import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../enviroments/enviroment';
import { Objetivo } from '../../models/objetivo.model';

@Injectable({ providedIn: 'root' })
export class ObjetivosService {
  private baseUrl = `${environment.apiUrl}/objetivos`;

  constructor(private http: HttpClient) {}

  getObjetivos() {
    return this.http.get<Objetivo[]>(this.baseUrl);
  }
}
