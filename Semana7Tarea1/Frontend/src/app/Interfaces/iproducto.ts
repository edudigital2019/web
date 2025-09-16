export interface IProducto {
  id: number;
  nombre: string;
  codigo_Barras: string;   
  stock: number;           
  precio: number;          
  descripcion?: string | null; 
}