import {Iingrediente} from './iingrediente'

export interface Irecetaingrediente {
  recetaId: number;
  ingredienteId: number;
  cantidad: number;
  unidad: string;
  calorias: number;
  ingrediente: Iingrediente;
}