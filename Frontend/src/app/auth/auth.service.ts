import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  login(token: string) {
  localStorage.setItem('token', token);
}

  logout() {
    localStorage.removeItem('token');
  }

  getUserFromToken(): any | null {
    const token = this.getToken();
    if (!token) return null;

    const payload = token.split('.')[1];
    try {
      return JSON.parse(atob(payload));
    } catch {
      return null;
    }
  }

  getRol(): number | null {
    const user = this.getUserFromToken();
    return user?.rol ?? null;
  }
}
