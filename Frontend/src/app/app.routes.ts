import { Routes } from '@angular/router';
import { provideRouter } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { authGuard } from './auth/auth.guard';
import { NuevoreporteComponent } from './pages/nuevoreporte/nuevoreporte.component';
import { EditarReporteComponent } from './pages/editarreporte/editarreporte.component';
import { MisReportesComponent } from './pages/misreportes/misreportes.component';
import { MenuComponent } from './pages/menu/menu.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
  { path: 'nuevoreporte', component: NuevoreporteComponent },
  { path: 'reporte/:id', component: EditarReporteComponent },
  { path: 'misreportes', component: MisReportesComponent },
  { path: 'menu', component: MenuComponent },


];

export const AppRouter = provideRouter(routes);
