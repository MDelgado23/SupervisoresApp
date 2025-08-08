import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { VehiculosService } from '../../services/api/vehiculos.service';
import { UsuarioService } from '../../services/api/usuario.service';
import { ReportesService } from '../../services/api/reportes.service';
import { ObjetivosService } from '../../services/api/objetivos.service';
import { ActividadesService } from '../../services/api/actividades.service';
import { VigiladoresService } from '../../services/api/vigiladores.service';
import { ReporteDetalleService } from '../../services/api/detalles.service';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SpinnerComponent } from '../../shared/spinner/spinner.component';
import { LoadingService } from '../../services/components/loading.service';
import { ConfirmService } from '../../services/components/confirm.service';
import { ReporteDetalle } from '../../models/reporteDetalle.model';

type Calificacion = {
  [key: string]: number;
  horario: number;
  presencia: number;
  elementos: number;
  documentos: number;
  atencion: number;
  puestos: number;
};

@Component({
  selector: 'app-editarreporte',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, SpinnerComponent],
  templateUrl: './editarreporte.component.html',
})
export class EditarReporteComponent implements OnInit {
  usuario: any;
  vehiculos: any[] = [];
  objetivos: any[] = [];
  actividades: any[] = [];
  vigiladores: any[] = [];
  fechaReporteISO: string = '';
  fechaMostrada: string = '';
  reporteId!: number;
  reporteCargado: boolean = false;

  reporte = {
    vehiculoId: null,
    horaInicio: '',
    horaFin: '',
    kmInicio: 0,
    kmFin: 0,
    finalizado: false,
    observaciones: '',
  };

  detalleReportes: any[] = [];

  calificacionKeys: (keyof Calificacion)[] = [
    'horario',
    'presencia',
    'elementos',
    'documentos',
    'atencion',
    'puestos',
  ];

  calificacionLabels: { [key: string]: string } = {
    horario: 'Horario',
    presencia: 'Presencia',
    elementos: 'Elementos',
    documentos: 'Documentos',
    atencion: 'Atención',
    puestos: 'Puestos',
  };

  mostrarHoraFin = false;
  mostrarKmFin = false;
  mostrarObservaciones = false;
  mostrarCompletar = false;
  mostrarResumenConfirmacion = false;

  detalleEditando: boolean = false; // para manejar si se está editando un detalle
  detalleActual: any = null; // almacena el detalle que se está cargando antes de guardarlo
  editandoIniciales: boolean = false;

  // detalleExpandidoIndex: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private usuarioService: UsuarioService,
    private vehiculoService: VehiculosService,
    private objetivoService: ObjetivosService,
    private actividadService: ActividadesService,
    private vigiladorService: VigiladoresService,
    private reportesService: ReportesService,
    private detallesService: ReporteDetalleService,
    private loadingService: LoadingService,
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {}

  ngOnInit(): void {
    this.reporteId = Number(this.route.snapshot.paramMap.get('id'));

    this.usuarioService.getUsuarioActual().subscribe((u) => (this.usuario = u));
    this.vehiculoService.getVehiculos().subscribe((v) => (this.vehiculos = v));
    this.objetivoService.getObjetivos().subscribe((o) => (this.objetivos = o));
    this.vigiladorService
      .getVigiladores()
      .subscribe((v) => (this.vigiladores = v));

    this.actividadService.getActividades().subscribe((a) => {
      this.actividades = a;

      this.cargarReporte(); // ahora el spinner se activa desde acá
    });
  }

