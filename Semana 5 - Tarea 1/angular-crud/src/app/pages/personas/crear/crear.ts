import { Component, OnInit } from '@angular/core';
import {
  FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators
} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PersonasService } from '../../../Services/personas-servicios';

declare const Swal: any;

@Component({
  selector: 'app-crear',
  standalone: true,
  imports: [RouterLink, FormsModule, ReactiveFormsModule, CommonModule],
  templateUrl: './crear.html',
  styleUrls: ['./crear.css'],
})
export class Crear implements OnInit {

  personaform: FormGroup = new FormGroup({});
  titulo_formulario = 'Registro de nuevo cliente';
  id: number = 0;
  Editar: boolean = false;

  constructor(
    private clienteServicio: PersonasService,
    private navegacion: Router,
    private parametros: ActivatedRoute
  ) {
    this.personaform = new FormGroup({
      nombres: new FormControl('', [Validators.required, Validators.minLength(3)]),
      direccion: new FormControl('', [Validators.required, Validators.minLength(3)]),
      telefono: new FormControl('', [Validators.required, Validators.minLength(7)]),
      cedula_ruc: new FormControl('', [Validators.required, Validators.minLength(10)]),
      email: new FormControl('', [Validators.required, Validators.email]),
    });

    this.parametros.params.subscribe((p) => {
      if (p['id']) {
        this.titulo_formulario = 'Actualizar datos de cliente';
        this.id = +p['id'];
        this.Editar = true;
        this.clienteServicio.getById(this.id).subscribe(cliente => {
          // Si tu API ya envía "cedula_ruc", esto basta:
          this.personaform.patchValue(cliente);
          // Si no, descomenta este mapeo defensivo:
          // const cedula = cliente.cedula_ruc ?? cliente.cedula_RUC ?? cliente.cedulaRuc;
          // this.personaform.patchValue({ ...cliente, cedula_ruc: cedula ?? '' });
        });
      } else {
        this.personaform.reset();
      }
    });
  }

  ngOnInit() {}

  enviar() {
    if (this.personaform.invalid) {
      this.personaform.markAllAsTouched();
      return;
    }

    Swal.fire({
      title: this.Editar ? '¿Desea actualizar la información?' : '¿Desea guardar la información?',
      showDenyButton: true,
      showCancelButton: true,
      confirmButtonText: this.Editar ? 'Actualizar' : 'Guardar',
      denyButtonText: 'Cancelar',
      icon: 'question',
    }).then((result: any) => {
      if (!result.isConfirmed) return;

      if (this.Editar) {
        // IMPORTANTE: mandar el id en el body para que coincida con la URL del PUT
        const body = { ...this.personaform.value, id: this.id };

        this.clienteServicio.update(body).subscribe({
          next: () => {
            Swal.fire('Clientes', 'Se actualizó con éxito', 'success');
            this.personaform.reset();
            this.navegacion.navigate(['/personas']);
          },
          error: () => Swal.fire('Clientes', 'Error al actualizar', 'error'),
        });
      } else {
        const body = { ...this.personaform.value };
        this.clienteServicio.create(body).subscribe({
          next: () => {
            Swal.fire('Clientes', 'Se guardó con éxito', 'success');
            this.personaform.reset();
            this.navegacion.navigate(['/personas']);
          },
          error: () => Swal.fire('Clientes', 'Error al guardar', 'error'),
        });
      }
    });
  }
}
