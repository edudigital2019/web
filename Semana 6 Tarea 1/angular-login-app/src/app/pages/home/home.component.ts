import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginServicios } from '../../core/services/login-servicios';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  private readonly servicioLogin = inject(LoginServicios);

  get usuario() {
    return this.servicioLogin.obtenerUsuarioAutenticado();
  }
}
