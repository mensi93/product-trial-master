import { Product } from '../../products/data-access/product.model';

export interface CartItem {
  product: Product;
  quantity: number; 
}
