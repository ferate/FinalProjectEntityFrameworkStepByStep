import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { from, Observable} from 'rxjs';
import { Product } from '../models/product';
import { ListResponseModel } from '../models/listResponseModel';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  apiURL = "https://localhost:7184/api/";
            

  constructor(private httpClient:HttpClient) { }


  getProducts():Observable<ListResponseModel<Product>>{
    let newPath = this.apiURL + "products/getall"
    return this.httpClient.get<ListResponseModel<Product>>(newPath);   
  
   }

   getProductsByCategory(categoryId:number):Observable<ListResponseModel<Product>>{
    let newPath = this.apiURL + "products/getbycategory?categoryId="+categoryId 
    return this.httpClient.get<ListResponseModel<Product>>(newPath);   
  
   }



}
