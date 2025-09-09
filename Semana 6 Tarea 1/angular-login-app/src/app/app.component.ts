import { Component, inject } from '@angular/core';
import { RouterOutlet, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { LoginServicios } from './core/services/login-servicios';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  private readonly servicioLogin = inject(LoginServicios);
  private readonly rutas = inject(Router);

  obtenerNombreUsuario(): string {
    const u = this.servicioLogin.obtenerUsuarioAutenticado();
    return u?.nombreCompleto ?? '';
  }

  salir(): void {
    this.servicioLogin.cerrarSesion();
    this.rutas.navigate(['/login']);
  }
}
