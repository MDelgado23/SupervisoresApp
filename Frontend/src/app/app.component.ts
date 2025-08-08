import { Component } from '@angular/core';
import { RouterOutlet, Router } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  constructor(public router: Router) {}

  isLoginPage(): boolean {
    return this.router.url === '/login';
  }
}
