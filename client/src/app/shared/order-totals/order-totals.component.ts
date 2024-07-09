import { Component } from '@angular/core';
import { BasketRoutingModule } from 'src/app/basket/basket-routing.module';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-totals',
  templateUrl:"order-totals.component.html",
  styleUrls: ["order-totals.component.scss"]
})
export class OrderTotalsComponent {

  constructor(public basketService: BasketService){}
}
