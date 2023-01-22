import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'; 
import { from, Observable} from 'rxjs';
import { Category } from '../models/category';
import { ListResponseModel } from '../models/listResponseModel';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  apiURL = "https://localhost:7184/api/Categories/getall";
            
  constructor(private httpClient:HttpClient) { }

  getCategories():Observable<ListResponseModel<Category>>{
    return this.httpClient.get<ListResponseModel<Category>>(this.apiURL);     
   }

}
