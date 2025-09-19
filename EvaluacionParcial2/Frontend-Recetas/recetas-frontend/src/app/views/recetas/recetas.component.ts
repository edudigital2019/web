import { Component, OnInit } from '@angular/core';
import type { Ireceta } from '../../interfaces/ireceta';
import { RecetaService } from '../../services/sreceta.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-recetas',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './recetas.component.html',
  styleUrls: ['./recetas.component.css']
})
export class RecetasComponent implements OnInit {
  recetas: Ireceta[] = [];

  constructor(private recetaService: RecetaService) {}

  ngOnInit(): void {
    this.cargarRecetas();
  }

  cargarRecetas(): void {
    this.recetaService.listarRecetas().subscribe({
      next: (data) => this.recetas = data,
      error: (e) => console.error('Error al cargar recetas:', e)
    });
  }

    eliminarReceta(id: number) {
    Swal.fire({
      title: '¿Eliminar receta?',
      text: 'Esta acción no se puede deshacer.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
      reverseButtons: true
    }).then(r => {
      if (!r.isConfirmed) return;

      this.recetaService.eliminarReceta(id).subscribe({
        next: () => {
          Swal.fire({
            toast: true, position: 'top-end', icon: 'success',
            title: 'Receta eliminada', showConfirmButton: false, timer: 1500
          });
          this.cargarRecetas();
        },
        error: () => {
          Swal.fire({ icon: 'error', title: 'No se pudo eliminar la receta' });
        }
      });
    });
  }
}
