import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Users } from './pages/users/users';
import { About } from './pages/about/about';
import { Personas } from './pages/personas/personas';
import { Crear as CrearPersona } from './pages/personas/crear/crear';

export const routes: Routes = [
    {path:'',component:Home},
    {path:'personas',component:Personas},
    {path:'usuarios',component:Users},
    {path:'nosotros',component:About},
    {path:'personas/crear',component:CrearPersona},
    {path:'personas/editar/:id',component:CrearPersona},
    {path:'**',redirectTo:''}
];
