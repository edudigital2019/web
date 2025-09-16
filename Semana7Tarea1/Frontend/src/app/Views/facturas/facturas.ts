import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import Swal from 'sweetalert2';
import { IFactura } from '../../Interfaces/ifactura';
import { SFacturas } from '../../Services/sfacturas';

@Component({
  selector: 'app-facturas',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './facturas.html',
  styleUrl: './facturas.css'
})
export class Facturas implements OnInit {
  private api = inject(SFacturas);

  lista_facturas = signal<IFactura[]>([]);
  loading = signal(false);

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.loading.set(true);

    this.api.getAll().subscribe({
      next: res => { this.lista_facturas.set(res); this.loading.set(false); },
      error: () => { this.loading.set(false); Swal.fire('Facturas', 'No se pudo cargar la lista', 'error'); }
    });
  }

  eliminar(id: number): void {
    Swal.fire({
      title: '¿Eliminar esta factura?',
      text: 'Esta acción no se puede deshacer',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then(r => {
      if (!r.isConfirmed) return;
      this.loading.set(true);
      this.api.remove(id).subscribe({
        next: () => { this.cargar(); Swal.fire('Facturas', 'Eliminada correctamente', 'success'); },
        error: () => { this.loading.set(false); Swal.fire('Facturas', 'Error al eliminar', 'error'); }
      });
    });
  }

  imprimir(): void {
    const html = document.querySelector('.factura-container')?.innerHTML ?? '';
    const win = window.open('', '', 'height=600,width=900');
    if (!win) return;
    win.document.open();
    win.document.write(`<!doctype html>
      <html><head><meta charset="utf-8"><title>Facturas</title>
      <style>
        @media print{ button{display:none;} }
        @page{ size: A4 portrait; margin: 12mm; }
        table{ width:100%; border-collapse:collapse; }
        th,td{ border-bottom:1px solid #e5e7eb; padding:.4rem; text-align:left; }
      </style></head>
      <body onload="window.print(); window.close();">${html}</body></html>`);
    win.document.close();
  }
}
