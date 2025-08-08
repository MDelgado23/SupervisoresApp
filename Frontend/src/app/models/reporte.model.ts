import { ReporteDetalle } from './reporteDetalle.model';

export interface Reporte {
  id: number;
  usuarioId: number;
  vehiculoId: number | null;
  fecha: string;
  horaInicio: string;
  horaFin: string;
  kmInicio: number;
  kmFin: number;
  hsTotales: string;
  hsSupervision: string;
  hsAdministrativas: string;
  hsApoyo: string;
  hsTranslados: string;
  observaciones: string;
  finalizado: boolean;
  detalleReportes: ReporteDetalle[];
}
