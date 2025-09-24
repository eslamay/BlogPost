import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPost } from '../models/blog-post.model';
import { UpdateBlogPost } from '../models/edit-blog-post.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class BlogPostService {
 
  private apiBaseUrl = 'https://localhost:7177';
  private httpClient = inject(HttpClient);
  private cookieService = inject(CookieService);

  getAllBlogPosts(): Observable<BlogPost[]> {
    return this.httpClient.get<BlogPost[]>(`${this.apiBaseUrl}/api/BlogPost`);
  }

  getBlogPostById(id: string): Observable<BlogPost> {
    return this.httpClient.get<BlogPost>(`${this.apiBaseUrl}/api/BlogPost/${id}`);
  }

  getBlogPostByUrlHandle(urlHandle: string): Observable<BlogPost> {
    return this.httpClient.get<BlogPost>(`${this.apiBaseUrl}/api/BlogPost/${urlHandle}`);
  }

  addBlogPost(model: AddBlogPost): Observable<BlogPost> {
    return this.httpClient.post<BlogPost>(`${this.apiBaseUrl}/api/BlogPost`, model,
    {
      headers:{
        'Authorization': this.cookieService.get('authorization')
      }
    });
  }

  updateBlogPost(id: string, updateBlogPost: UpdateBlogPost): Observable<BlogPost> {
    return this.httpClient.put<BlogPost>(`${this.apiBaseUrl}/api/BlogPost/${id}`, updateBlogPost,
    {
      headers:{
        'Authorization': this.cookieService.get('authorization')
      }
    });
  }

  deleteBlogPost(id: string): Observable<BlogPost> {
    return this.httpClient.delete<BlogPost>(`${this.apiBaseUrl}/api/BlogPost/${id}`,
    {
      headers:{
        'Authorization': this.cookieService.get('authorization')
      }
    });
  }
}
