import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { CartService } from 'app/cart/data-access/cart.service';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, CardModule,ButtonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartService = inject(CartService);

  increase(productId: number) {
    const item = this.cartService.items().find(i => i.product.id === productId);
    if (item) {
      this.cartService.updateQuantity(productId, item.quantity + 1);
    }
  }

  decrease(productId: number) {
    const item = this.cartService.items().find(i => i.product.id === productId);
    if (item) {
      const newQty = item.quantity - 1;
      if (newQty > 0) {
        this.cartService.updateQuantity(productId, newQty);
      } else {
        this.cartService.removeFromCart(productId);
      }
    }
  }
}
