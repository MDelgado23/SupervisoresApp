import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../enviroments/enviroment';
import { Observable } from 'rxjs';
import { Reporte } from '../../models/reporte.model';

@Injectable({ providedIn: 'root' })
export class ReportesService {
  private baseUrl = `${environment.apiUrl}/reportes`;

  constructor(private http: HttpClient) {}

  getReportes(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  getReporteById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  crearReporte(reporte: any) {
    return this.http.post<Reporte>(this.baseUrl, reporte);
  }

  updateReporte(id: number, reporte: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, reporte);
  }

  getIdsYFechasPorUsuario(usuarioId: string, dias: number = 31) {
  return this.http.get<{ id: number, fecha: string, finalizado:boolean }[]>(
    `${this.baseUrl}/ids-usuario/${usuarioId}?dias=${dias}`
  );
}
}