  cargarReporte() {
    this.loadingService.cargar(() => {
      this.reporteCargado = true;
    });

    this.reportesService.getReporteById(this.reporteId).subscribe((data) => {
      this.reporte = {
        vehiculoId: data.vehiculoId,
        horaInicio: data.horaInicio?.slice(0, 5),
        horaFin: data.horaFin?.slice(0, 5),
        kmInicio: data.kmInicio,
        kmFin: data.kmFin,
        finalizado: data.finalizado,
        observaciones: data.observaciones,
      };

      this.fechaReporteISO = data.fecha;

      const fecha = new Date(data.fecha);
      const dia = String(fecha.getDate()).padStart(2, '0');
      const mes = String(fecha.getMonth() + 1).padStart(2, '0');
      const anio = fecha.getFullYear();
      this.fechaMostrada = `${dia}/${mes}/${anio}`;

      this.detalleReportes = data.detalleReportes.map((d: any) => ({
        id: d.id,
        reporteId: d.reporteId,
        objetivoId: d.objetivoId,
        horaEntrada: d.horaEntrada?.slice(0, 5),
        horaSalida: d.horaSalida?.slice(0, 5),
        vigiladorId: d.vigiladorId,
        actividadId: d.actividadId,
        descripcion: d.actividadDescripcion,
        calificacion: {
          horario: d.horario,
          presencia: d.presencia,
          elementos: d.elementos,
          documentos: d.documentos,
          atencion: d.atencion,
          puestos: d.puestos,
        },
      }));

      this.loadingService.notificarCargaRealCompletada(); // 🔁 ahora sí se apaga el spinner
    });
  }

  agregarDetalle() {
    if (this.detalleActual) {
      const error = this.validarDetalle(
        this.detalleActual,
        this.detalleReportes.length
      );
      if (error) {
        this.toastr.error(error);
        return;
      }

      this.detalleReportes.push({
        ...this.detalleActual,
        expandido: false,
      });
      this.detalleActual = null;
    }

    // Cerrar todos los detalles previos
    this.detalleReportes.forEach((d) => (d.expandido = false));

    // Iniciar uno nuevo
    this.detalleActual = {
      objetivoId: null,
      horaEntrada: '',
      horaSalida: '',
      vigiladorId: null,
      actividadId: null,
      descripcion: '',
      calificacion: {
        horario: 0,
        presencia: 0,
        elementos: 0,
        documentos: 0,
        atencion: 0,
        puestos: 0,
      },
    };

    this.detalleEditando = true;
  }

  validarDetalle(detalle: ReporteDetalle, index: number): string | null {
    if (!detalle.objetivoId || !detalle.vigiladorId || !detalle.actividadId) {
      return 'Todos los campos del detalle son obligatorios';
    }

    if (!detalle.horaEntrada || !detalle.horaSalida) {
      return 'Debe completar la hora de entrada y salida';
    }

    const entrada = this.convertirAHora(detalle.horaEntrada);
    const salida = this.convertirAHora(detalle.horaSalida);
    const horaInicio = this.convertirAHora(this.reporte.horaInicio);

    if (isNaN(entrada) || isNaN(salida)) {
      return 'Formato de hora inválido';
    }

    if (entrada < horaInicio) {
      return 'La hora de entrada no puede ser anterior a la hora de inicio del reporte';
    }

    if (salida <= entrada) {
      return 'La hora de salida debe ser posterior a la entrada';
    }

    // 🔁 Validar que no se solape con otros detalles
    for (let i = 0; i < this.detalleReportes.length; i++) {
      if (i === index) continue;

      const otro = this.detalleReportes[i];
      if (!otro.horaEntrada || !otro.horaSalida) continue;

      const entradaOtro = this.convertirAHora(otro.horaEntrada);
      const salidaOtro = this.convertirAHora(otro.horaSalida);

      const seSolapan =
        (entrada >= entradaOtro && entrada < salidaOtro) ||
        (salida > entradaOtro && salida <= salidaOtro) ||
        (entrada <= entradaOtro && salida >= salidaOtro);

      if (seSolapan) {
        return `El detalle se superpone con el detalle #${i + 1} (${
          otro.horaEntrada
        } - ${otro.horaSalida})`;
      }
    }

    // ✅ Mismo vigilador puede aparecer varias veces en el mismo objetivo
    // 🚫 Pero no puede estar en dos objetivos distintos
    for (let i = 0; i < this.detalleReportes.length; i++) {
      if (i === index) continue;

      const otro = this.detalleReportes[i];
      if (
        otro.vigiladorId === detalle.vigiladorId &&
        otro.objetivoId !== detalle.objetivoId
      ) {
        return `El vigilador ya está asignado a otro objetivo en el detalle #${
          i + 1
        }`;
      }
    }

    // ✅ Validar que cada calificación esté entre 0 y 5
    for (const key of this.calificacionKeys) {
      const valor = (detalle as any).calificacion?.[key];
      if (valor < 0 || valor > 5) {
        return `La calificación "${this.calificacionLabels[key]}" debe estar entre 0 y 5`;
      }
    }

    return null;
  }

