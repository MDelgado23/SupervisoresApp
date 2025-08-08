import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SpinnerComponent } from '../../shared/spinner/spinner.component';
import { LoadingService } from '../../services/components/loading.service';

@Component({
  standalone: true,
  selector: 'app-menu',
  imports: [CommonModule, SpinnerComponent],
  templateUrl: './menu.component.html',
})
export class MenuComponent {
  constructor(
    private router: Router,
    public loadingService: LoadingService
  ) {}

  ngOnInit(): void {
  this.loadingService.reset();
}

  irANuevoReporte() {
    this.loadingService.cargar(() => {
    this.router.navigate(['/nuevoreporte']);
  });
    this.loadingService.notificarCargaRealCompletada();
  }

  irAMisReportes() {

    this.loadingService.cargar(() => {
    this.router.navigate(['/misreportes']);
  });
    this.loadingService.notificarCargaRealCompletada();
  }
}
