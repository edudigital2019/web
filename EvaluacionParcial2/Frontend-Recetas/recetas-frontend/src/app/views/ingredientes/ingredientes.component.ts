import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import type { Iingrediente } from '../../interfaces/iingrediente';
import { IngredienteService } from '../../services/singrediente.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-ingredientes',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './ingredientes.component.html',
  styleUrls: ['./ingredientes.component.css']
})
export class IngredientesComponent implements OnInit {
  private ingSvc = inject(IngredienteService);

  ingredientes: Iingrediente[] = [];
  cargando = false;
  error?: string;

  ngOnInit(): void {
    this.cargar();
  }

  cargar(): void {
    this.cargando = true;
    this.error = undefined;

    this.ingSvc.listarIngredientes().subscribe({
      next: (data) => {
        this.ingredientes = data ?? [];
        this.cargando = false;
      },
      error: (e) => {
        console.error('GET /ingredientes falló:', e);
        this.error = e?.message || 'No se pudieron cargar los ingredientes.';
        this.cargando = false;
      }
    });
  }

  nuevo(): void {}

  editar(id: number): void {}

    eliminar(id: number) {
    Swal.fire({
      title: '¿Eliminar ingrediente?',
      text: 'Se eliminará definitivamente.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar',
      reverseButtons: true
    }).then(r => {
      if (!r.isConfirmed) return;

      this.ingSvc.eliminarIngrediente(id).subscribe({
        next: () => {
          Swal.fire({
            toast: true, position: 'top-end', icon: 'success',
            title: 'Ingrediente eliminado', showConfirmButton: false, timer: 1500
          });
          this.cargar();
        },
        error: () => Swal.fire({ icon: 'error', title: 'No se pudo eliminar el ingrediente' })
      });
    });
  }
}
