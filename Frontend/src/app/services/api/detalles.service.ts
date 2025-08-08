import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReporteDetalle } from '../../models/reporteDetalle.model'; // ajustá el path si hace falta
import { Observable } from 'rxjs';
import { environment } from '../../../enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class ReporteDetalleService {
 private baseUrl = `${environment.apiUrl}/reportedetalles`; // reemplazá por tu endpoint real

  constructor(private http: HttpClient) {}

  createDetalle(detalle: ReporteDetalle): Observable<ReporteDetalle> {
  return this.http.post<ReporteDetalle>(`${this.baseUrl}`, detalle);
}

  updateDetalle(id: number, detalle: ReporteDetalle): Observable<ReporteDetalle> {
    return this.http.put<ReporteDetalle>(`${this.baseUrl}/${id}`, detalle);
  }

  deleteDetalle(id: number): Observable<void> {
  return this.http.delete<void>(`${this.baseUrl}/${id}`);
}
}
