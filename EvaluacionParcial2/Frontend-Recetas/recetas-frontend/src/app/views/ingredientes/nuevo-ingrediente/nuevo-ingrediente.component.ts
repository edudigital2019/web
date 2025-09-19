import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { IngredienteService } from '../../../services/singrediente.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-nuevo-ingrediente',
  standalone: true,                                
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './nuevo-ingrediente.component.html',
  styleUrls: ['./nuevo-ingrediente.component.css']
})
export class NuevoIngredienteComponent implements OnInit {
  titulo = 'Nuevo Ingrediente';
  id: number | null = null;

  form!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private ingSvc: IngredienteService
  ) {
    this.form = this.fb.group({
      nombre: ['', [Validators.required, Validators.maxLength(100)]]
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    this.id = idParam ? +idParam : null;

    if (this.id) {
      this.titulo = 'Editar Ingrediente';
      this.ingSvc.obtenerIngrediente(this.id).subscribe({
        next: ing => this.form.patchValue({ nombre: ing.nombre }),
        error: () => alert('No se pudo cargar el ingrediente.')
      });
    }
  }

  guardar(): void {
  if (this.form.invalid) { this.form.markAllAsTouched(); return; }
  const payload = { ingredienteId: this.id ?? 0, nombre: this.form.value.nombre!.trim() };

  if (this.id) {
    this.ingSvc.actualizarIngrediente(this.id, payload as any).subscribe({
      next: () => {
        Swal.fire({ toast: true, position: 'top-end', icon: 'success', title: 'Ingrediente actualizado', showConfirmButton: false, timer: 1500 });
        this.router.navigate(['/ingredientes']);
      },
      error: () => Swal.fire({ icon: 'error', title: 'No se pudo actualizar el ingrediente' })
    });
  } else {
    this.ingSvc.crearIngrediente(payload as any).subscribe({
      next: () => {
        Swal.fire({ toast: true, position: 'top-end', icon: 'success', title: 'Ingrediente creado', showConfirmButton: false, timer: 1500 });
        this.router.navigate(['/ingredientes']);
      },
      error: () => Swal.fire({ icon: 'error', title: 'No se pudo crear el ingrediente' })
    });
  }
}

  cancelar(): void { this.router.navigate(['/ingredientes']); }
}
