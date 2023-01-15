import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { ProductResponseModel } from '../models/productResponseModel';
import { from, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  apiURL = "https://localhost:7184/api/products/getall";
            

  constructor(private httpClient:HttpClient) { }


  getProducts():Observable<ProductResponseModel>{
    return this.httpClient.get<ProductResponseModel>(this.apiURL);   
  
   }




}
