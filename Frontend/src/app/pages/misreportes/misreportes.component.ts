import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReportesService } from '../../services/api/reportes.service';
import { UsuarioService } from '../../services/api/usuario.service';
import { SpinnerComponent } from "../../shared/spinner/spinner.component";

@Component({
  standalone: true,
  selector: 'app-mis-reportes',
  imports: [CommonModule, RouterModule, SpinnerComponent],
  templateUrl: './misreportes.component.html',
})
export class MisReportesComponent implements OnInit {
  reportes: { id: number, fecha: string, finalizado:boolean }[] = [];

  constructor(
    private reportesService: ReportesService,
    private usuarioService: UsuarioService
  ) {}

  diasMostrados = 31;

  ngOnInit(): void {
    this.usuarioService.getUsuarioActual().subscribe({
      next: (usuario) => {
        const id = usuario.id;
        this.cargarReportes(id);
      },
      error: (err) => {
        console.error('Error al obtener el usuario actual:', err);
      }
    });
  }

  cargarReportes(usuarioId: string) {
    
    
    this.reportesService.getIdsYFechasPorUsuario(usuarioId, this.diasMostrados).subscribe({
      next: (data) => {
        this.reportes = data;
      },
      error: (err) => {
        console.error('Error al obtener los reportes:', err);
      }
    });
  }

  irAReporte(id: number) {
    window.location.href = `/reporte/${id}`; 
  }
}
