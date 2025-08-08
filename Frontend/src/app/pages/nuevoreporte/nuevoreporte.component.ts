import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from '../../models/usuario.model';
import { Vehiculo } from '../../models/vehiculo.model';
import { UsuarioService } from '../../services/api/usuario.service';
import { VehiculosService } from '../../services/api/vehiculos.service';
import { ReportesService } from '../../services/api/reportes.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { trigger, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-nuevoreporte',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './nuevoreporte.component.html',
  styleUrls: ['./nuevoreporte.component.scss'],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(-10px)' }),
        animate('300ms ease-out', style({ opacity: 1, transform: 'translateY(0)' })),
      ]),
    ]),
  ],
})
export class NuevoreporteComponent implements OnInit {
  usuario!: Usuario;
  vehiculos: Vehiculo[] = [];

  reporte = {
    vehiculoId: null,
    horaInicio: '',
    kmInicio: 0
  };

  fechaMostrada: string = '';
  fechaReporteISO: string = '';

  constructor(
    private usuarioService: UsuarioService,
    private vehiculosService: VehiculosService,
    private reportesService: ReportesService,
    private router: Router
  ) {}

  ngOnInit(): void {
  this.usuarioService.getUsuarioActual().subscribe(usuario => {
    this.usuario = usuario;
  });

  this.vehiculosService.getVehiculos().subscribe(data => {
    this.vehiculos = data;
  });

  const ahora = new Date();

  // 🔸 Ajustar la fecha al día anterior si es antes de las 6 AM
  if (ahora.getHours() < 6) {
    ahora.setDate(ahora.getDate() - 1);
  }

  // 🔸 Obtener la fecha como string en horario local
  const anio = ahora.getFullYear();
  const mes = String(ahora.getMonth() + 1).padStart(2, '0');
  const dia = String(ahora.getDate()).padStart(2, '0');
  const hora = String(ahora.getHours()).padStart(2, '0');
  const minutos = String(ahora.getMinutes()).padStart(2, '0');
  const segundos = String(ahora.getSeconds()).padStart(2, '0');

  this.fechaMostrada = `${anio}-${mes}-${dia}`;
  this.fechaReporteISO = `${anio}-${mes}-${dia}T${hora}:${minutos}:${segundos}`; // 🔹 sin Z, sin UTC
}



  iniciarSupervision() {
    const payload = {
      usuarioId: this.usuario.id,
      vehiculoId: this.reporte.vehiculoId,
      fecha: this.fechaReporteISO,
      horaInicio: this.reporte.horaInicio,
      kmInicio: this.reporte.kmInicio
    };

    this.reportesService.crearReporte(payload).subscribe({
      next: (nuevoReporte) => {
        this.router.navigate(['/reporte', nuevoReporte.id]);
      },
      error: (err) => {
        console.error('Error al crear reporte:', err);
        alert('Error al crear el reporte');
      }
    });
  }

  formularioValido(): boolean {
  return (
    !!this.reporte.horaInicio &&
    !!this.reporte.kmInicio &&
    !!this.reporte.vehiculoId
  );
}

}
