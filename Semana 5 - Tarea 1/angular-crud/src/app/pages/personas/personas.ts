
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { IPersonas } from '../../Interfaces/i-personas';
import { PersonasService } from '../../Services/personas-servicios';

declare const Swal: any;
 
 
@Component({
  selector: 'app-personas',
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: './personas.html',
  styleUrl: './personas.css'
})
export class Personas {
  lista_personas$!: IPersonas[];
 
  constructor(private personasServicio: PersonasService) {}
 
  ngOnInit() {
    this.cargaTabla();
  }
  cargaTabla() {
    this.personasServicio.getAll().subscribe((personas) => {
      this.lista_personas$ = personas;
    });
  }
 
  eliminarCliente(id: number) {
  Swal.fire({
    title: 'Personas',
    text: '¿Está seguro que desea eliminar este registro?',
    icon: 'question',
    showCancelButton: true,
    confirmButtonColor: '#d33',
    cancelButtonColor: '#838688ff',
    confirmButtonText: 'Eliminar',
  }).then((result: any) => {
    if (!result.isConfirmed) return;

    this.personasServicio.delete(id).subscribe({
      next: () => {
        this.cargaTabla(); // refresca la lista
        Swal.fire('Cliente Eliminado', 'Se eliminó correctamente.', 'success');
      },
      error: () => {
        Swal.fire('Error', 'No se pudo eliminar el registro.', 'error');
      }
    });
  });
}

 
  variables_sesion(id: number) {
    sessionStorage.setItem('id_cliente', id.toString());
  }
  eliminarvariable() {
    sessionStorage.removeItem('id_cliente');
  }

}