  finalizarSupervision() {
    if (this.detalleActual) {
      const error = this.validarDetalle(
        this.detalleActual,
        this.detalleReportes.length
      );
      if (error) {
        this.toastr.error(error);
        return; // 🚫 Detalle inválido, no seguir
      }

      // ✅ Detalle válido → guardar
      this.detalleReportes.push({ ...this.detalleActual });
      this.detalleActual = null;
    }

    this.detalleEditando = false;
    this.mostrarHoraFin = true;
  }
  confirmarFinalizacion() {
    this.mostrarResumenConfirmacion = true;
  }

  cancelarResumen() {
    this.mostrarResumenConfirmacion = false;
  }

  async eliminarDetalle(index: number): Promise<void> {
    const detalle = this.detalleReportes[index];

    const confirmado = await this.confirmService.confirm(
      '¿Estás seguro?',
      'Esta acción eliminará el detalle.',
      'Sí, eliminar'
    );

    if (!confirmado) return;

    this.loadingService.cargar(() => {
      this.toastr.success('Detalle eliminado con éxito');
    });

    if (detalle.id) {
      this.detallesService.deleteDetalle(detalle.id).subscribe({
        next: () => {
          this.detalleReportes.splice(index, 1);
          this.loadingService.notificarCargaRealCompletada();
        },
        error: (err) => {
          console.error('Error al eliminar el detalle:', err);
          this.toastr.error('Error al eliminar el detalle. Ver consola.');
          this.loadingService.notificarCargaRealCompletada();
        },
      });
    } else {
      this.detalleReportes.splice(index, 1);
      this.loadingService.notificarCargaRealCompletada();
    }
  }

  guardarCambios() {
    this.loadingService.cargar(() => {
      this.toastr.success('Reporte actualizado con éxito');
    });

    const payload: any = {
      usuarioId: this.usuario.id,
      vehiculoId: this.reporte.vehiculoId,
      fecha: this.fechaReporteISO,
      horaInicio: this.agregarSegundos(this.reporte.horaInicio),
      horaFin: this.agregarSegundos(this.reporte.horaFin),
      kmInicio: this.reporte.kmInicio,
      kmFin: this.reporte.kmFin,
      finalizado: this.reporte.finalizado,
      observaciones: this.reporte.observaciones,
      detalleReportes: this.detalleReportes.map((d) => ({
        objetivoId: d.objetivoId,
        horaEntrada: this.agregarSegundos(d.horaEntrada),
        horaSalida: this.agregarSegundos(d.horaSalida),
        vigiladorId: d.vigiladorId,
        actividadId: d.actividadId,
        actividadDescripcion: d.descripcion,
        horario: d.calificacion.horario,
        presencia: d.calificacion.presencia,
        elementos: d.calificacion.elementos,
        documentos: d.calificacion.documentos,
        atencion: d.calificacion.atencion,
        puestos: d.calificacion.puestos,
      })),
    };

    if (this.reporte.finalizado) {
      payload.hsTotales = this.toTimeSpanStr(this.hsTotalesNum);
      payload.hsSupervision = this.toTimeSpanStr(this.hsSupervision);
      payload.hsAdministrativas = this.toTimeSpanStr(this.hsAdministrativas);
      payload.hsApoyo = this.toTimeSpanStr(this.hsApoyo);
      payload.hsTranslados = this.toTimeSpanStr(this.hsTranslados);
    }

    console.log('Payload generado:', JSON.stringify(payload));

    this.reportesService.updateReporte(this.reporteId, payload).subscribe({
      next: () => {
        this.loadingService.notificarCargaRealCompletada();
      },
      error: (err) => {
        this.loadingService.notificarCargaRealCompletada(); // 🔁 para evitar spinner infinito en errores
        const mensaje = err.error?.errors || err.error || 'Error desconocido';
        console.error('Error al actualizar el reporte:', mensaje);
        this.toastr.error(mensaje);
      },
    });
  }

