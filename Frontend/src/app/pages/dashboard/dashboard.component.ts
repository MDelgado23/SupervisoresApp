import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="container mt-5">
      <h2>Bienvenido al panel</h2>
      <p>Estás logueado correctamente.</p>
    </div>
  `
})
export class DashboardComponent {}
