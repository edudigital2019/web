import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SClientes } from '../../Services/sclientes';
import Swal from 'sweetalert2'; 
import { ICliente } from '../../Interfaces/icliente';
import { RouterLink, RouterLinkActive } from '@angular/router'; // 


@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './clientes.html',
  styleUrl: './clientes.css'
})
export class Clientes implements OnInit {
  private api = inject(SClientes);

  clientes = signal<ICliente[]>([]);
  loading  = signal<boolean>(false);
  error    = signal<string | null>(null);

  ngOnInit(): void { this.cargar(); }

  cargar(): void {
    this.loading.set(true);
    this.error.set(null);
    this.api.getAll().subscribe({
      next: (res) => { this.clientes.set(res); this.loading.set(false); },
      error: (err) => { this.error.set(err?.message ?? 'Error'); this.loading.set(false); }
    });
  }
 eliminar(id: number): void {
    Swal.fire({
      title: '¿Eliminar este cliente?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then(r => {
      if (!r.isConfirmed) return;
      this.loading.set(true);
      this.api.remove(id).subscribe({
        next: () => { Swal.fire('Clientes', 'Eliminado correctamente', 'success'); this.cargar(); },
        error: (err) => { this.loading.set(false); Swal.fire('Clientes', err?.message ?? 'Error eliminando', 'error'); }
      });
    });
  }
}