  esReporteValido(): boolean {
    if (
      !this.reporte.vehiculoId ||
      !this.reporte.horaInicio ||
      !this.reporte.horaFin ||
      this.reporte.kmInicio == null ||
      this.reporte.kmFin == null
    ) {
      return false;
    }

    if (!this.detalleReportes.length) return false;

    for (const d of this.detalleReportes) {
      if (
        !d.objetivoId ||
        !d.horaEntrada ||
        !d.horaSalida ||
        !d.vigiladorId ||
        !d.actividadId
      ) {
        return false;
      }

      if (!d.calificacion) return false;

      const requiredKeys = [
        'horario',
        'presencia',
        'elementos',
        'documentos',
        'atencion',
        'puestos',
      ];
      for (const key of requiredKeys) {
        if (d.calificacion[key] == null || d.calificacion[key] === '') {
          return false;
        }
      }
    }

    return true;
  }

  finalizarReporte() {
    this.reporte.finalizado = true;
    this.guardarCambios();
    this.mostrarResumenConfirmacion = false;
    this.mostrarHoraFin = false;
    this.mostrarKmFin = false;
    this.mostrarObservaciones = false;
    this.mostrarCompletar = false;
  }

  get kmTotal() {
    return this.reporte.kmFin - this.reporte.kmInicio;
  }

  get hsTotalesStr(): string {
    const horas = this.hsTotalesNum;
    const horasEnteras = Math.floor(horas);
    const minutos = Math.round((horas - horasEnteras) * 60);
    return `${horasEnteras}h ${minutos}m`;
  }

  get hsTotalesNum(): number {
    if (!this.reporte.horaInicio || !this.reporte.horaFin) return 0;

    const [h1, m1] = this.reporte.horaInicio.split(':').map(Number);
    const [h2, m2] = this.reporte.horaFin.split(':').map(Number);

    let minutosInicio = h1 * 60 + m1;
    let minutosFin = h2 * 60 + m2;

    // Si fin <= inicio, asumimos que cruzó medianoche
    if (minutosFin <= minutosInicio) {
      minutosFin += 24 * 60;
    }

    const diff = minutosFin - minutosInicio;
    return diff / 60;
  }

  get hsSupervision(): number {
    return this.sumarHorasPorTipo([1]); // minutos
  }

  get hsAdministrativas(): number {
    return this.sumarHorasPorTipo([2]); // minutos
  }

  get hsApoyo(): number {
    return this.sumarHorasPorTipo([3]); // minutos
  }

  get hsTranslados(): number {
    return (
      this.hsTotalesNum -
      (this.hsSupervision + this.hsAdministrativas + this.hsApoyo)
    );
  }

