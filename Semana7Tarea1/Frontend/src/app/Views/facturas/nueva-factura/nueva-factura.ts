import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import Swal from 'sweetalert2';

import { SClientes } from '../../../Services/sclientes';
import { SProductos } from '../../../Services/sproductos';
import { SFacturas } from '../../../Services/sfacturas';

import { ICliente } from '../../../Interfaces/icliente';
import { IProducto } from '../../../Interfaces/iproducto';
import { IFactura } from '../../../Interfaces/ifactura';

@Component({
  selector: 'app-nueva-factura',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './nueva-factura.html',
  styleUrl: './nueva-factura.css'
})
export class NuevaFactura implements OnInit {
  private fb = inject(FormBuilder);
  private apiClientes = inject(SClientes);
  private apiProductos = inject(SProductos);
  private apiFacturas = inject(SFacturas);
  private router = inject(Router);
  private route  = inject(ActivatedRoute);

  titulo = signal('Nueva factura');
  isEdit = signal(false);
  id = signal<number | null>(null);

  clientes = signal<ICliente[]>([]);
  productos = signal<IProducto[]>([]);

  form = this.fb.group({
    clientesModelId: [null as number | null, [Validators.required]],
    codigo_Venta:    ['', [Validators.required]],
    estado_Venta:    ['Pendiente', [Validators.required]],
    metodo_Pago:     ['Efectivo', [Validators.required]],
   
    fechaVenta:      [new Date().toISOString().substring(0,10), [Validators.required]],
    notas:           [''],
    descuento:       [0, [Validators.min(0)]],
    sub_Total_Venta: [0],
    total_Venta:     [0],
    detalleFactura:  this.fb.array([])
  });

  get detalleFactura(): FormArray { return this.form.get('detalleFactura') as FormArray; }

  ngOnInit(): void {

    this.apiClientes.getAll().subscribe(res => this.clientes.set(res));
    this.apiProductos.getAll().subscribe(res => this.productos.set(res));

    this.route.paramMap.subscribe(pm => {
      const idParam = pm.get('id');
      if (idParam) {
        this.isEdit.set(true);
        this.id.set(Number(idParam));
        this.titulo.set('Editar factura');
        this.cargarFactura(this.id()!);
      } else {
        this.isEdit.set(false);
        this.titulo.set('Nueva factura');
        if (!this.detalleFactura.length) this.agregarLinea();
      }
    });


    this.form.valueChanges.subscribe(() => this.recalcularTotales());
  }

  private cargarFactura(id: number): void {
    this.apiFacturas.getById(id).subscribe({
      next: (f: IFactura | any) => {

        const cab = {
          clientesModelId: f.clientesModelId ?? f.ClientesModelId ?? null,
          codigo_Venta:    f.codigo_Venta    ?? f.Codigo_Venta    ?? '',
          estado_Venta:    f.estado_Venta    ?? f.Estado_Venta    ?? 'Pendiente',
          metodo_Pago:     f.metodo_Pago     ?? f.Metodo_Pago     ?? 'Efectivo',
          fechaVenta:      (f.fechaVenta ?? f.FechaVenta ?? '').toString().substring(0,10),
          notas:           f.notas ?? f.Notas ?? '',
          descuento:       f.descuento ?? f.Descuento ?? 0
        };
        this.form.patchValue(cab);

  
        const items: any[] = f.detalleFactura ?? f.DetalleFactura ?? [];
        this.detalleFactura.clear();
        items.forEach(d => {
          this.detalleFactura.push(this.fb.group({
            productosModelId: [d.productosModelId ?? d.ProductosModelId, Validators.required],
            nombre:           [d.nombre ?? d.Nombre ?? null],
            precio:           [d.precio ?? d.Precio ?? 0, [Validators.required, Validators.min(0)]],
            cantidad:         [d.cantidad ?? d.Cantidad ?? 1, [Validators.required, Validators.min(1)]],
            monto:            [d.monto ?? d.Monto ?? 0, [Validators.required, Validators.min(0)]]
          }));
        });

        this.recalcularTotales();
      },
      error: () => Swal.fire('Facturas', 'No se pudo cargar la factura', 'error')
    });
  }

