import { Component, TemplateRef, ViewChild } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { BasketItem } from 'src/app/shared/models/basket';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl:"./nav-bar.component.html",
  styleUrls: ["nav-bar.component.scss"]
})
export class NavBarComponent {
  modalRef?: BsModalRef;

  @ViewChild('registerTemplate') registerTemplate!: TemplateRef<any>;
  @ViewChild('loginTemplate') loginTemplate!: TemplateRef<any>;

  constructor(public basketService: BasketService, private modalService: BsModalService, public accountService: AccountService){}

  getCount(items: BasketItem []){
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }


  closeModal(){
    this.modalRef?.hide();
  }

  openLoginModal() {
    if(this.modalRef) this.closeModal();
    this.modalRef = this.modalService.show(this.loginTemplate);
  }

  openSignUpModal() {
    if(this.modalRef) this.closeModal();
    this.modalRef = this.modalService.show(this.registerTemplate);
  }

}
