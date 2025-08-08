import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../auth.service'
import { LoadingService } from '../../services/components/loading.service';
import { SpinnerComponent } from '../../shared/spinner/spinner.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, SpinnerComponent],
  templateUrl: './login.component.html',
})
export class LoginComponent {
  dni: string = '';
  password: string = '';

  constructor(
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService,
    private auth: AuthService,
    public loadingService: LoadingService,
  ) {}

  ngOnInit(): void {
  this.loadingService.reset();
}
  
  login() {
    this.http.post<{ token: string }>('http://localhost:5008/api/auth/login', {
      dni: this.dni,
      password: this.password
    }).subscribe({
      next: (res) => {
        this.auth.login(res.token);
        
        this.loadingService.notificarCargaRealCompletada(); // ✅ apaga el spinner
        this.toastr.success('Bienvenido');
        
      },
      error: () => {
        this.loadingService.notificarCargaRealCompletada(); // ✅ apaga también si falla
        this.toastr.error('DNI o contraseña incorrectos', 'Error de inicio de sesión');
        
      }
    });
  }

  loginConSpinner() {
  this.loadingService.cargar(() => {this.router.navigate(['/menu']);}); // ✅ activa el spinner
  this.login();                         // ✅ desactiva cuando termine (con notificarCargaRealCompletada)
}


}



