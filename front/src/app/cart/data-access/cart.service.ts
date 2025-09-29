import { computed, Injectable, signal } from '@angular/core';
import { CartItem } from 'app/cart/data-access/CartItem';
import { Product } from 'app/products/data-access/product.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cartItems = signal<CartItem[]>([]);

  items = computed(() => this.cartItems());

  totalQuantity = computed(() =>
    this.cartItems().reduce((sum, item) => sum + item.quantity, 0)
  );

  totalPrice = computed(() =>
    this.cartItems().reduce((sum, item) => sum + item.product.price * item.quantity, 0)
  );


  addToCart(product: Product, quantity: number = 1) {
    this.cartItems.update(items => {
      const existing = items.find(i => i.product.id === product.id);
      if (existing) {
        // augmente la quantité si déjà dans le panier
        return items.map(i =>
          i.product.id === product.id ? { ...i, quantity: i.quantity + quantity } : i
        );
      }
      // ajoute un nouveau CartItem
      return [...items, { product, quantity }];
    });
  }

  removeFromCart(productId: number) {
    this.cartItems.update(items => items.filter(i => i.product.id !== productId));
  }

  clearCart() {
    this.cartItems.set([]);
  }
  
  updateQuantity(productId: number, quantity: number) {
    this.cartItems.update(items =>
      items
        .map(i => i.product.id === productId ? { ...i, quantity } : i)
        .filter(i => i.quantity > 0) // supprime si quantité = 0
    );
  }

}
