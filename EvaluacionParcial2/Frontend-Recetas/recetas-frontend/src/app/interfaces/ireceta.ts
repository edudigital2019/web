import {Irecetaingrediente } from './irecetaingrediente';

export interface Ireceta {
  recetaId: number;
  nombre: string;
  descripcion: string;
  tiempoPreparacion: number;
  dificultad: string;
  recetaIngredientes: Irecetaingrediente[];
}