  private sumarHorasPorTipo(tipoIds: number[]): number {
    return this.detalleReportes.reduce((total, d) => {
      const actividad = this.actividades.find(
        (a) => a.id === Number(d.actividadId)
      );
      if (!actividad || !tipoIds.includes(actividad.tipo)) return total;
      if (!d.horaEntrada || !d.horaSalida) return total;

      const [h1, m1] = d.horaEntrada.split(':').map(Number);
      const [h2, m2] = d.horaSalida.split(':').map(Number);

      let minutosInicio = h1 * 60 + m1;
      let minutosFin = h2 * 60 + m2;

      if (minutosFin <= minutosInicio) {
        minutosFin += 24 * 60;
      }

      const diff = minutosFin - minutosInicio;
      return total + diff / 60;
    }, 0);
  }

  toTimeSpanStr(minutos: number): string {
    const totalSeconds = Math.round(minutos * 60);
    const hours = Math.floor(totalSeconds / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;

    const hh = String(hours).padStart(2, '0');
    const mm = String(minutes).padStart(2, '0');
    const ss = String(seconds).padStart(2, '0');

    return `${hh}:${mm}:${ss}`;
  }

  fromTimeSpanToDecimal(timeSpan: string): number {
    const [h, m, s] = timeSpan.split(':').map(Number);
    return h + m / 60 + s / 3600;
  }

  guardarDetalle() {
    if (!this.detalleActual) return;

    const error = this.validarDetalle(
      this.detalleActual,
      this.detalleReportes.length
    );
    if (error) {
      this.toastr.error(error);
      return;
    }

    this.detalleReportes.push({
      ...this.detalleActual,
      expandido: false,
    });

    this.detalleActual = null;
    this.detalleEditando = false;
    this.detalleReportes.forEach((d) => (d.expandido = false));

    this.guardarCambios();
  }

  cancelarDetalle() {
    this.detalleActual = null;
    this.detalleEditando = false;
  }

  toggleDetalle(index: number): void {
    // Si hay un detalle en edición, lo cerramos
    this.detalleActual = null;
    this.detalleEditando = false;

    // Expandimos solo el clickeado y cerramos los demás
    this.detalleReportes.forEach((d, i) => {
      d.expandido = i === index ? !d.expandido : false;
    });
    if (this.mostrarHoraFin || this.mostrarKmFin || this.mostrarCompletar) {
      this.cancelarFinalizacion();
    }
  }

  getNombreObjetivo(id: number | null): string {
    const objetivo = this.objetivos.find((o) => o.id === id);
    return objetivo ? objetivo.name : '';
  }

  editarDatosIniciales() {
    this.editandoIniciales = true;
  }

  guardarDatosIniciales() {
    this.editandoIniciales = false;
    this.guardarCambios(); // reaprovechás el método que ya guarda todo
  }

  guardarCambiosDetalle(index: number): void {
    const detalle = this.detalleReportes[index];
    const error = this.validarDetalle(detalle, index);

    if (error) {
      this.toastr.error(error);
      return;
    }

    this.loadingService.cargar(() => {
      this.toastr.success('Detalle guardado con éxito');
    });

    const horaEntrada = this.agregarSegundos(detalle.horaEntrada);
    const horaSalida = this.agregarSegundos(detalle.horaSalida);

    const detalleParaEnviar: any = {
      updated: new Date().toISOString(),
      id: detalle.id,
      reporteId: detalle.reporteId ?? this.reporteId,
      objetivoId: detalle.objetivoId,
      vigiladorId: detalle.vigiladorId,
      actividadId: detalle.actividadId,
      actividadDescripcion: detalle.descripcion,
      atencion: detalle.calificacion.atencion,
      documentos: detalle.calificacion.documentos,
      puestos: detalle.calificacion.puestos,
      elementos: detalle.calificacion.elementos,
      presencia: detalle.calificacion.presencia,
      horario: detalle.calificacion.horario,
    };

    if (horaEntrada) detalleParaEnviar.horaEntrada = horaEntrada;
    if (horaSalida) detalleParaEnviar.horaSalida = horaSalida;

    const callback = {
      next: (res: any) => {
        detalle.expandido = false;
        if (!detalle.id && res?.id) detalle.id = res.id;
        this.loadingService.notificarCargaRealCompletada(); // 🔁 apagamos el spinner
      },
      error: (err: any) => {
        console.error('❌ Error al guardar el detalle:', err);
        this.toastr.error('Error al guardar el detalle. Ver consola.');
        this.loadingService.notificarCargaRealCompletada(); // 🔁 apagamos igual en caso de error
      },
    };

    if (detalle.id) {
      this.detallesService
        .updateDetalle(detalle.id, detalleParaEnviar)
        .subscribe(callback);
    } else {
      this.detallesService.createDetalle(detalleParaEnviar).subscribe(callback);
    }
  }

  puedeFinalizarReporte(): boolean {
    if (!this.detalleReportes.length) return false;

    for (let i = 0; i < this.detalleReportes.length; i++) {
      const error = this.validarDetalle(this.detalleReportes[i], i);
      if (error) {
        console.warn(`Detalle #${i + 1} inválido:`, error);
        return false;
      }
    }

    return true;
  }

  iniciarFinalizacion(): void {
    this.mostrarHoraFin = true;
    this.mostrarKmFin = false;
    this.mostrarCompletar = false;
  }

  cancelarFinalizacion(): void {
    this.mostrarHoraFin = false;
    this.mostrarKmFin = false;
    this.mostrarCompletar = false;
  }

  async confirmarFinalizarReporte() {
    const { horaInicio, horaFin, kmInicio, kmFin } = this.reporte;

    if (!horaFin || !horaInicio) {
      this.toastr.error('Debe completar la hora de inicio y finalización.');
      return;
    }

    const inicio = this.convertirAHora(horaInicio);
    const fin = this.convertirAHora(horaFin);

    if (fin <= inicio) {
      this.toastr.error(
        'La hora de finalización no puede ser anterior o igual a la hora de inicio.'
      );
      return;
    }

    // 🔹 Validar que horaFin sea posterior a la última horaSalida
    const horaSalidaMax = this.detalleReportes.reduce((max, d) => {
      if (!d.horaSalida) return max;
      const salida = this.convertirAHora(d.horaSalida);
      return salida > max ? salida : max;
    }, 0);

    if (fin <= horaSalidaMax) {
      this.toastr.error(
        'La hora de finalización no puede ser anterior o igual a la última hora de salida registrada.'
      );
      return;
    }

    if (kmFin != null && kmInicio != null && kmFin < kmInicio) {
      this.toastr.error(
        'El kilómetro final no puede ser menor al kilómetro inicial.'
      );
      return;
    }

    const confirmado = await this.confirmService.confirm(
      '¿Finalizar reporte?',
      'Una vez completado, no podrás editarlo.',
      'Sí, finalizar'
    );

    if (!confirmado) return;

    this.finalizarReporte(); // ✅ solo si todo es válido
  }

  agregarSegundos(hora: string | null | undefined): string | null {
    if (!hora || hora.trim() === '' || !hora.includes(':')) return null;

    if (hora.length === 8 && hora.split(':').length === 3) return hora;
    if (hora.length === 5 && hora.split(':').length === 2) return `${hora}:00`;

    return null;
  }

  convertirAHora(hora: string): number {
    const [h, m] = hora.split(':').map(Number);
    return h * 60 + m;
  }

  onHoraFinChange() {
    if (this.reporte.horaFin?.trim()) {
      this.mostrarKmFin = true;
    } else {
      this.mostrarKmFin = false;
      this.mostrarObservaciones = false;
      this.mostrarCompletar = false;
    }
  }

  onKmFinChange() {
    if (this.reporte.kmFin !== null && this.reporte.kmFin !== undefined) {
      this.mostrarObservaciones = true;
      this.mostrarCompletar = true;
    } else {
      this.mostrarObservaciones = false;
      this.mostrarCompletar = false;
    }
  }
}
