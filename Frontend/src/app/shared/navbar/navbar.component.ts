import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { AuthService } from '../../auth/auth.service';
import { ConfirmService } from '../../services/components/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  constructor(
    public auth: AuthService, 
    private router: Router, 
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}

  async logout() {
  const confirmado = await this.confirmService.confirm(
    '¿Cerrar sesión?',
    'Se cerrará tu sesión actual.',
    'Cerrar sesión',
    'question'
  );

  if (!confirmado) return;

  this.auth.logout();
  this.router.navigate(['/login']);
  this.toastr.success('Sesión cerrada correctamente', 'Hasta luego 👋');
}

  get rol(): number | null {
    return this.auth.getRol();
  }
}
