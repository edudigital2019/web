import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { SClientes } from '../../../Services/sclientes';
import { ICliente } from '../../../Interfaces/icliente';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-nuevo-cliente',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './nuevo-cliente.html',
  styleUrl: './nuevo-cliente.css'
})
export class NuevoCliente implements OnInit {
  private fb = inject(FormBuilder);
  private api = inject(SClientes);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  titulo = 'Registro de nuevo cliente';
  isEdit = false;
  id = 0;

  form = this.fb.group({
    nombres:   ['', [Validators.required, Validators.minLength(3)]],
    email:     ['', [Validators.required, Validators.email]],
    telefono:  ['', [Validators.required, Validators.minLength(7)]],
    direccion: ['', [Validators.required, Validators.minLength(3)]],
    cedula_ruc:['', [Validators.required, Validators.minLength(10)]],
  });

  ngOnInit(): void {
    this.route.paramMap.subscribe(pm => {
      const idParam = pm.get('id');
      if (idParam) {
        this.isEdit = true;
        this.id = Number(idParam);
        this.titulo = 'Actualizar datos de cliente';
        this.cargarCliente(this.id);
      } else {
        this.isEdit = false;
        this.titulo = 'Registro de nuevo cliente';
        this.form.reset();
      }
    });
  }

  private cargarCliente(id: number): void {
    this.api.getById(id).subscribe({
      next: (cli: ICliente) => this.form.patchValue(cli),
      error: () => Swal.fire('Clientes', 'No se pudo cargar el cliente', 'error')
    });
  }

  guardarCliente(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      Swal.fire('Clientes', 'Formulario inválido. Revisa los campos.', 'warning');
      return;
    }

    Swal.fire({
      title: this.isEdit ? '¿Guardar cambios del cliente?' : '¿Guardar nuevo cliente?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: 'Guardar',
      denyButtonText: `Cancelar`,
      icon: 'question',
    }).then(result => {
      if (!result.isConfirmed) {
        if (result.isDenied) Swal.fire('Clientes', 'Operación cancelada', 'info');
        return;
      }

      const dto = this.form.value as Omit<ICliente, 'id'>;

      if (this.isEdit) {
        this.api.update(this.id, dto).subscribe({
          next: () => {
            Swal.fire('Clientes', 'Se guardó con éxito', 'success');
            this.router.navigateByUrl('/clientes');
          },
          error: () => Swal.fire('Clientes', 'Error al guardar', 'error')
        });
      } else {
        this.api.create(dto).subscribe({
          next: () => {
            Swal.fire('Clientes', 'Se guardó con éxito', 'success');
            this.router.navigateByUrl('/clientes');
          },
          error: () => Swal.fire('Clientes', 'Error al guardar', 'error')
        });
      }
    });
  }
}