  private lineaDetalleInicial() {
    return this.fb.group({
      productosModelId: [null, Validators.required],
      nombre:           [null as string | null],
      precio:           [0, [Validators.required, Validators.min(0)]],
      cantidad:         [1, [Validators.required, Validators.min(1)]],
      monto:            [0, [Validators.required, Validators.min(0)]]
    });
  }

  agregarLinea(): void {
    this.detalleFactura.push(this.lineaDetalleInicial());
  }

  eliminarLinea(idx: number): void {
    this.detalleFactura.removeAt(idx);
    this.recalcularTotales();
  }

  onProductoChange(idx: number): void {
    const linea = this.detalleFactura.at(idx);
    const prodId = linea.get('productosModelId')!.value as number | null;
    const prod = this.productos().find(p => p.id === prodId);
    if (prod) {
      linea.patchValue({
        nombre: prod.nombre,
        precio: prod.precio
      }, { emitEvent: false });
      this.recalcLinea(idx);
    }
  }

  recalcLinea(idx: number): void {
    const linea = this.detalleFactura.at(idx);
    const cantidad = Number(linea.get('cantidad')!.value) || 0;
    const precio   = Number(linea.get('precio')!.value)   || 0;
    const monto = +(cantidad * precio).toFixed(2);
    linea.get('monto')!.setValue(monto, { emitEvent: false });
    this.recalcularTotales();
  }

  private recalcularTotales(): void {
    const sub = this.detalleFactura.controls
      .reduce((acc, g) => acc + (Number(g.get('monto')!.value) || 0), 0);
    const descuento = Number(this.form.get('descuento')!.value) || 0;
    const total = Math.max(0, +(sub - descuento).toFixed(2));
    this.form.patchValue({
      sub_Total_Venta: +sub.toFixed(2),
      total_Venta: total
    }, { emitEvent: false });
  }

 private toApiPayload() {
  const raw = this.form.value as any;

  const fechaISO =
    raw.fechaVenta && typeof raw.fechaVenta === 'string'
      ? new Date(raw.fechaVenta + 'T00:00:00').toISOString()
      : new Date().toISOString();


  const detalle = (raw.detalleFactura || []).map((d: any) => ({
  
    productosModelId: Number(d.productosModelId),
    nombre: d.nombre ?? null,
    precio: Number(d.precio),
    cantidad: Number(d.cantidad),
    monto: Number(d.monto)

  }));

  return {
    id: this.isEdit() ? this.id()! : 0,
    fechaVenta: fechaISO,
    codigo_Venta: String(raw.codigo_Venta),
    notas: raw.notas ? String(raw.notas) : '',
    sub_Total_Venta: Number(raw.sub_Total_Venta) || 0,
    estado_Venta: String(raw.estado_Venta),
    descuento: Number(raw.descuento) || 0,
    total_Venta: Number(raw.total_Venta) || 0,
    metodo_Pago: String(raw.metodo_Pago),
    clientesModelId: Number(raw.clientesModelId),
    detalleFactura: detalle
  };
}

 guardar(): void {
  if (this.form.invalid || !this.detalleFactura.length) {
    this.form.markAllAsTouched();
    Swal.fire('Facturas', 'Formulario inválido o sin detalles.', 'warning');
    return;
  }

    const payload = this.toApiPayload();
    console.log('Payload →', payload);

    Swal.fire({
      title: this.isEdit() ? '¿Guardar cambios de la factura?' : '¿Guardar nueva factura?',
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Guardar',
      cancelButtonText: 'Cancelar'
    }).then(res => {
      if (!res.isConfirmed) return;

      if (this.isEdit()) {
        this.apiFacturas.update(this.id()!, payload as any).subscribe({
          next: () => { Swal.fire('Facturas', 'Guardado con éxito', 'success'); this.router.navigateByUrl('/facturas'); },
          error: () => Swal.fire('Facturas', 'Error al guardar', 'error')
        });
      } else {
        this.apiFacturas.create(payload as any).subscribe({
          next: () => { Swal.fire('Facturas', 'Guardado con éxito', 'success'); this.router.navigateByUrl('/facturas'); },
          error: () => Swal.fire('Facturas', 'Error al guardar', 'error')
        });
      }
    });
  }
}
