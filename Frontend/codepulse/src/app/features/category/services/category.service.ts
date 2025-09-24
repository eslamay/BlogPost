import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
 private httpClient=inject(HttpClient);
 private cookieService = inject(CookieService);
 private apiBaseUrl = 'https://localhost:7177';
getAllCategories(query?:string,sortBy?: string,
   sortDirection?: string, pageNumber?: number, 
   pageSize?: number):Observable<Category[]>{
  let params=new HttpParams();
  if(query){
    params=params.set('query',query);
  }

  if(sortBy){
    params=params.set('sortBy',sortBy);
  }

  if(sortDirection){
    params=params.set('sortDirection',sortDirection);
  }

  if(pageNumber){
    params=params.set('pageNumber',pageNumber);
  }

  if(pageSize){
    params=params.set('pageSize',pageSize);
  }

   return this.httpClient.get<Category[]>(`${this.apiBaseUrl}/api/Categories`,{
    params:params
   });
}

getCategoryById(id: string): Observable<Category> {
   return this.httpClient.get<Category>(`${this.apiBaseUrl}/api/Categories/${id}`);
 }

getCategoryCount(): Observable<number> {
  return this.httpClient.get<number>(`${this.apiBaseUrl}/api/Categories/count`);
} 

 addCategory(model:AddCategoryRequest):Observable<void>{
   return this.httpClient.post<void>(`${this.apiBaseUrl}/api/Categories`, model,
    {
      headers:{
        'Authorization': this.cookieService.get('authorization')
      }
    }
   );
 }

  updateCategory(id: string, model: UpdateCategoryRequest): Observable<void> {
    return this.httpClient.put<void>(`${this.apiBaseUrl}/api/Categories/${id}`, model,
    {
      headers:{
        'Authorization': this.cookieService.get('authorization')
      }
    });
  }

  deleteCategory(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiBaseUrl}/api/Categories/${id}`,
    {
      headers:{
        'Authorization': this.cookieService.get('authorization')
      }
    });
  }
}
