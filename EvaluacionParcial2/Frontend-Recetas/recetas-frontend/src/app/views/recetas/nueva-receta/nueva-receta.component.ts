import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { RecetaService } from '../../../services/sreceta.service';
import { IngredienteService } from '../../../services/singrediente.service';
import type { Ireceta } from '../../../interfaces/ireceta';
import type { Iingrediente } from '../../../interfaces/iingrediente';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-nueva-receta',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './nueva-receta.component.html',
  styleUrls: ['./nueva-receta.component.css']
})
export class NuevaRecetaComponent implements OnInit {

  form!: FormGroup;
  modoEdicion = false;
  recetaId: number | null = null;

  ingredientes: Iingrediente[] = [];
  mostrarModalNuevoIngrediente = false;
  formNuevoIngrediente!: FormGroup;

  private filaParaNuevoIng: number | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private recetaSvc: RecetaService,
    private ingSvc: IngredienteService
  ) {}

  ngOnInit(): void {
    const idStr = this.route.snapshot.paramMap.get('id');
    this.modoEdicion = !!idStr;
    this.recetaId = idStr ? +idStr : null;

    this.inicializarFormularios();
    this.cargarIngredientes();

    if (this.modoEdicion && this.recetaId != null) {
      this.cargarReceta(this.recetaId);
    } else {
      this.agregarRecetaIngrediente();
    }
  }

  inicializarFormularios(): void {
    this.form = this.fb.group({
  nombre: ['', Validators.required],
  descripcion: ['', Validators.required],
  tiempoPreparacion: [1, [Validators.required, Validators.min(1)]],
  dificultad: ['Media', Validators.required],
  recetaIngredientes: this.fb.array([this.grupoRecetaIngrediente()], { validators: Validators.minLength(1) })
});

    this.formNuevoIngrediente = this.fb.group({
      nombre: ['', Validators.required]
    });
  }

  get recetaIngredientes(): FormArray {
    return this.form.get('recetaIngredientes') as FormArray;
  }

  private grupoRecetaIngrediente(): FormGroup {
    return this.fb.group({
      ingredienteId: [null, Validators.required],
      cantidad: [0, [Validators.required, Validators.min(0.01)]],
      unidad: ['', Validators.required],
      calorias: [0, [Validators.required, Validators.min(0)]]
    });
  }

  agregarRecetaIngrediente(): void {
    this.recetaIngredientes.push(this.grupoRecetaIngrediente());
  }

  eliminarRecetaIngrediente(i: number): void {
    this.recetaIngredientes.removeAt(i);
  }

  cargarIngredientes(): void {
    this.ingSvc.listarIngredientes().subscribe({
      next: (data) => (this.ingredientes = data),
      error: (e) => console.error('Error cargando ingredientes:', e)
    });
  }

  cargarReceta(id: number): void {
    this.recetaSvc.obtenerReceta(id).subscribe({
      next: (r: Ireceta) => {

        while (this.recetaIngredientes.length) this.recetaIngredientes.removeAt(0);
        (r.recetaIngredientes ?? []).forEach(ri => {
          this.recetaIngredientes.push(this.fb.group({
            ingredienteId: [ri.ingredienteId, Validators.required],
            cantidad: [ri.cantidad, [Validators.required, Validators.min(0.01)]],
            unidad: [ri.unidad, Validators.required],
            calorias: [ri.calorias, [Validators.required, Validators.min(0)]]
          }));
        });

        this.form.patchValue({
          nombre: r.nombre,
          descripcion: r.descripcion,
          tiempoPreparacion: r.tiempoPreparacion,
          dificultad: r.dificultad
        });
      },
      error: (e) => console.error('Error cargando receta:', e)
    });
  }

indiceFilaParaNuevoIng: number | null = null;

abrirModalNuevoIngrediente(index?: number): void {

  if (typeof index === 'number') {
    this.indiceFilaParaNuevoIng = index;
  } else {
    if (!this.recetaIngredientes.length) this.agregarRecetaIngrediente();
    this.indiceFilaParaNuevoIng = this.recetaIngredientes.length - 1;
  }
  this.formNuevoIngrediente.reset();
  this.mostrarModalNuevoIngrediente = true;
}

  crearNuevoIngrediente(): void {
  if (this.formNuevoIngrediente.invalid) return;
  const payload = { nombre: this.formNuevoIngrediente.value.nombre };

  this.ingSvc.crearIngrediente(payload as any).subscribe({
    next: (nuevo) => {
      this.ingredientes = [...this.ingredientes, nuevo];
      if (this.indiceFilaParaNuevoIng != null) {
        this.recetaIngredientes.at(this.indiceFilaParaNuevoIng).get('ingredienteId')?.setValue(nuevo.ingredienteId);
      }
      this.mostrarModalNuevoIngrediente = false;
      this.indiceFilaParaNuevoIng = null;
    },
    error: (e) => {
      const detalle = e?.error?.title || e?.error?.message || e?.message || '';
      alert(`No se pudo crear el ingrediente.\n${e.status} ${e.statusText || ''}\n${detalle}`);
      console.error('POST ingredientes:', e);
    }
  });
}

  
    guardar(): void {
      
  const receta = this.form.value as Ireceta;
    if (this.modoEdicion && this.recetaId != null) {
      this.recetaSvc.actualizarReceta(this.recetaId, receta).subscribe({
        next: () => {
          Swal.fire({
            toast: true, position: 'top-end', icon: 'success',
            title: 'Receta actualizada', showConfirmButton: false, timer: 1500
          });
          this.router.navigate(['/recetas']);
        },
        error: () => Swal.fire({ icon: 'error', title: 'No se pudo actualizar la receta' })
      });
    } else {
      this.recetaSvc.crearReceta(receta).subscribe({
        next: () => {
          Swal.fire({
            toast: true, position: 'top-end', icon: 'success',
            title: 'Receta creada', showConfirmButton: false, timer: 1500
          });
          this.router.navigate(['/recetas']);
        },
        error: () => Swal.fire({ icon: 'error', title: 'No se pudo crear la receta' })
      });
    }
  }

}
