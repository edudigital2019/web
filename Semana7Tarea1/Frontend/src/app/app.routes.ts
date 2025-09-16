import { Routes } from '@angular/router';
import { Facturas } from './Views/facturas/facturas';

export const routes: Routes = [
    {
    path: '',
    component: Facturas,
    pathMatch: 'full'
  },
    {
    path: 'clientes',
    loadComponent: () => import('./Views/clientes/clientes').then(m => m.Clientes)
  },
  {
    path: 'clientes/nuevo',
    loadComponent: () => import('./Views/clientes/nuevo-cliente/nuevo-cliente').then(m => m.NuevoCliente)
  },
  {
    path: 'clientes/editar/:id',
    loadComponent: () => import('./Views/clientes/nuevo-cliente/nuevo-cliente').then(m => m.NuevoCliente)
  },
  {
    path: 'productos',
    loadComponent: () => import('./Views/productos/productos').then(m => m.Productos)
  },
  {
    path: 'productos/nuevo',
    loadComponent: () => import('./Views/productos/nuevo-producto/nuevo-producto').then(m => m.NuevoProducto)
  },
  {
    path: 'productos/editar/:id',
    loadComponent: () => import('./Views/productos/nuevo-producto/nuevo-producto').then(m => m.NuevoProducto)
  },
   { path: 'facturas', component: Facturas },
   {
    path: 'facturas/nueva',
    loadComponent: () => import('./Views/facturas/nueva-factura/nueva-factura')
      .then(m => m.NuevaFactura)
  },
  {
    path: 'facturas/editar/:id',
    loadComponent: () => import('./Views/facturas/nueva-factura/nueva-factura')
      .then(m => m.NuevaFactura)
  },
  
];
