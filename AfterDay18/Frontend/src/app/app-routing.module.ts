import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductComponent } from './components/product/product.component';

const routes: Routes = [
  // Aşağıdaki satır ana sayfamda nerenin açılacağını gösteriyor.
  {path:"",pathMatch:"full",component:ProductComponent},
  // Bu satır http://localhost:4200/products yazıldığında nerenin açılacağını gösteriyor.
  {path:"products",component:ProductComponent},
  {path:"products/category/:categoryId",component:ProductComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
