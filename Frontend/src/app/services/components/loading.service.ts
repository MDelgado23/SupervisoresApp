import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class LoadingService {
  private cargaMinimaCompletada = false;
  private cargaRealCompletada = false;
  private callbackListos: (() => void)[] = [];

  private cargandoSubject = new BehaviorSubject<boolean>(false);
  public cargando$ = this.cargandoSubject.asObservable(); // 👈 público

  public cargar(callback: () => void): void {
    this.cargaMinimaCompletada = false;
    this.cargaRealCompletada = false;
    this.callbackListos = [callback];

    setTimeout(() => {
      this.cargaMinimaCompletada = true;
      this.verificarListo();
    }, 800); // 1 segundo mínimo
  }

  public notificarCargaRealCompletada(): void {
    this.cargaRealCompletada = true;
    this.verificarListo();
  }

  private verificarListo(): void {
    if (this.cargaMinimaCompletada && this.cargaRealCompletada) {
        this.cargandoSubject.next(false);
      this.callbackListos.forEach(cb => cb());
    }
  }

  get isCargando(): boolean {
  return !(this.cargaRealCompletada && this.cargaMinimaCompletada);
}

public reset(): void {
  this.cargaMinimaCompletada = true;
  this.cargaRealCompletada = true;
  this.cargandoSubject.next(false);
}
}
