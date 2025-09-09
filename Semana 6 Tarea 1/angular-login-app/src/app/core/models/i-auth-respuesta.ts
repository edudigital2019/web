export interface IAuthRespuesta {
  id: number;
  email: string;
  nombreCompleto: string;
  activo: boolean;
  token?: string;
}
