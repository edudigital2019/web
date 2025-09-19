import { Routes } from '@angular/router';
import { RecetasComponent } from './views/recetas/recetas.component';
import { NuevaRecetaComponent } from './views/recetas/nueva-receta/nueva-receta.component';
import { IngredientesComponent } from './views/ingredientes/ingredientes.component';
import { NuevoIngredienteComponent } from './views/ingredientes/nuevo-ingrediente/nuevo-ingrediente.component';

export const routes: Routes = [
  { path: '', redirectTo: 'recetas', pathMatch: 'full' },
  { path: 'recetas', component: RecetasComponent },
  { path: 'recetas/nueva', component: NuevaRecetaComponent },
  { path: 'recetas/:id/editar', component: NuevaRecetaComponent },
  { path: 'ingredientes', component: IngredientesComponent },
  { path: 'ingredientes/nuevo', component: NuevoIngredienteComponent },
  { path: 'ingredientes/:id/editar', component: NuevoIngredienteComponent },
  { path: '**', redirectTo: 'recetas' }
];
