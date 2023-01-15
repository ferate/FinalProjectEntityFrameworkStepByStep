import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product';
import { ProductResponseModel } from 'src/app/models/productResponseModel';
import { ProductService } from 'src/app/services/product.service';


@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit{

  product1: any = { productId: 1, productName: 'Bardak', categoryId: 1, unitPrice: 5 }
  product2: any = { productId: 2, productName: 'Laptop', categoryId: 1, unitPrice: 5 }
  product3: any = { productId: 3, productName: 'Mouse', categoryId: 1, unitPrice: 5 }
  product4: any = { productId: 4, productName: 'Keyboard', categoryId: 1, unitPrice: 5 }
  product5: any = { productId: 5, productName: 'Kamera', categoryId: 1, unitPrice: 5 }

 // products:Product[] = [this.product1,this.product2,this.product3,this.product4,this.product5]
 // Yukarıdaki kısmı iptal ettik gerçek veriler ile çalışacağız
 products:Product[] = [];
 dataLoaded = false;

 constructor(private productService:ProductService) {}

 ngOnInit():void{
  this.getProducts();
 }

 getProducts(){
  // Kod Refactor edildi
  /* this.httpClient.get<ProductResponseModel>(this.apiURL)
  .subscribe((response)=>{
    //this.productResponseModel = response
    
    // Bu kısmı ayrı ayrı her yerde yazmak istemiyorum. Onun için bu kısmıda iyileştirelim
    this.products = response.data
  }); */

  this.productService.getProducts().subscribe(response=>{
    this.products=response.data
    this.dataLoaded = true;
  })
  
 }

}
