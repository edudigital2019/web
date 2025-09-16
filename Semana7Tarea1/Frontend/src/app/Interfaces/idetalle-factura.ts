import { IProducto } from "./iproducto";


export interface IDetalleFactura {
  id: number;
  productosModelId: number
  producto?: IProducto;
  nombre?: string | null;
  precio: number;       
  cantidad: number;    
  monto: number;      
  facturasModelId: number;
}
