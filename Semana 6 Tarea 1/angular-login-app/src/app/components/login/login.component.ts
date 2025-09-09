import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginServicios } from '../../core/services/login-servicios';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  private readonly creadorFormularios = inject(FormBuilder);
  private readonly servicioLogin = inject(LoginServicios);
  private readonly rutas = inject(Router);

  mensajeError = '';
  mostrandoContrasenia = false;

  formularioLogin = this.creadorFormularios.group({
    email: ['', [Validators.required, Validators.email]],
    contrasenia: ['', [Validators.required, Validators.minLength(4)]],
  });

  alternarVisibilidadContrasenia(): void {
    this.mostrandoContrasenia = !this.mostrandoContrasenia;
  }

  enviarLogin(): void {
    if (this.formularioLogin.invalid) {
      this.mensajeError = 'Completa los campos correctamente';
      return;
    }

    const credenciales = this.formularioLogin.getRawValue();
    this.servicioLogin.iniciarSesion({
      email: credenciales.email!,
      contrasenia: credenciales.contrasenia!,
    }).subscribe({
      next: () => this.rutas.navigate(['/']),
      error: () => this.mensajeError = 'Correo o contraseña inválidos',
    });
  }
}
