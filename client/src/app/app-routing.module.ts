import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ContactComponent } from './contact/contact.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { AuthGuard } from './core/guard/auth.guard';
import { NavBarComponent } from './core/nav-bar/nav-bar.component';

const routes: Routes = [
  {path: "", component: HomeComponent, data:{breadcrumb: "Home"}},
  {path: "shop", loadChildren: () => import("./shop/shop.module").then(m => m.ShopModule)},
  {path: "basket", loadChildren: () => import("./basket/basket.module").then(m => m.BasketModule)},
  {
    path: "checkout", 
    canActivate:[AuthGuard],
    loadChildren: () => import("./checkout/checkout.module").then(m => m.CheckoutModule)
  },
  {path: "account", loadChildren: () => import("./account/account.module").then(m => m.AccountModule)},
  {path: "contact", component: ContactComponent},
  {path: "**", redirectTo: " ", pathMatch: "full"},
  {path: "test-error", component: TestErrorComponent},
  {path: "not-found", component: NotFoundComponent},
  {path: "server-error", component: ServerErrorComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes),
    
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
