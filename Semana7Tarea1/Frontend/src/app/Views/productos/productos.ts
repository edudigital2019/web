import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { SProductos } from '../../Services/sproductos';
import { IProducto } from '../../Interfaces/iproducto';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './productos.html',
  styleUrl: './productos.css'
})
export class Productos implements OnInit {
  private api = inject(SProductos);

  productos = signal<IProducto[]>([]);
  loading   = signal<boolean>(false);
  error     = signal<string | null>(null);

  ngOnInit(): void { this.cargar(); }

  cargar(): void {
    this.loading.set(true);
    this.error.set(null);
    this.api.getAll().subscribe({
      next: r => { this.productos.set(r); this.loading.set(false); },
      error: e => { this.error.set(e?.message ?? 'Error'); this.loading.set(false); }
    });
  }

eliminar(id: number): void {
    Swal.fire({
      title: '¿Eliminar este producto?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then(res => {
      if (!res.isConfirmed) return;
      this.loading.set(true);
      this.api.remove(id).subscribe({
        next: () => { Swal.fire('Productos', 'Eliminado correctamente', 'success'); this.cargar(); },
        error: (e) => { this.loading.set(false); Swal.fire('Productos', e?.message ?? 'Error eliminando', 'error'); }
      });
    });
  }
}
