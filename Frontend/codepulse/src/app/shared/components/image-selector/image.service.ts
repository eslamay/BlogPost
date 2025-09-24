import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BlogImage } from './models/blog-image.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private httpClient=inject(HttpClient)
  private apiBaseUrl = 'https://localhost:7177';

  selectedImage:BehaviorSubject<BlogImage>=new BehaviorSubject<BlogImage>({
    id:'',
    fileName: '',
    title: '',
    url: '',
    fileExtension: '',
  });

  getAllImages(): Observable<BlogImage[]> {
    return this.httpClient.get<BlogImage[]>(`${this.apiBaseUrl}/api/Images`);
  }

  UploadImage(file: File, fileName: string, title: string): Observable<BlogImage> {
     const formData = new FormData();
     formData.append('file', file);
     formData.append('fileName', fileName);
     formData.append('title', title);

     return this.httpClient.post<BlogImage>(`${this.apiBaseUrl}/api/Images`, formData);
  }

  setSelectedImage(image: BlogImage): void {
    this.selectedImage.next(image);
  }

  onSelectedImage(): Observable<BlogImage> {
    return this.selectedImage.asObservable();
  }
}
