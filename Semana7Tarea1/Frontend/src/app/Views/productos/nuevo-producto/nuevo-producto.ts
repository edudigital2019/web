import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { SProductos } from '../../../Services/sproductos';
import { IProducto } from '../../../Interfaces/iproducto';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-nuevo-producto',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './nuevo-producto.html',
  styleUrl: './nuevo-producto.css'
})
export class NuevoProducto implements OnInit {
  private fb = inject(FormBuilder);
  private api = inject(SProductos);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  titulo = 'Registro de nuevo producto';
  isEdit = false;
  id = 0;

  form = this.fb.group({
    nombre:        ['', [Validators.required, Validators.minLength(2)]],
    codigo_Barras: ['', [Validators.required, Validators.minLength(3)]],
    stock:         [0,   [Validators.required, Validators.min(0)]],
    precio:        [0,   [Validators.required, Validators.min(0)]],
    descripcion:   ['']  // opcional
  });

  ngOnInit(): void {
    this.route.paramMap.subscribe(pm => {
      const idParam = pm.get('id');
      if (idParam) {
        this.isEdit = true;
        this.id = Number(idParam);
        this.titulo = 'Actualizar producto';
        this.api.getById(this.id).subscribe({
          next: (p) => this.form.patchValue(p),
          error: () => Swal.fire('Productos', 'No se pudo cargar el producto', 'error')
        });
      } else {
        this.isEdit = false;
        this.titulo = 'Registro de nuevo producto';
        this.form.reset({ stock: 0, precio: 0 });
      }
    });
  }

  guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      Swal.fire('Productos', 'Formulario inválido.', 'warning');
      return;
    }

    // Normalizar tipos numéricos y opcionales
    const raw = this.form.value;
    const dto: Omit<IProducto, 'id'> = {
      nombre:        String(raw.nombre),
      codigo_Barras: String(raw.codigo_Barras),
      stock:         Number(raw.stock),
      precio:        Number(raw.precio),
      descripcion:   raw.descripcion ? String(raw.descripcion) : null
    };

    Swal.fire({
      title: this.isEdit ? '¿Guardar cambios del producto?' : '¿Guardar nuevo producto?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Guardar',
      cancelButtonText: 'Cancelar'
    }).then(res => {
      if (!res.isConfirmed) return;

      if (this.isEdit) {
        this.api.update(this.id, dto).subscribe({
          next: () => { Swal.fire('Productos', 'Guardado con éxito', 'success'); this.router.navigateByUrl('/productos'); },
          error: () => Swal.fire('Productos', 'Error al guardar', 'error')
        });
      } else {
        this.api.create(dto).subscribe({
          next: () => { Swal.fire('Productos', 'Guardado con éxito', 'success'); this.router.navigateByUrl('/productos'); },
          error: () => Swal.fire('Productos', 'Error al guardar', 'error')
        });
      }
    });
  }
}
