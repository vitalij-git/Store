import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl:"./product-details.component.html",
  styleUrls: ["./product-details.component.scss" ]
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;
  quantity: number = 1;
  quantityInBasket: number = 0;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService,private basketService: BasketService){
    this.bcService.set("@productDetails", " ")
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    const id = this.activatedRoute.snapshot.paramMap.get("id");
    if(id) this.shopService.geProduct(+id).subscribe({
      next: product => {
        this.product = product;
        this.bcService.set("@productDetails", product.name); 
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
            const item = basket?.basketItems.find(x => x.id === +id);
            if(item){
              this.quantity = item.quantity;
              this.quantityInBasket = item.quantity;
              console.log(this.quantity, this.quantityInBasket);
            }
          }
        })
      },
      error: error => console.log(error)
    });
  }
  addItemToBasket(){
    if(this.product)  {
      if(this.quantity > this.quantityInBasket){
        const quantityToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += quantityToAdd;
        this.basketService.addItemToBasket(this.product, this.quantity);
      }
      else{
        const quantityToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= quantityToRemove;
        this.basketService.removeItemFromBasket(this.product.id, quantityToRemove);
      }
    }
  }

  incrementQuantiti(){
    this.quantity += 1;
  }
  decrementQuantiti(){
    this.quantity -=1;
  }

  get buttonText(){
    return this.quantityInBasket === 0 ? "Add to basket" : "Update basket";
  }
}
