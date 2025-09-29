import {
  ChangeDetectorRef,
  Component,
  inject,
  signal,
} from "@angular/core";
import { Router, RouterModule } from "@angular/router";
import { SplitterModule } from 'primeng/splitter';
import { ToolbarModule } from 'primeng/toolbar';
import { PanelMenuComponent } from "./shared/ui/panel-menu/panel-menu.component";
import { CartService } from "./cart/data-access/cart.service";
import { BadgeModule } from 'primeng/badge';
import { DialogModule } from "primeng/dialog";
import { CommonModule } from "@angular/common";
import { CartComponent } from "./cart/features/cart-list/cart.component";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
  standalone: true,
  imports: [RouterModule, SplitterModule, ToolbarModule, PanelMenuComponent, BadgeModule,DialogModule,CartComponent,CommonModule],
})
export class AppComponent {
  title = "ALTEN SHOP";

  cartService = inject(CartService);
  cartDialogVisible = false;
  
  openCart() {
    this.cartDialogVisible = true;
  }

  closeCart() {
    this.cartDialogVisible = false;
  }

}
